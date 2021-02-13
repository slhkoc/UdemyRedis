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
            //1.yol
            if(String.IsNullOrEmpty(_memoryCache.Get<string>("tarih")))
            {
                _memoryCache.Set<string>("tarih", DateTime.Now.ToString());
            }
            
            //2.yol
            if(!_memoryCache.TryGetValue("tarih",out string tarihcache))
            {
                _memoryCache.Set<string>("tarih", DateTime.Now.ToString());
            }

            


            return View();
        }

        public IActionResult Show()
        {

            _memoryCache.Remove("tarih"); //silmek için

            _memoryCache.GetOrCreate<string>("tarih", entry =>
            {
                return DateTime.Now.ToString();
            });


            ViewBag.zaman=_memoryCache.Get<string>("tarih");

            return View();
        }
    }
}
