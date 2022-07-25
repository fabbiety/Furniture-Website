using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.Models;
using System.Collections.Generic;
using Team.DataBase.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Team.Utilities;

namespace _4nitureDesigns.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double SumTotal { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SumTotal = 0;
        }
        public void OnGet()
        {
            //Retriving the User Id from claimsIdentity

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.AppUserId == claim.Value,
                    includeProperties: "Products,Products.FurnitureType,Products.category");

                foreach(var cartItem in ShoppingCartList)
                {
                    SumTotal+=(cartItem.Products.Price*cartItem.Count);  /* Calculating the Cart Total*/
                }
            }
        }

        //Performing the Increment,Decrement and Remove operations on their respective post handlers
        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncreaseCount(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count == 1)
            {
                //Retriving the shopping cart for application user while  implementing session 
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == cart.AppUserId).ToList().Count-1;

                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();

                //Retriving the shopping cart for application user while  implementing session for the Minus method
                HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
            }
            else
            {
                _unitOfWork.ShoppingCart.DecreaseCount(cart, 1);
            }


            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);

            //Retriving the shopping cart for application user while  implementing session 

            var count =   _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == cart.AppUserId).ToList().Count-1;

            

            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();

            //Retriving the shopping cart for application user while  implementing session for the remove method
            HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
            return RedirectToPage("/Customer/Cart/Index");
        }
    }



}
