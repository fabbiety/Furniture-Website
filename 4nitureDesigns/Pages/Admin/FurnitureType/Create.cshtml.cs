
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.FurnitureType
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public FurnitureTypes FurnitureTypes { get; set; }
       
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FurnitureTypes.Add(FurnitureTypes);
                 _unitOfWork.Save();
                TempData["success"] = "Item Created Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
