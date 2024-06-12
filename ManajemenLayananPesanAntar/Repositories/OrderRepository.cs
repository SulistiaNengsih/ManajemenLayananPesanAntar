using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Enums;
using API_Manajemen_Layanan_Pesan_Antar.Models;
using API_Manajemen_Layanan_Pesan_Antar.Services;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static API_Manajemen_Layanan_Pesan_Antar.Controllers.OrderController;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using FirebaseAdmin;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;

namespace API_Manajemen_Layanan_Pesan_Antar.Repositories
{
    public class OrderRepository : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private static FirebaseApp _firebaseApp;

        public OrderRepository(ApplicationDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ResponseDataInfo<OrderDto> CreateOrder()
        {
            var newOrder = new Order();
            newOrder.order_status = 0;
            newOrder.total_payment = 0;

            newOrder.SetCreated();
            _dbContext.Add<Order>(newOrder);
            _dbContext.SaveChanges();

            var newOrderDto = _mapper.Map<OrderDto>(newOrder);
            return new ResponseDataInfo<OrderDto>()
            {
                data = newOrderDto,
                info = "Pesanan berhasil dibuat."
            };
        }

        public ResponseDataInfo<OrderDto> CreateOrderWithDetails(List<AddOrderItemsRequest> aoiReq, AddOrderDetailRequest aodReq)
        {
            var response = new ResponseDataInfo<OrderDto>();
            response.data = null;
            response.info = "";

            var newOrder = new Order();
            newOrder.order_status = 0;
            newOrder.total_payment = 0;

            newOrder.order_items = new List<OrderItem>();

            if (aoiReq == null)
            {
                response.info = "Item pesanan tidak boleh kosong.";
                return response;
            }

            foreach (var oi in aoiReq)
            {
                var product = _dbContext.Set<Product>().Where(x => x.id == oi.product_id).FirstOrDefault();

                if (product == null)
                {
                    response.info = "Produk tidak ditemukan.";
                    return response;
                }

                if (oi.order_qty > product.available_qty)
                {
                    response.info = $"Stok produk {product.product_name} telah habis. Silahkan pilih produk lain!";
                    return response;
                }

                var newOrderItem = new OrderItem();
                newOrderItem.order_qty = oi.order_qty;
                newOrderItem.unit_price = product.unit_price;
                newOrderItem.subtotal = oi.order_qty * product.unit_price;
                newOrderItem.product = product;
                newOrderItem.SetCreated();

                newOrder.order_items.Add(newOrderItem);
                newOrder.total_payment += newOrderItem.subtotal;
            }

            newOrder.cust_name = aodReq.cust_name;
            newOrder.cust_phone_number = FormatPhoneNumber(aodReq.cust_phone);
            newOrder.cash_amount = aodReq.cash_amount;
            newOrder.SetUpdated();

            newOrder.order_delivery = new OrderDelivery();
            newOrder.order_delivery.SetCreated();
            newOrder.order_delivery.delivery_address = aodReq.delivery_address;
            newOrder.order_delivery.delivery_remark = aodReq.delivery_remark;
            newOrder.order_delivery.delivery_latitude = aodReq.delivery_latitude;
            newOrder.order_delivery.delivery_longitude = aodReq.delivery_longitude;

            newOrder.SetCreated();
            _dbContext.Add<Order>(newOrder);
            _dbContext.SaveChanges();

            var newOrderDto = ProcessOrder(newOrder.id).data;
            if (newOrderDto != null)
            {
                newOrderDto.order_delivery.deliverylatlng = $"{newOrder.order_delivery.delivery_latitude},{newOrder.order_delivery.delivery_longitude}";
                return new ResponseDataInfo<OrderDto>()
                {
                    data = newOrderDto,
                    info = "Pesanan berhasil dibuat."
                };
            } else
            {
                return new ResponseDataInfo<OrderDto>()
                {
                    data = null,
                    info = "Terjadi kesalahan dalam membuat pesanan"
                };
            }
            
        }

        public ResponseDataInfo<OrderDto> AddOrderItems(List<AddOrderItemsRequest> req, long id)
        {
            var response = new ResponseDataInfo<OrderDto>();
            response.data = null;
            response.info = "";

            var order = _dbContext.Set<Order>()
                .Where(x => x.id == id)
                .Include(x => x.order_items)
                .FirstOrDefault();

            if (order == null)
            {
                response.info = "Pesanan tidak ditemukan.";
                return response;
            }

            order.order_items = new List<OrderItem>();

            if (req == null)
            {
                response.info = "Item pesanan tidak boleh kosong.";
                return response;
            }

            foreach (var oi in req)
            {
                var product = _dbContext.Set<Product>().Where(x => x.id == oi.product_id).FirstOrDefault();

                if (product == null)
                {
                    response.info = "Produk tidak ditemukan.";
                    return response;
                }

                if (oi.order_qty > product.available_qty)
                {
                    response.info = $"Stok produk {product.product_name} telah habis. Silahkan pilih produk lain!";
                    return response;
                }

                var newOrderItem = new OrderItem();
                newOrderItem.order_qty = oi.order_qty;
                newOrderItem.unit_price = product.unit_price;
                newOrderItem.subtotal = oi.order_qty * product.unit_price;
                newOrderItem.product = product;
                newOrderItem.SetCreated();

                order.order_items.Add(newOrderItem);
                order.total_payment += newOrderItem.subtotal;
            }

            order.SetUpdated();
            _dbContext.Update<Order>(order);
            _dbContext.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            response.data = orderDto;
            response.info = "Item pesanan berhasil ditambahkan.";

            return response;
        }

        public ResponseDataInfo<OrderDto> AddOrderDetail(AddOrderDetailRequest req, long id)
        {
            var response = new ResponseDataInfo<OrderDto>();
            response.data = null;
            response.info = "";

            var order = GetOrderDetail(id);
            if (order == null)
            {
                response.info = "Pesanan tidak ditemukan.";
                return response;
            }

            order.cust_name = req.cust_name;
            order.cust_phone_number = FormatPhoneNumber(req.cust_phone);
            order.cash_amount = req.cash_amount;
            order.SetUpdated();

            order.order_delivery = new OrderDelivery();
            order.order_delivery.SetCreated();
            order.order_delivery.delivery_address = req.delivery_address;
            order.order_delivery.delivery_remark = req.delivery_remark;
            order.order_delivery.delivery_latitude = req.delivery_latitude;
            order.order_delivery.delivery_longitude = req.delivery_longitude;
            
            _dbContext.Update<Order>(order);
            _dbContext.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.order_delivery.deliverylatlng = $"{order.order_delivery.delivery_latitude},{order.order_delivery.delivery_longitude}";
            response.data = orderDto;
            response.info = "Detail pesanan berhasil ditambahkan.";
            return response;
        }
        
        public ResponseDataInfo<OrderDto> ProcessOrder(long id)
        {
            var response = new ResponseDataInfo<OrderDto>();
            response.data = null;
            response.info = "";

            var order = GetOrderDetail(id);

            if (order == null)
            {
                response.info = "Pesanan tidak ditemukan.";
                return response;
            }

            if (order.order_items.Count == 0)
            {
                response.info = "Item pesanan tidak boleh kosong.";
                return response;
            }

            foreach (var oi in order.order_items)
            {
                if (oi.order_qty > oi.product.available_qty)
                {
                    response.info = $"Stok produk {oi.product.product_name} telah habis. Silahkan pilih produk lain!";
                    return response;
                }
                
                oi.product.available_qty -= oi.order_qty;
                oi.product.SetUpdated();
                _dbContext.Update<Product>(oi.product);
                _dbContext.SaveChanges();
            }

            order.order_number = GenerateOrderNumber();
            order.order_status = OrderStatusEnum.Dalam_Proses;
            order.processed_at = DateTime.UtcNow.AddHours(7);
            order.SetUpdated();

            _dbContext.Update<Order>(order);
            _dbContext.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.tracking_url = GenerateTrackingUrl(orderDto.order_status, orderDto.order_number);
            response.data = orderDto;
            response.info = "Pesanan berhasil dibuat.";

            SendPushNotification();

            return response;
        }
        
        public ResponseDataInfo<OrderDto> DeliverOrder(DeliverOrderRequest req, long id)
        {
            var response = new ResponseDataInfo<OrderDto>();
            response.data = null;
            response.info = "";

            var order = GetOrderDetail(id);

            if (order == null)
            {
                response.info = "Pesanan tidak ditemukan.";
                return response;
            }

            order.order_status = OrderStatusEnum.Dalam_Pengiriman;
            order.delivered_at = DateTime.UtcNow.AddHours(7);
            order.delivered_by = req.delivered_by;
            order.SetUpdated();

            _dbContext.Update<Order>(order);
            _dbContext.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.send_whatsapp_url = GenerateSendWhatsAppUrl(orderDto);
            response.data = orderDto;
            response.info = "Berhasil melakukan pengiriman pesanan.";

            return response;
        }
        
        public ResponseDataInfo<OrderDto> CompleteOrder(long id)
        {
            var response = new ResponseDataInfo<OrderDto>();
            response.data = null;
            response.info = "";

            var order = GetOrderDetail(id);

            if (order == null)
            {
                response.info = "Pesanan tidak ditemukan.";
                return response;
            }

            order.order_status = OrderStatusEnum.Diterima;
            order.received_at = DateTime.UtcNow.AddHours(7);
            order.SetUpdated();

            _dbContext.Update<Order>(order);
            _dbContext.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.send_whatsapp_url = GenerateSendWhatsAppUrl(orderDto);
            response.data = orderDto;
            response.info = "Pesanan telah selesai.";

            return response;
        }
        
        public ResponseDataInfo<OrderDto> SuspendOrder(SuspendOrCancelOderRequest req, long id)
        {
            var response = new ResponseDataInfo<OrderDto>();
            response.data = null;
            response.info = "";

            var order = GetOrderDetail(id);

            if (order == null)
            {
                response.info = "Pesanan tidak ditemukan.";
                return response;
            }

            order.order_status = OrderStatusEnum.Tertunda;
            order.suspended_at = DateTime.UtcNow.AddHours(7);
            order.suspend_remark = req.remark;
            order.SetUpdated();

            _dbContext.Update<Order>(order);
            _dbContext.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.send_whatsapp_url = GenerateSendWhatsAppUrl(orderDto);
            response.data = orderDto;
            response.info = "Pesanan telah ditunda.";

            return response;
        }
        
        public ResponseDataInfo<OrderDto> CancelOrder(SuspendOrCancelOderRequest req, long id)
        {
            var response = new ResponseDataInfo<OrderDto>();
            response.data = null;
            response.info = "";

            var order = GetOrderDetail(id);

            if (order == null)
            {
                response.info = "Pesanan tidak ditemukan.";
                return response;
            }

            order.order_status = OrderStatusEnum.Dibatalkan;
            order.canceled_at = DateTime.UtcNow.AddHours(7);
            order.cancel_remark = req.remark;
            order.SetUpdated();

            foreach (var oi in order.order_items)
            {
                oi.product.available_qty += oi.order_qty;
                oi.product.SetUpdated();
                _dbContext.Update<Product>(oi.product);
                _dbContext.SaveChanges();
            }

            _dbContext.Update<Order>(order);
            _dbContext.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.send_whatsapp_url = GenerateSendWhatsAppUrl(orderDto);
            response.data = orderDto;
            response.info = "Pesanan telah dibatalkan.";

            return response;
        }
        
        public ResponseDataInfo<List<OrderDto>> GetOrder()
        {
            var listOrder = _dbContext.Set<Order>()
                .Include(x => x.order_items)
                .ThenInclude(x => x.product)
                .Include(x => x.order_delivery)
                .OrderByDescending(x => x.updated_at).ToList();

            if (listOrder.Count == 0)
            {
                return new ResponseDataInfo<List<OrderDto>>()
                {
                    data = null,
                    info = "Tidak ada pesanan."
                };
            }

            var listOrderDto = _mapper.Map<List<OrderDto>>(listOrder);
            return new ResponseDataInfo<List<OrderDto>>()
            {
                data = listOrderDto,
                info = "Ok"
            };
        }
        
        public ResponseDataInfo<OrderDto> GetOrderById(long id)
        {
            var order = GetOrderDetail(id);

            if (order == null)
            {
                return new ResponseDataInfo<OrderDto>()
                {
                    data = null,
                    info = "Pesanan tidak ditemukan."
                };
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return new ResponseDataInfo<OrderDto>()
            {
                data = orderDto,
                info = "Ok"
            };
        }
        
        public ResponseDataInfo<OrderDto> GetOrderByOrderNumber(string order_number)
        {
            var order = _dbContext.Set<Order>().Where(x => x.order_number == order_number)
                .Include(x => x.order_items)
                .ThenInclude(x => x.product)
                .Include(x => x.order_delivery)
                .FirstOrDefault();

            if (order == null)
            {
                return new ResponseDataInfo<OrderDto>()
                {
                    data = null,
                    info = "Pesanan tidak ditemukan."
                };
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return new ResponseDataInfo<OrderDto>()
            {
                data = orderDto,
                info = "Ok"
            };
        }

        public ResponseDataInfo<OrderDeliveryDto> UpdateCourierLocation(long orderDeliveryId, string latitude, string longitude)
        {
            var orderDelivery = _dbContext.Order_Deliveries.Where(x => x.id == orderDeliveryId).FirstOrDefault();

            if (orderDelivery == null)
            {
                return new ResponseDataInfo<OrderDeliveryDto>()
                {
                    data = null,
                    info = "order delivery tidak ditemukan."
                };
            }

            orderDelivery.courier_latitude = latitude;
            orderDelivery.courier_longitude = longitude;
            orderDelivery.SetUpdated();
            _dbContext.Update(orderDelivery);
            _dbContext.SaveChanges();

            return new ResponseDataInfo<OrderDeliveryDto>()
            {
                data = _mapper.Map<OrderDeliveryDto>(orderDelivery),
                info = "ok"
            };
        }

        public ResponseDataInfo<string> AddFcmToken(string token)
        {
            var fcmToken = new FcmToken();
            fcmToken.fcm_token = token;
            fcmToken.SetCreated();
            _dbContext.Add<FcmToken>(fcmToken);
            _dbContext.SaveChanges();

            return new ResponseDataInfo<string>()
            {
                data = token,
                info = "Ok"
            };
        }

        private Order GetOrderDetail(long id)
        {
            var order = _dbContext.Set<Order>().Where(x => x.id == id)
                .Include(x => x.order_items)
                .ThenInclude(x => x.product)
                .Include(x => x.order_delivery)
                .FirstOrDefault();

            return order;
        }
        
        private string GenerateSendWhatsAppUrl(OrderDto order)
        {
            var orderStatus = "SEDANG DALAM PENGIRIMAN";
            
            if (order.order_status == OrderStatusEnum.Tertunda.ToString() && !order.suspend_remark.IsNullOrEmpty()) 
            {
                orderStatus = $"TERTUNDA karena {order.suspend_remark.ToUpper()}";
            }
            else if (order.order_status == OrderStatusEnum.Dibatalkan.ToString() && !order.cancel_remark.IsNullOrEmpty())
            {
                orderStatus = $"TELAH DIBATALKAN karena {order.cancel_remark.ToUpper()}";
            }
            else if (order.order_status == OrderStatusEnum.Diterima.ToString())
            {
                orderStatus = "TELAH DITERIMA";
            }

            var message = $"Selamat {GetTimeOfDay()} Bapak/Ibu {order.cust_name} YTH. "
                    + $"Pesanan Anda dari Toko Gas Anton dengan total {ConvertToRupiah(order.total_payment)} {orderStatus}."
                    + $"\n\nKlik link berikut untuk melacak pesanan Anda!"
                    + $"\n{GenerateTrackingUrl(order.order_status, order.order_number)}"
                    + $"\n\nTerima kasih telah berbelanja di Toko Gas Anton.";

            var encodedMessage = Uri.EscapeDataString(message);
            var link = $"https://api.whatsapp.com/send?phone={order.cust_phone_number}&text={encodedMessage}";
            return link;
        }
        
        private static string GenerateTrackingUrl(string orderStatus, string orderNumber)
        {
            var trackingUrl = "";
            if (orderStatus != OrderStatusEnum.Temporer.ToString())
            {
                trackingUrl = $"https://tokoanton/track_order/{orderNumber}";
            }
            return trackingUrl;
        }

        private string GenerateOrderNumber()
        {
            string currentDate = DateTime.Now.AddHours(7).ToString("yyMMdd");
            string orderNumber = currentDate;

            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] randomChars = new char[4];

            for (int i = 0; i < randomChars.Length; i++)
            {
                randomChars[i] = chars[random.Next(chars.Length)];
                orderNumber += randomChars[i];
            }
            return new string(orderNumber);
        }

        public static string ConvertToRupiah(int? amount)
        {
            var cultureInfo = new CultureInfo("id-ID");
            return string.Format(cultureInfo, "{0:C}", amount);
        }

        private string FormatPhoneNumber(string phone_number)
        {
            string cleanedNumber = Regex.Replace(phone_number, @"\D", "");
            if (cleanedNumber.First() == '0')
            {
                cleanedNumber = "62" + cleanedNumber.Substring(1);
            }
            return cleanedNumber;
        }
        
        private string GetTimeOfDay()
        {
            var currentTime = DateTime.Now.AddHours(7);
            var hour = currentTime.Hour;

            if (hour >= 5 && hour < 11)
            {
                return "pagi";
            }
            else if (hour >= 11 && hour < 3)
            {
                return "siang";
            }
            else if (hour >= 3 && hour < 6)
            {
                return "sore";
            }
            else
            {
                return "malam";
            }
        }

        private async void SendPushNotification()
        {
            if (_firebaseApp == null)
            {
                // Initialize FirebaseApp if not already initialized
                _firebaseApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("Credentials/pushnotification-d3c4c-firebase-adminsdk-84t7w-485b5db4eb.json")
                });
            }

            var tokens = _dbContext.Set<FcmToken>().ToList();

            foreach (var token in tokens)
            {
                var message = new Message()
                {
                    Token = token.fcm_token,
                    Notification = new FirebaseAdmin.Messaging.Notification()
                    {
                        Title = "Pesanan baru!",
                        Body = "Terdapat pesanan baru."
                    }
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);
            }
        }
    }
}
