using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Customer.HomePage
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty(SupportsGet = true)]              //Search functionality
        public string searchItem { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<Products> ProductList { get; set; }
       

        public void OnGet()
        {
            ProductList = _unitOfWork.Products.GetAll();
           
            ProductList = _unitOfWork.Products.Search(searchItem);


        }

       

    }
}
