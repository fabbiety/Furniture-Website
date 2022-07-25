using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System.Security.Claims;
using Team.DataBase.Repository.IRepository;
using Team.Models;
using Team.Utilities;

namespace _4nitureDesigns.Pages.Customer.Cart

{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }    
        private readonly IUnitOfWork _unitOfWork;
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();    
           
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.AppUserId == claim.Value,
                    includeProperties: "Products,Products.FurnitureType,Products.category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.Products.Price * cartItem.Count);
             
                }

                AppUser appUser = _unitOfWork.AppUser.GetFirstOrDefault(u=>u.Id == claim.Value);    
                OrderHeader.Name=appUser.FirstName+" " + appUser.LastName;
                OrderHeader.PhoneNumber = appUser.PhoneNumber;

            }
        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.AppUserId == claim.Value,
                    includeProperties: "Products,Products.FurnitureType,Products.category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.Products.Price * cartItem.Count);

                }
                OrderHeader.Status = StaticDetail.StatusPending;
               
                OrderHeader.UserId= claim.Value;
                
                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach(var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new()
                    {
                        ProductId = item.ProductId, 
                        OrderId=OrderHeader.Id,
                        Name=item.Products.Name,
                        Price=item.Products.Price,
                        Count=item.Count
                    };
                    _unitOfWork.OrderDetails.Add(orderDetails);
                    
                }
               
                _unitOfWork.Save();



                //STRIPE INTEGRATION FOR CHECKOUT

                var domain = "https://localhost:44353/";
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount =(long)(OrderHeader.OrderTotal*100),
                        Currency="GBP",
                        ProductData= new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Best 4niture Designs"
                        },

                    },
                    Quantity = 1
                  },
                },
                    Mode = "payment",
                    SuccessUrl = domain + $"Customer/Cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "Customer/Cart/Index",
                };
                var service = new Stripe.Checkout.SessionService();
                Stripe.Checkout.Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);

                OrderHeader.TransactionId = session.Id;
                _unitOfWork.Save();
                return new StatusCodeResult(303);

            }
            return Page();
        }
    }
}
