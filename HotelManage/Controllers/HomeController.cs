using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManage.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 酒店规定首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}