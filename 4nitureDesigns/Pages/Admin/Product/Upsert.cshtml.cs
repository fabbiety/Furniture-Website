

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace _4nitureDesigns.Pages.Admin.Product
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;      //For picking images from file

        
        public Products Products { get; set; }

        //creating the dropdown on the create product form
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FurnitureTypeList { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            Products = new();  /*to hold the new method for new product*/
        }
        public void OnGet(int? id)
        {
            if(id != null)
            {
                //Editing
                Products = _unitOfWork.Products.GetFirstOrDefault(u => u.Id == id);
            }

            //Populating the Category and the furnitureType list to the form
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            FurnitureTypeList = _unitOfWork.FurnitureTypes.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
           string webRootPath = _hostEnvironment.WebRootPath;   // Pointing to the root folder for adding images to /image folder
            var files = HttpContext.Request.Form.Files;
            if(Products.Id == 0)
            {
                //creating
                string fileName_new =Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\Products");  
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);    
                }

                Products.Image = @"\images\Products\" + fileName_new + extension;
                _unitOfWork.Products.Add(Products);
                _unitOfWork.Save();


            }
            else
            {
                //editing

                var objFromDb = _unitOfWork.Products.GetFirstOrDefault(u=>u.Id == Products.Id);
                if(files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\Products");
                    var extension = Path.GetExtension(files[0].FileName);

                    //deleting the old img
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    //New Upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    Products.Image = @"\images\Products\" + fileName_new + extension;
                }
                else
                {
                    Products.Image = objFromDb.Image;
                }
                _unitOfWork.Products.Update(Products);
                _unitOfWork.Save();
            }
            return RedirectToPage("./Index");
        }
    }
}
