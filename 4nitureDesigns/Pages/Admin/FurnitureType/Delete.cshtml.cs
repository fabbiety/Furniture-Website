
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.FurnitureType
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public FurnitureTypes FurnitureTypes { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FurnitureTypes = _unitOfWork.FurnitureTypes.GetFirstOrDefault(u=>u.Id==id);

        }

        public async Task<IActionResult> OnPost()
        {
            
            
                var furnitureTypeFromDb = _unitOfWork.FurnitureTypes.GetFirstOrDefault(u => u.Id == FurnitureTypes.Id);
            if (furnitureTypeFromDb != null)
                {
                _unitOfWork.FurnitureTypes.Remove(furnitureTypeFromDb);
                _unitOfWork.Save();
                    TempData["success"] = "Item Deleted Successfully";
                    return RedirectToPage("Index");
                }
                
                
              
            return Page();
        }
    }
}
