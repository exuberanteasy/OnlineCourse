using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TestStore.Models;

namespace TestStore.Controllers
{
    public class CourseListController : Controller
    {
        TestStoreDBModel db = new TestStoreDBModel();

        // GET: CourseList
        public ActionResult Coursec001()
        {
            return View();
        }

        public ActionResult Coursec002()
        {
            return View();
        }

        public ActionResult Coursec003()
        {
            return View();
        }

        public ActionResult Coursec004()
        {
            return View();
        }

        public ActionResult Subscribe(string fCId)
        {
            //string uid = User.Identity.Name;
            string UserId = (Session["Member"] as Member).UserId;

            var currentCar = db.CourseOrderDetails
                .Where(m => m.UserId == UserId && m.IsApproved == "否" && m.CId == fCId)
                .FirstOrDefault();

            if (currentCar == null)
            {
                var course = db.Courses.Where(m => m.CId == fCId).FirstOrDefault();
                CourseOrderDetail courseOrderDetail = new CourseOrderDetail();
                courseOrderDetail.UserId = UserId;
                courseOrderDetail.CId = course.CId;
                courseOrderDetail.Name = course.Name;
                courseOrderDetail.Price = course.Price;
                courseOrderDetail.IsApproved = "否";
                db.CourseOrderDetails.Add(courseOrderDetail);
            }
            db.SaveChanges();

            return RedirectToAction("Cart");




            //var courseOrderDetails = db.CourseOrderDetails
            //    .Where(m => m.UserId == uid && m.CId == fCId).ToList();

            //var course = db.Courses.Where(m => m.CId == fCId).FirstOrDefault();
            //ShoppingCar newCar = new ShoppingCar();
            //newCar.UserId = uid;
            //newCar.CId = fCId;
            //newCar.CName = course.Name;
            //newCar.Price = course.Price;
            //db.ShoppingCar.Add(newCar);

            //db.SaveChanges();

            //return View("SubscribeList", courseOrderDetails);
        }

        public ActionResult DeleteCarSubscribe(int fId)
        {
            var orderDetail = db.CourseOrderDetails.Where(m => m.Id == fId).FirstOrDefault();

            db.CourseOrderDetails.Remove(orderDetail);
            db.SaveChanges();

            return RedirectToAction("SubscribeList");

        }

        public ActionResult Cart()
        {
            //string uid = User.Identity.Name;

            string uid = (Session["Member"] as Member).UserId;

            var courseOrderDetails = db.CourseOrderDetails
                .Where(m => m.UserId == uid && m.IsApproved == "否").ToList();

            //var shoppingCar = db.ShoppingCar.Where(m => m.UserId == uid).ToList();

            return View("Cart", courseOrderDetails);

        }

        [HttpPost]
        public ActionResult ShoppingCar()
        {
            string UserId = (Session["Member"] as Member).UserId;

            string guid = Guid.NewGuid().ToString();

            CourseOrder courseOrder = new CourseOrder();
            courseOrder.OrderGuid = guid;
            courseOrder.UserId = UserId;
            courseOrder.OrderDate = DateTime.Now;
            db.CourseOrders.Add(courseOrder);

            var carList = db.CourseOrderDetails
                .Where(m => m.IsApproved == "否" && m.UserId == UserId)
                .ToList();

            foreach (var item in carList)
            {
                item.OrderGuid = guid;
                item.IsApproved = "是";
            }

            db.SaveChanges();

            return RedirectToAction("SubscribeList");

        }

        public ActionResult SubscribeList()
        {
            string UserId = (Session["Member"] as Member).UserId;

            var orders = db.CourseOrders.Where(m => m.UserId == UserId)
                .OrderByDescending(m => m.OrderDate).ToList();

            return View("SubscribeList", orders);
        }

     }
}
