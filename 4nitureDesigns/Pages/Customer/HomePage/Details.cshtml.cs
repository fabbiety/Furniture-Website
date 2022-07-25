using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Team.DataBase.Repository.IRepository;
using Team.Models;
using Team.Utilities;

namespace _4nitureDesigns.Pages.Customer.HomePage
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        


        public void OnGet(int id)
        {
            //Retriving the User Id from claimsIdentity

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            

            ShoppingCart = new()
            { 
                AppUserId = claim.Value,
                Products = _unitOfWork.Products.GetFirstOrDefault(u => u.Id == id, includeProperties: "category, FurnitureType"),
                ProductId = id
            };
            
             
            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                 //To check if the existing request for the product already in the database using filter method 
                ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                   filter: u => u.AppUserId == ShoppingCart.AppUserId &&
                u.ProductId == ShoppingCart.ProductId);

                if (shoppingCartFromDb == null)
                {
                    _unitOfWork.ShoppingCart.Add(ShoppingCart);
                    _unitOfWork.Save();

                    //Retriving the shopping cart for application user while  implementing session 

                    HttpContext.Session.SetInt32(StaticDetail.SessionCart, 
                        _unitOfWork.ShoppingCart.GetAll(u=>u.AppUserId==ShoppingCart.AppUserId).ToList().Count);

                }
                else
                {
                    _unitOfWork.ShoppingCart.IncreaseCount(shoppingCartFromDb,ShoppingCart.Count);
                }
                return RedirectToPage("Index");

            }

            return Page();

           



        }

    }
}
