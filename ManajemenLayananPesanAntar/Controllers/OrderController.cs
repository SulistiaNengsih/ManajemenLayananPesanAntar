﻿using Microsoft.AspNetCore.Mvc;
using API_Manajemen_Layanan_Pesan_Antar.Services;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Enums;
using Microsoft.Identity.Client;
using static API_Manajemen_Layanan_Pesan_Antar.Controllers.OrderController;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace API_Manajemen_Layanan_Pesan_Antar.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        public readonly IOrderService _svc;

        public OrderController(IOrderService svc)
        {
            _svc = svc;
        }

        [AllowAnonymous]
        [HttpPost("create_order")]
        public ActionResult<ResponseDataInfo<OrderDto>> CreateOrder()
        {
            var response = _svc.CreateOrder();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("add_order_items/{id}")]
        public ActionResult<ResponseDataInfo<OrderDto>> AddOrderItems([FromBody] List<AddOrderItemsRequest> req, long id)
        {
            var response = _svc.AddOrderItems(req, id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("add_order_detail/{id}")]
        public ActionResult<ResponseDataInfo<OrderDto>> AddOrderDetail([FromBody] AddOrderDetailRequest req, long id)
        {
            var response = _svc.AddOrderDetail(req, id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("process_order/{id}")]
        public ActionResult<ResponseDataInfo<OrderDto>> ProcessOrder(long id)
        {
            var response = _svc.ProcessOrder(id);
            return Ok(response);
        }

        [HttpPost("deliver_order/{id}")]
        public ActionResult<ResponseDataInfo<OrderDto>> DeliverOrder([FromBody] DeliverOrderRequest req, long id)
        {
            var response = _svc.DeliverOrder(req, id);
            return Ok(response);
        }

        [HttpPost("complete_order/{id}")]
        public ActionResult<ResponseDataInfo<OrderDto>> CompleteOrder(long id)
        {
            var response = _svc.CompleteOrder(id);
            return Ok(response);
        }

        [HttpPost("suspend_order/{id}")]
        public ActionResult<ResponseDataInfo<OrderDto>> SuspendOrder([FromBody] SuspendOrCancelOderRequest req, long id)
        {
            var response = _svc.SuspendOrder(req, id);
            return Ok(response);
        }

        [HttpPost("cancel_order/{id}")]
        public ActionResult<ResponseDataInfo<OrderDto>> CancelOrder([FromBody] SuspendOrCancelOderRequest req, long id)
        {
            var response = _svc.CancelOrder(req, id);
            return Ok(response);
        }

        [HttpGet("get_order")]
        public ActionResult<ResponseDataInfo<List<OrderDto>>> GetOrder()
        {
            var response = _svc.GetOrder();
            return Ok(response);
        }

        [HttpGet("get_order/{id}")]
        public ActionResult<ResponseDataInfo<OrderDto>> GetOrderById(long id)
        {
            var response = _svc.GetOrderById(id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("get_order_by_order_number/{order_number}")]
        public ActionResult<ResponseDataInfo<OrderDto>> GetTrackOrder(string order_number)
        {
            var response = _svc.GetOrderByOrderNumber(order_number);
            return Ok(response);
        }

        public class AddOrderItemsRequest
        {
            public long product_id { get; set; }
            public int order_qty { get; set; }
        }

        public class AddOrderDetailRequest
        {
            public string cust_name { get; set; }
            public string cust_phone { get; set; }
            public string? delivery_address { get; set; }
            public string? delivery_remark { get; set; }
            public string? location_name { get; set; }
            public double delivery_latitude { get; set; }
            public double delivery_longitude { get; set; }
            public int? cash_amount { get; set; }
        }

        public class DeliverOrderRequest
        {
            public string delivered_by { get; set; }
        }

        public class SuspendOrCancelOderRequest
        {
            public string remark { get; set; }
        }
    }
}
