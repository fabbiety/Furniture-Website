using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using Team.DataBase.Repository.IRepository;
using Team.Models;
using Team.Utilities;

namespace _4nitureDesigns.Pages.Customer.Cart
{
    public class OrderConfirmationModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public int OrderId { get; set; }
        public OrderConfirmationModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           

        }
        public void OnGet(int id)
        {
           
            OrderId = id;
        }
    }
}
