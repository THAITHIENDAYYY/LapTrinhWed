using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using namj.Models;
using PagedList;

namespace namj.Controllers
{
    public class DIENKEController : Controller
    {
        phanmemtinhtiendienEntities db = new phanmemtinhtiendienEntities();
        // GET: DIENKE
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            var dk = (from ds in db.DienKes select ds).OrderBy(x => x.MaDienKe);
            int pageSize = 4;

            int pageNumber = (page ?? 1);
            return View(dk.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DienKe dk)
        {
            db.DienKes.Add(dk);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(String madk)
        {
          DienKe dg = db.DienKes.Find(madk);

            return View(dg);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection f)
        {
            String ma = f.Get("MaDienKe");
            DienKe dg = db.DienKes.Find(ma);

            dg.MaDienKe = (f.Get("MaDienKe"));
            dg.HieuDienThe = f.Get("HieuDienThe");
            dg.MaKh = (f.Get("MaKH"));
            dg.MaKhuVuc = (f.Get("MaKhuVuc"));
            dg.NuocSanXuat = (f.Get("NuocSanXuat"));
            dg.GhiChu = (f.Get("GhiChu"));

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string madk)
        {
            DienKe m = db.DienKes.Find(madk);
            return View(m);
        }
        public ActionResult Search(string searchString, string search)
        {
            if (searchString == "MaDienKe")
                return View(db.DienKes.Where(s => s.MaDienKe.StartsWith(search)).ToList());
            else if (searchString == "HieuDienThe")
                return View(db.DienKes.Where(s => s.HieuDienThe.StartsWith(search)).ToList());
            else if (searchString == "Makh")
                return View(db.DienKes.Where(s => s.MaKh.StartsWith(search)).ToList());
            if (searchString == "MaKhuVuc")
                return View(db.DienKes.Where(s => s.MaKhuVuc.StartsWith(search)).ToList());
            else if (searchString == "NuocSanXuat")
                return View(db.DienKes.Where(s => s.NuocSanXuat.StartsWith(search)).ToList());
            else if (searchString == "GhiChu")
                return View(db.DienKes.Where(s => s.GhiChu.StartsWith(search)).ToList());
            return View(db.DienKes.ToList());
        }
        [HttpGet]
        public ActionResult Delete(string madk)
        {
            DienKe g = db.DienKes.Find(madk);
            return View(g);
        }


        [HttpPost]
        public ActionResult Delete(FormCollection f)
        {
            string madk = f.Get("txtMaDienKe");

            var kb = (from s in db.HoaDons where s.MaDienKe == madk select s).FirstOrDefault();
            if (kb == null)
            {
                DienKe i = db.DienKes.Find(madk);
                db.DienKes.Remove(i);
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