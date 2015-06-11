using SqlGIS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SqlGIS.Controllers
{
    public class HomePageController : Controller
    {
        //
        // GET: /HomePage/
        IRepository repository;
        public HomePageController(IRepository repo) 
        {
            repository = repo;
        }

        public ActionResult Index1()
        {
            return View();
        }

        [HttpPost]
        //do search
        //with help json choose data
        //choose hotel in country
        public ActionResult Search(string Location)
        {
            
            List<object> hot = new List<object>();
            if (!String.IsNullOrWhiteSpace(Location))
            {
            //choose country
                var country = repository.Countrys
                    .Where(c => c.Name.StartsWith(Location))
                    .Select(c => new
                    {
                        Name = c.Name,
                        Id = c.Id
                    });
                var countr = country.First();

//in trip we have country and hotel
//choose trip with chooosen country
                var trip = repository.Trips
                    .Where(c => c.CountryID == countr.Id)
                    .AsEnumerable()
                    .Select(c => new
                    {
                        HotelID = c.HotelID

                    });
                    //choose hotel
                foreach (var item in trip)
                {
                    hotels = repository.Hotels
                        .Where(c => c.Id == item.HotelID)
                        .AsEnumerable()
                        .Select(c => new
                        {
                            HotelName = c.HotelName,
                            Id = c.Id,
                            X = c.X,
                            Y = c.Y,
                            Address = c.Address,
                            Desctirption = c.Description,
                            Category = c.Category
                        });
                    hot.Add(hotels.First());
                }
                return Json(hot, JsonRequestBehavior.AllowGet);
            }
            return Json(hotels, JsonRequestBehavior.AllowGet);
        }
    }
}
