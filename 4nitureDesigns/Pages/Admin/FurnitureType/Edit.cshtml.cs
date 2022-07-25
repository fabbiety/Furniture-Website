
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.FurnitureType
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public FurnitureTypes FurnitureTypes { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void  OnGet(int id)
        {
            FurnitureTypes =  _unitOfWork.FurnitureTypes.GetFirstOrDefault(u=>u.Id==id);

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                 

                _unitOfWork.FurnitureTypes.Update(FurnitureTypes);
                _unitOfWork.Save();
                TempData["success"] =  "Successful";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
