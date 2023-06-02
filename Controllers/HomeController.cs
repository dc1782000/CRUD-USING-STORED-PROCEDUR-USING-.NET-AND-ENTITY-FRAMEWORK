using CrudByStoredProcedure.Data;
using CrudByStoredProcedure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudByStoredProcedure.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DcEntities db = new DcEntities();
            List<EmpModel> dbm = new List<EmpModel>();
            var res = db.Employees.ToList();
            foreach (var item in res)
            {
                dbm.Add(new EmpModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    City = item.City
                });
            }
            return View(dbm);
        }
        public ActionResult AddForm()
        {


            return View();
        }

        [HttpPost]
        public ActionResult AddForm(EmpModel model)
        {
            DcEntities db = new DcEntities();
            db.InsertEmp(model.Name, model.City);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            DcEntities db = new DcEntities();
            db.DeleteEmployeeByID(Id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            EmpModel model = new EmpModel();

            DcEntities db = new DcEntities();
            var data = db.Employees.Where(x => x.Id == Id).First();
            model.Id = data.Id;
            model.City = data.City;
            model.Name = data.Name;
            return View(model);
        }


        [HttpPost]

        public ActionResult Edit(EmpModel model)
        {
            DcEntities db = new DcEntities();
            db.UpdateEmployeeName(model.Id, model.Name);
            db.UpdateEmployeeCity(model.Id, model.City);

            return RedirectToAction("Index");
        }






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}