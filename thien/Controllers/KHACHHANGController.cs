using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using namj.Models;
using PagedList;

namespace namj.Controllers
{
    public class KHACHHANGController : Controller
    {
        phanmemtinhtiendienEntities db = new phanmemtinhtiendienEntities();
        // GET: KHACHHANG
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            var kh = (from ds in db.KhachHangs select ds).OrderBy(x => x.MaKH);
            int pageSize = 4;

            int pageNumber = (page ?? 1);
            return View(kh.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Edit(String makh)
        {
            KhachHang dg = db.KhachHangs.Find(makh);

            return View(dg);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection f)
        {
            String ma = f.Get("MaKH");
            KhachHang dg = db.KhachHangs.Find(ma);

            dg.DiaChi = f.Get("DiaChi");
            dg.SoDienThoai = f.Get("SoDienThoai");

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(KhachHang kh)
        {
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         
        public ActionResult Details(string makh)
        {
            KhachHang m = db.KhachHangs.Find(makh);
            return View(m);
        }
        public ActionResult Search(string searchString, string search)
        {
            if (searchString == "MaKH")
                return View(db.KhachHangs.Where(s => s.MaKH.StartsWith(search)).ToList());
            else if (searchString == "SoDienThoai")
                return View(db.KhachHangs.Where(s => s.SoDienThoai.StartsWith(search)).ToList());
            else if (searchString == "DiaChi")
                return View(db.KhachHangs.Where(s => s.DiaChi.StartsWith(search)).ToList());
            return View(db.KhachHangs.ToList());
        }
        [HttpGet]
        public ActionResult Delete(string makh)
        {
            KhachHang g = db.KhachHangs.Find(makh);
            return View(g);
        }


        [HttpPost]
        public ActionResult Delete(FormCollection f)
        {
            string makh = f.Get("txtMaKH");

            var kb = (from s in db.DienKes where s.MaKh == makh select s).FirstOrDefault();
            if (kb == null)
            {
                KhachHang kh = db.KhachHangs.Find(makh);
                db.KhachHangs.Remove(kh);
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