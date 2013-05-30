﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Home;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new EntityModel());
        }

        [HttpPost]
        public ActionResult ChangeCurrentName(EntityModel model)
        {
            return View("Index", new EntityModel());
        }
    }
}