using Microsoft.AspNetCore.Mvc;
using TestA.Models;
using TestA.Repositories;

namespace TestA.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductReponsitory _productReponsitory;

        public ProductsController(ProductReponsitory productReponsitory)
        {
            _productReponsitory = productReponsitory;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productReponsitory.GetAllProduct();
            return View(products);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductModel productModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    await _productReponsitory.CrerateProduct(productModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(productModel);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occured");
                return View(productModel);
            }

           
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productReponsitory.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    await _productReponsitory.DeleteProduct(id);
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occured");
            }
            return RedirectToAction(nameof(Index));



        }

    }
}
