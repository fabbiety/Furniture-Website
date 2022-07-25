using Microsoft.AspNetCore.Mvc;
using Team.DataBase.Repository.IRepository;

namespace _4nitureDesigns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var productList= _unitOfWork.Products.GetAll(includeProperties: "category,FurnitureType");
            return Json(new {data=productList});
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            var objFromDb= _unitOfWork.Products.GetFirstOrDefault(u =>u.Id==id);

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Products.Remove(objFromDb);
            _unitOfWork.Save();
           
            return Json(new { success = true, message = "Deleted succesfully"  });

        }
    }
}
