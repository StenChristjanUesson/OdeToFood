﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //var model =
            //    from r in _context.Restaurants
            //    orderby r.Review.Average(review => review.Rating) descending
            //    Select new RestaurantListViewModel
            //    {
            //        Id = r.Id,
            //        Name = r.Name,
            //        City = r.City,
            //        Country = r.Country,
            //        CountOfReviews = r.Review.Count
            //    };
            var model = _context.Restaurants.OrderByDescending(r => r.Review.Average(review => review.Rating))
            .Take(10)
            .Select(r => new RestaurantListViewModel
            {
                Id = r.Id,
                Name = r.Name,
                City = r.City,
                Country = r.Country,
                CountOfReviews = r.Review.Count
            });
            return View(model);
        }

        public IActionResult About()
        {
            var model = new AboutModel()
            {
                Name = "Sten",
                Location = "Tallinn"
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
