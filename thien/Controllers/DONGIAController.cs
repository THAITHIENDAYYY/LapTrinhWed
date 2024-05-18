using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using namj.Models;
using PagedList;

namespace namj.Controllers
{
    public class DONGIAController : Controller
    {
        phanmemtinhtiendienEntities db = new phanmemtinhtiendienEntities();
        // GET: DONGIA
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            var dg = (from ds in db.DonGias select ds).OrderBy(x => x.MaDonGia);
            int pageSize = 4;

            int pageNumber = (page ?? 1);
            return View(dg.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DonGia dg)
        {
            db.DonGias.Add(dg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(String madg)
        {
            DonGia dg = db.DonGias.Find(madg);

            return View(dg);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection f)
        {
            string ma = f.Get("MaDonGia");
            DonGia dg = db.DonGias.Find(ma);


            int SoTien;
            if (int.TryParse(f.Get("SoTien"), out SoTien))
            {
                dg.SoTien = SoTien;
            }
            int TuKW;
            if (int.TryParse(f.Get("TuKW"), out TuKW))
            {
                dg.TuKW = TuKW;
            }
            int DenKW;
            if (int.TryParse(f.Get("DenKW"), out DenKW))
            {
                dg.DenKW = DenKW;
            }
            dg.GhiChu = f.Get("GhiChu");


            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string madg)
        {
            DonGia m = db.DonGias.Find(madg);
            return View(m);
        }
        public ActionResult Search(string searchString, string search)
        {
            if (searchString == "MaDonGia")
                return View(db.DonGias.Where(s => s.MaDonGia.StartsWith(search)).ToList());
            else if (searchString == "SoTien")
                return View(db.DonGias.Where(s => s.SoTien.ToString().StartsWith(search)).ToList());
            else if (searchString == "TuKW")
                return View(db.DonGias.Where(s => s.TuKW.ToString().StartsWith(search)).ToList());
            else if (searchString == "DenKW")
                return View(db.DonGias.Where(s => s.DenKW.ToString().StartsWith(search)).ToList());
            else if (searchString == "GhiChu")
                return View(db.DonGias.Where(s => s.GhiChu.StartsWith(search)).ToList());
            return View(db.DonGias.ToList());
        }
        [HttpGet]
        public ActionResult Delete(string madg)
        {
            DonGia g = db.DonGias.Find(madg);
            return View(g);
        }


        [HttpPost]
        public ActionResult Delete(FormCollection f)
        {
            string madg = f.Get("txtMaDonGia");

            var kb = (from s in db.ChiTietHoaDons where s.MaDonGia == madg select s).FirstOrDefault();
            if (kb == null)
            {
                DonGia dg = db.DonGias.Find(madg);
                db.DonGias.Remove(dg);
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