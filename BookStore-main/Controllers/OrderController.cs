using BookStore.Data.Cart;
using BookStore.Data.Repositories;
using BookStore.Data.ViewModels;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ShoppingCart _shoppingCart;
        private readonly ILogger<ErrorController> _logger;
        public OrderController(IUnitOfWork unitOfWork, ShoppingCart shoppingCart, ILogger<ErrorController> logger)
        {
            _unitOfWork = unitOfWork;
            _shoppingCart = shoppingCart;
            _logger = logger;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string roleId = User.FindFirstValue(ClaimTypes.Role);
            var order = _unitOfWork.Orders.GetOrderByUserIdAndUserRole(userId, roleId);
            return View(order);
        }

        public IActionResult ShoppingCart()
        {
            var item = _shoppingCart.GetCartItems();
            ViewBag.Total = _shoppingCart.GetCartTotal();
            return View(item);
        }

        public IActionResult AddToCart(int id)
        {
            var item = _unitOfWork.Products.GetProductById(id);
            if (item != null)
            {
                _shoppingCart.AddToCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var item = _unitOfWork.Products.GetProductById(id);
            if (item != null)
            {
                _shoppingCart.RemoveFromCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }


        public IActionResult CompleteTheOrder()
        {
            var items = _shoppingCart.GetCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Product> products;
            products = _unitOfWork.Products.GetAllProducts().ToList();
            foreach (var item in items)
            {
                foreach (var product in products)
                {
                    if (item.Product.Id == product.Id)
                    {
                        product.Stock = false;
                    }
                }
            }
            var shippingInfo = _unitOfWork.ShippingInfos.GetAll().Where(x => x.UserId == userId).LastOrDefault();


            _unitOfWork.Orders.StoreOrder(items, userId, shippingInfo);

            _shoppingCart.ClearCart();
            _unitOfWork.SaveChanges();
            return View("CompleteTheOrder");
        }

        [HttpGet]
        public IActionResult CreateShippingInfo()
        {
            var dropDown = _unitOfWork.Orders.GetDropDownValues();
            ViewBag.ShippingMethod = new SelectList(dropDown.PaymentMethods, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateShippingInfo(ShippingInfo shippingInfo)
        {
            shippingInfo.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dropDown = _unitOfWork.Orders.GetDropDownValues();
            ViewBag.ShippingMethod = new SelectList(dropDown.PaymentMethods, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View(shippingInfo);
            }
            _unitOfWork.ShippingInfos.Add(shippingInfo);
            _unitOfWork.SaveChanges();
            return RedirectToAction("CompleteTheOrder");
        }

        public IActionResult Payment()
        {
            ViewBag.Total = _shoppingCart.GetCartTotal();
            return View();
        }

        [HttpGet]
        public IActionResult ShippingInfoDetails(int id)
        {
            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string roleId = User.FindFirstValue(ClaimTypes.Role);
            var orders = _unitOfWork.Orders.GetOrderByUserIdAndUserRole(user, roleId).Where(x => x.ShippingInfoId == id).ToList();
            foreach(var order in orders)
            {
                return View(order);            
            }
           
            return View();
        }

        [HttpPost]
        public IActionResult CancelOrder(int orderId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Product> products;
            products = _unitOfWork.Products.GetAllProducts().ToList();
            List<OrderItem> orderItems;
            orderItems = _unitOfWork.OrderItems.GetAll().ToList();

            foreach (var orderItem in orderItems)
            {
                if (orderItem != null && orderItem.OrderId == orderId)
                {
                    foreach (var product in products)
                    {
                        if (orderItem.Product.Id == product.Id)
                        {
                            product.Stock = true;
                        }
                    }
                }
            }

            _unitOfWork.Orders.CancelOrder(userId, orderId);
            _unitOfWork.SaveChanges();

            _logger.LogInformation($"Narudžba broj {orderId} je uspješno otlazana od strane korisnika {userId}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CancelOrderByAdmin(int orderId)
        {
            List<Product> products;
            products = _unitOfWork.Products.GetAllProducts().ToList();
            List<OrderItem> orderItems;
            orderItems = _unitOfWork.OrderItems.GetAll().ToList();

            foreach (var orderItem in orderItems)
            {
                if (orderItem != null && orderItem.OrderId == orderId)
                {
                    foreach (var product in products)
                    {
                        if (orderItem.Product.Id == product.Id)
                        {
                            product.Stock = true;
                        }
                    }
                }
            }

            _unitOfWork.Orders.CancelOrderByAdmin(orderId);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult OrderPaid(int orderId)
        {

           _unitOfWork.Orders.OrderPaid(orderId);
           _unitOfWork.SaveChanges();

            _logger.LogInformation($"Narudžba broj {orderId} je plaćena");

            return RedirectToAction("Index");
        }

        public IActionResult PlatformDetails() 
        {
            return View();
        }

    }
}
