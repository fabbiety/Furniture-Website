using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.Product
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<Products> Product { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            Product = _unitOfWork.Products.GetAll();
        }
    }
}
