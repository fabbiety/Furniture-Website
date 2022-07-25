
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.Product
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Products Products { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            Products = _unitOfWork.Products.GetFirstOrDefault(p=>p.Id==id);

        }

        public async Task<IActionResult> OnPost()
        {
            
            
                var productTypeFromDb = _unitOfWork.Products.GetFirstOrDefault(p => p.Id == Products.Id);
            if (productTypeFromDb != null)
                {
                _unitOfWork.Products.Remove(productTypeFromDb);
                _unitOfWork.Save();
                    TempData["success"] = "Item Deleted Successfully";
                    return RedirectToPage("Index");
                }
                
                
              
            return Page();
        }
    }
}
