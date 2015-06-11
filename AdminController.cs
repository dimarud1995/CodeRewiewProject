using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using SqlGIS.Entity;
using System.Linq;
using System.Collections;
using SqlGIS;

namespace SqlGIS.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private IRepository repository;

        public AdminController(IRepository repo)
        {
            this.repository = repo;
        }

        #region Client
        //Show all Clients
        public ViewResult ClientsList()
        {
            return View(repository.Clients);
        }

        //Edit selected client
        public ViewResult EditClient(int ID)
        {
            Client client = repository.Clients
                                      .FirstOrDefault(p => p.Id == ID);
            return View(client);
        }

        [HttpPost]
        //click on button
        //Edit selected client
        //Save new data
        public ActionResult EditClient(Client client)
        {
            if (ModelState.IsValid)
            {
                repository.SaveClient(client);
                TempData["message"] = string.Format("{0} has been saved", client.Name);
                return RedirectToAction("ClientsList");
            }
            else
            {
                // there is something wrong with the data values
                return View(client);
            }
        }

        //create new
        public ViewResult CreateClient()
        {
            return View("EditClient", new Client());
        }

        [HttpPost]
        //delete selected
        public ActionResult DeleteClient(int ID)
        {
            Client deletedClient = repository.DeleteClient(ID);
            if (deletedClient != null)
                TempData["message"] = string.Format("{0} was deleted", deletedClient.Name);
            return RedirectToAction("EditClient");
        }
        #endregion

        #region Country
        //show all
        public ViewResult CountryList()
        {
            return View(repository.Countrys);
        }
        //show page for editing
        
        public ViewResult EditCountry(int ID)
        {
            Country country = repository.Countrys
                                        .FirstOrDefault(p => p.Id == ID);
            return View(country);
        }

        [HttpPost]
        //do editing with click
        public ActionResult EditCountry(Country country)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCountry(country);
                TempData["message"] = string.Format("{0} has been saved", country.Name);
                return RedirectToAction("CountryList");
            }
            else
            {
                return View(country);
            }
        }
//create new
        public ViewResult CreateCountry()
        {
            return View("EditCountry", new Country());
        }

        [HttpPost]
        //delete selected
        public ActionResult DeleteCountry(int ID)
        {
            Country deletedCountry = repository.DeleteCountry(ID);
            if (deletedCountry != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedCountry.Name);
            }
            return RedirectToAction("CountryList");
        }
        #endregion

        #region Flight
//show all
        public ViewResult FlightList()
        {
            return View(repository.Flights);
        }
//show page for editing
        public ViewResult EditFlight(int ID)
        {
            Flight flight = repository.Flights
                                      .FirstOrDefault(p => p.Id == ID);
            return View(flight);
        }

        [HttpPost]
        //do editing with click
        public ActionResult EditFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                repository.SaveFlight(flight);
                TempData["message"] = string.Format("{0} has been saved", flight.Id);
                return RedirectToAction("FlightList");
            }
            else
            {
                // there is something wrong with the data values
                return View(flight);
            }
        }
//create new
        public ViewResult CreateFlight()
        {
            return View("EditFlight", new Flight());
        }

        [HttpPost]
        //delete selected
        public ActionResult DeleteFlight(int ID)
        {
            Flight deletedFlight = repository.DeleteFlight(ID);
            if (deletedFlight != null)
                TempData["message"] = string.Format("{0} was deleted", deletedFlight.Id);
            return RedirectToAction("EditFlight");
        }

        #endregion

        #region Hotel
        //show all
        public ViewResult HotelList()
        {
            return View(repository.Hotels);
        }
        //show page for editing
        public ViewResult EditHotel(int ID)
        {
            Hotel hotel = repository.Hotels
                                        .FirstOrDefault(p => p.Id == ID);
            return View(hotel);
        }

        [HttpPost]
        //do editing with click
        public ActionResult EditHotel(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                repository.SaveHotel(hotel);
                TempData["message"] = string.Format("{0} has been saved", hotel.HotelName);
                return RedirectToAction("HotelList");
            }
            else
            {
                return View(hotel);
            }
        }
//create new
        public ViewResult CreateHotel()
        {
            return View("EditHotel", new Hotel());
        }
//delete selected
        [HttpPost]
        public ActionResult DeleteHotel(int ID)
        {
            Hotel deletedHotel = repository.DeleteHotel(ID);
            if (deletedHotel != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedHotel.HotelName);
            }
            return RedirectToAction("HotelList");
        }
        #endregion

        #region Trip
        //show all
        public ViewResult TripList()
        {
            return View(repository.Trips);
        }
        //show page for editing
        public ViewResult EditTrip(int ID)
        {
            Trip trip = repository.Trips
                                        .FirstOrDefault(p => p.Id == ID);
                ViewBag.Countries = new SelectList(repository.Countrys, "Id", "Name");

            return View(trip);

        }


        [HttpPost]
        //do editing with click
        public ActionResult EditTrip(Trip trip)
        {
            if (ModelState.IsValid)
            {
                repository.SaveTrip(trip);
                TempData["message"] = string.Format("{0} has been saved", trip.Id);
                return RedirectToAction("TripList");
            }
            else
            {
                return View(trip);
            }
        }
//create new
        public ViewResult CreateTrip()
        {
            ViewBag.Countries = new SelectList(repository.Countrys, "Id", "Name");
            return View("EditTrip", new Trip());
        }

        [HttpPost]
        //delete selected
        public ActionResult DeleteTrip(int ID)
        {
            Trip deletedTrip = repository.DeleteTrip(ID);
            if (deletedTrip != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedTrip.Id);
            }
            return RedirectToAction("TripList");
        }


        #endregion

        #region Resort
        //show all
        public ViewResult ResortList()
        {
            return View(repository.Resorts);
        }
//show page for editing
        public ViewResult EditResort(int ID)
        {
            Resort resort = repository.Resorts
                                        .FirstOrDefault(p => p.Id == ID);
            return View(resort);
        }

        [HttpPost]
        //do editing with click
        public ActionResult EditResort(Resort resort, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                resort.PhotoData = imageData;
                repository.SaveResort(resort);
                TempData["message"] = string.Format("{0} has been saved", resort.Id);
                return RedirectToAction("ResortList");
            }
            else
            {
                return View(resort);
            }
        }
//create new
        public ViewResult CreateResort()
        {
            return View("EditResort", new Resort());
        }
//delete selected
        [HttpPost]
        public ActionResult DeleteResort(int ID)
        {
            Resort deletedResort = repository.DeleteResort(ID);
            if (deletedResort != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedResort.Id);
            }
            return RedirectToAction("ResortList");
        }
        #endregion
    }
}
