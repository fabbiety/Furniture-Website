
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.UsersMgt

{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppUser AppUser { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public void OnGet(string Email)
        {
            AppUser = _unitOfWork.AppUser.GetFirstOrDefault(x => x.Email == Email); 

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {


                _unitOfWork.AppUser.Update(AppUser);
                _unitOfWork.Save();
                TempData["success"] = "Successful";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
