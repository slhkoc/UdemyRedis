using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedisInMemory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisInMemory.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {

            if (!_memoryCache.TryGetValue("tarih", out string tarihcache))
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

               
                options.AbsoluteExpiration = DateTime.Now.AddSeconds(10); //--> AbsoluteExpiration
                options.SlidingExpiration = TimeSpan.FromSeconds(10); //--> Sliding Expiration

                options.Priority = CacheItemPriority.High;

                options.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    _memoryCache.Set("callback", $"{key}->{value}=>sebep:{reason}");
                });

                _memoryCache.Set<string>("tarih", DateTime.Now.ToString(), options);

                Product p = new Product { Id = 1, Name = "Kalem", Price = 200 };

                _memoryCache.Set<Product>("product:1", p);
            }
            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue("tarih", out string tarihcache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.zaman = tarihcache;
            ViewBag.callback = callback;
            ViewBag.product = _memoryCache.Get<Product>("product:1");

            return View();
        }
    }
}
