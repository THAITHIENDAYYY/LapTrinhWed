using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using namj.Models;
using PagedList;

namespace namj.Controllers
{
    public class HOADONController : Controller
    {
        phanmemtinhtiendienEntities db = new phanmemtinhtiendienEntities();
        // GET: HOADON
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            var hd = (from ds in db.HoaDons select ds).OrderBy(x => x.SoHoaDon);
            int pageSize = 4;

            int pageNumber = (page ?? 1);
            return View(hd.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HoaDon hd)
        {
            db.HoaDons.Add(hd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(String mahd)
        {
            HoaDon dg = db.HoaDons.Find(mahd);

            return View(dg);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection f)
        {
            String ma = f.Get("SoHoaDon");
            HoaDon dg = db.HoaDons.Find(ma);
            dg.SoHoaDon = f.Get("SoHoaDon");
            dg.MaDienKe = f.Get("MaDienKe");
            dg.ThanhTien = f.Get("ThanhTien");
            DateTime Thang;
            if (DateTime.TryParse(f.Get("Thang"), out Thang))
            {

            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string mahd)
        {
            HoaDon m = db.HoaDons.Find(mahd);
            return View(m);
        }
        public ActionResult Search(string searchString, string search)
        {
            if (searchString == "SoHoaDon")
                return View(db.HoaDons.Where(s => s.SoHoaDon.StartsWith(search)).ToList());
            else if (searchString == "MaDienKe")
                return View(db.HoaDons.Where(s => s.MaDienKe.StartsWith(search)).ToList());
            else if (searchString == "ThanhTien")
                return View(db.HoaDons.Where(s => s.ThanhTien.StartsWith(search)).ToList());
            return View(db.HoaDons.ToList());
        }

        [HttpGet]
        public ActionResult Delete(string mahd)
        {
            HoaDon g = db.HoaDons.Find(mahd);
            return View(g);
        }


        [HttpPost]
        public ActionResult Delete(FormCollection f)
        {
            string mahd = f.Get("txtSoHoaDon");

            var kb = (from s in db.ChiTietHoaDons where s.SoHoaDon == mahd select s).FirstOrDefault();
            if (kb == null)
            {
                HoaDon i = db.HoaDons.Find(mahd);
                db.HoaDons.Remove(i);
                db.SaveChanges();
            }
            else
            {
                TempData["error"] = "Không Còn Tồn Tại";
                return RedirectToAction("DeleteTB");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTB()
        {
            ViewBag.Error = TempData["error"];
            return View();
        }
    }
}