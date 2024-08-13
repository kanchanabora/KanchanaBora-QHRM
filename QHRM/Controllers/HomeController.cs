using Microsoft.AspNetCore.Mvc;
using QHRM.Interfaces;
using QHRM.Models;
using System.Diagnostics;

namespace QHRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductInterface _productRepository;
        public HomeController(ILogger<HomeController> logger, IProductInterface productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index(string msg="")
        {
			try
			{
				IEnumerable<Products> lists = await _productRepository.GetAllProducts();
				ViewData["message"] = msg;
				return View(lists);
			}
			catch (Exception ex)
			{
				return View(ex);
			}

		}

        public IActionResult AddProduct()
        {
			try
			{
				return View();
			}
			catch (Exception ex)
			{
				return View(ex);
			}

		}
		
		[HttpPost]
        public async Task<IActionResult> AddProduct(Products products)
        {
            try
            {
				var msg = ""; 
				if (ModelState.IsValid)
				{
					string productname = await _productRepository.AddProduct(products);
					if (productname != "")
					{
						msg = "Added successfully!";
					}
					else
					{
						msg = "Some problem occurred ";
					}
				}
				return RedirectToAction("Index","Home",new { msg = msg });
			}
            catch(Exception ex) 
            {
                return View(ex);
            }
            
        }
		
		public async Task<IActionResult> DeleteProduct(int id)
		{
            try
            {
				var msg = "";
				if (ModelState.IsValid)
				{
					bool deleted_id = await _productRepository.DeleteProduct(id);
					if (deleted_id)
					{
						msg = "Deleted Successfully!";
					}
					else
					{
						msg = "Some problem occurred";

					}
					return RedirectToAction("Index", "Home",new { msg = msg });
				}
				return RedirectToAction("Index", "Home", new { msg = msg });
			}
            catch (Exception ex)
            {
                return View(ex);
            }
           
        }

        public async Task<IActionResult> EditProduct(int id,int updatedid=0,bool status=false)
        {
            try
            {
				if (ModelState.IsValid)
				{
					Products editrecord = await _productRepository.GetProductById(id);
					ViewData["id"] = id;
					return View(editrecord);
				}
				return View();
			}
			catch (Exception ex)
			{
				return View(ex);
			}
		}

		public async Task<IActionResult> UpdateProduct(int id,Products products)
		{
            try
            {
				var msg = "";
				if (ModelState.IsValid)
				{
					bool recordupdated = await _productRepository.UpdateProduct(id, products);
					if (recordupdated)
					{
						msg = "Updated Successfully!";
					}
					else
					{
						msg = "Some error occurred";
					}
					return RedirectToAction("Index", "Home", new { msg = msg });
				}
				return RedirectToAction("Index", "Home", new { msg = msg });
			}
			catch (Exception ex)
			{
				return View(ex);
			}

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}