using OdeToFood.Data.Model;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.WebSite.Controllers
{
    //Responding to HTTP Messages from a Controller

    public class RestaurantController : Controller
    {
        private OdeToFood.Data.Services.BaseDataService<Restaurant> db;

        public RestaurantController()
        {
            db = new InMemoryRestaurantData();
        }

        public ActionResult Index()
        {
            var list = db.Get();
            return View(list);
        }

    }
}