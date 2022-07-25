using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages
{
    [BindProperties]
    public class ContactUsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Contact Contact { get; set; }

        public ContactUsModel(IUnitOfWork unitOfWork)
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
                _unitOfWork.Contact.Add(Contact);
                _unitOfWork.Save();
                TempData["success"] = "Thank you, Message Received";
               
            }
            return Page();
        }
    }
}
