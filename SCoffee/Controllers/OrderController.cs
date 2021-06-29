using Coffee.Service.Customer;
using Coffee.Service.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCoffee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCoffee.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice)
        {
            _logger.LogInformation("Generating invoice ");
            var order = OderMapper.SerializeInvoiceOrder(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);
            return Ok();
        }

    }
}
