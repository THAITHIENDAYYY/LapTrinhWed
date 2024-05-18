using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using namj.Models;
using PagedList;

namespace namj.Controllers
{
    public class KHUVUCController : Controller
    {
        phanmemtinhtiendienEntities db = new phanmemtinhtiendienEntities();
        // GET: KHUVUC
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            var kv = (from ds in db.KhuVucs select ds).OrderBy(x => x.MaKhuVuc);
            int pageSize = 4;

            int pageNumber = (page ?? 1);
            return View(kv.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(KhuVuc kv)
        {
            db.KhuVucs.Add(kv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(String makv)
        {
            DonGia dg = db.DonGias.Find(makv);

            return View(dg);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection f)
        {
            String ma = f.Get("MaKhuVuc");
            KhuVuc dg = db.KhuVucs.Find(ma);

            dg.MaKhuVuc = f.Get("MaKhuVuc");
            dg.TenKhuVuc = f.Get("TenKhuVuc");
            dg.QuanHuyen = f.Get("QuanHuyen");

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(String makv)
        {
            KhuVuc kv = db.KhuVucs.Find(makv);
            return View(kv);
        }
        public ActionResult Search(string searchString, string search)
        {
            if (searchString == "MaKhuVuc")
                return View(db.KhuVucs.Where(s => s.MaKhuVuc.StartsWith(search)).ToList());
            else if (searchString == "TenKhuVuc")
                return View(db.KhuVucs.Where(s => s.TenKhuVuc.StartsWith(search)).ToList());
            else if (searchString == "QuanHuyen")
                return View(db.KhuVucs.Where(s => s.QuanHuyen.StartsWith(search)).ToList());
            return View(db.KhuVucs.ToList());
        }

        [HttpGet]
        public ActionResult Delete(string makv)
        {
            KhuVuc g = db.KhuVucs.Find(makv);
            return View(g);
        }

        [HttpPost]
        public ActionResult Delete(FormCollection f)
        {
            string makv = f.Get("txtMaKhuVuc");

            var kb = (from s in db.DienKes where s.MaKhuVuc == makv select s).FirstOrDefault();
            if (kb == null)
            {
                KhuVuc i = db.KhuVucs.Find(makv);
                db.KhuVucs.Remove(i);
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