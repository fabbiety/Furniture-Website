
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Category Category { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


       
        public void  OnGet(int id)
        {
            Category =  _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id);

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                 

                _unitOfWork.Category.Update(Category);
                _unitOfWork.Save();
                TempData["success"] =  "Successful";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
