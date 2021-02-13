using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisInMemory.Web.Controllers
{
    public class ProductController : Controller
    {
        private  IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {

            _memoryCache.Set<string>("tarih", DateTime.Now.ToString());


            return View();
        }

        public IActionResult Show()
        {
            ViewBag.zaman=_memoryCache.Get<string>("tarih");

            return View();
        }
    }
}
