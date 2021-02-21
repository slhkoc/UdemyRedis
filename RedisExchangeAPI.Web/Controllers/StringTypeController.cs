using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {
            db.StringSet("name", "salih");
            db.StringSet("ziyaretci", 100);

            return View();
        }

        public IActionResult Show()
        {
            //var value = db.StringGet("name");
            //var value = db.StringGetRange("name",0,3);
            var value = db.StringLength("name");

            //db.StringIncrement("ziyaretci", 1);

            //var count=db.StringDecrementAsync("ziyaretci", 1).Result;

            db.StringDecrementAsync("ziyaretci", 10).Wait();


            ViewBag.value = value.ToString();


            return View();
        }
    }
}
