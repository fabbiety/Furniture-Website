﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Team.DataBase.Repository.IRepository;
using Team.Utilities;

namespace _4nitureDesigns.ViewComponents
{
    public class ShoppingCart : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCart(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;  
            if(claim != null)
            {
                //User is logged in
                if(HttpContext.Session.GetInt32(StaticDetail.SessionCart)  != null)
                {
                    return View(HttpContext.Session.GetInt32(StaticDetail.SessionCart));
                }
                else
                {
                    count = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
                    return View(count);
                }
            }
            else
            {
                //User has not logged in
                HttpContext.Session.Clear();
                return View(count);
            }
                
        }
    }
}
