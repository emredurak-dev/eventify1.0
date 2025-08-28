using eventify1._0.Context;
using eventify1._0.Entities;
using eventify1._0.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eventify1._0.Controllers
{
    public class EventController : Controller
    {
        private readonly EventContext db = new EventContext();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== GetEvents STARTED ===");

                var events = db.Events.ToList();
                System.Diagnostics.Debug.WriteLine($"Found {events.Count} events in database");

                if (events.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No events found!");
                    return Json(new List<object>(), JsonRequestBehavior.AllowGet);
                }
            
                foreach (var evt in events)
                {
                    System.Diagnostics.Debug.WriteLine($"Event: ID={evt.EventId}, Title={evt.Title}, Start={evt.StartDate}, End={evt.EndDate}");
                }

                // json'a cevir
                var result = events.Select(e => new
                {
                    id = e.EventId,
                    title = e.Title,
                    start = e.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = e.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    color = e.Color,
                    description = e.Description,
                    allDay = e.IsAllDay
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Returning {result.Count} events");
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetEvents ERROR: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateEvent(Event model)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== CreateEvent STARTED ===");
                System.Diagnostics.Debug.WriteLine($"Model: Title={model.Title}, StartDate={model.StartDate}, EndDate={model.EndDate}, Color={model.Color}");
                System.Diagnostics.Debug.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

                model.EventId = 0;

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    System.Diagnostics.Debug.WriteLine($"Validation errors: {string.Join(", ", errors)}");
                    return Json(new { success = false, message = "Invalid data: " + string.Join(", ", errors) });
                }

                db.Events.Add(model);
                db.SaveChanges();

                System.Diagnostics.Debug.WriteLine($"Event added successfully with ID: {model.EventId}");
                return Json(new { success = true, id = model.EventId });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddEvent ERROR: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EditEvent(Event model)
        {
            try
            {
                var existingEvent = db.Events.Find(model.EventId);
                if (existingEvent == null)
                {
                    return Json(new { success = false, message = "Event not found" });
                }

                existingEvent.Title = model.Title;
                existingEvent.StartDate = model.StartDate;
                existingEvent.EndDate = model.EndDate;
                existingEvent.Color = model.Color;
                existingEvent.Description = model.Description;
                existingEvent.IsAllDay = model.IsAllDay;

                db.Entry(existingEvent).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteEvent(int id)
        {
            try
            {
                var existingEvent = db.Events.Find(id);
                if (existingEvent == null)
                {
                    return Json(new { success = false, message = "Event not found" });
                }

                db.Events.Remove(existingEvent);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateEventTime(int id, DateTime startDate, DateTime endDate)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"UpdateEventTime called - ID: {id}, Start: {startDate}, End: {endDate}");

                var existingEvent = db.Events.Find(id);
                if (existingEvent == null)
                {
                    return Json(new { success = false, message = "Event not found" });
                }

                existingEvent.StartDate = startDate;
                existingEvent.EndDate = endDate;

                db.Entry(existingEvent).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}