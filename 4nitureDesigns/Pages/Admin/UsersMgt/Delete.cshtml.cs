using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.UsersMgt
{
    [BindProperties]
    public class DeleteModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;

        public AppUser AppUser { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public void OnGet(string Email)
        {
            AppUser = _unitOfWork.AppUser.GetFirstOrDefault(u => u.Email == Email);

        }

        public async Task<IActionResult> OnPost()
        {


            //    var AppUserFromDb = _unitOfWork.AppUser.GetFirstOrDefault(u => u.Email == Email);
            //if (AppUserFromDb != null)
            //    {
            _unitOfWork.AppUser.Remove(AppUser);
            _unitOfWork.Save();
            TempData["success"] = "List Item Deleted Successfully";
            return RedirectToPage("Index");
            //}



            return Page();
        }
    }
}
