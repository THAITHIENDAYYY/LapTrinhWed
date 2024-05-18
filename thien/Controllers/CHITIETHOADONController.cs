using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using namj.Models;
using PagedList;

namespace namj.Controllers
{
    public class CHITIETHOADONController : Controller
    {
        phanmemtinhtiendienEntities db = new phanmemtinhtiendienEntities();
        // GET: CHITIETHOADON
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            var cthd = (from ds in db.ChiTietHoaDons select ds).OrderBy(x => x.MaDonGia);
            int pageSize = 4;

            int pageNumber = (page ?? 1);
            return View(cthd.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ChiTietHoaDon cthd)
        {
            db.ChiTietHoaDons.Add(cthd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id1, string id2)
        {
            ChiTietHoaDon cthd = db.ChiTietHoaDons.Find(id1, id2);
            return View(cthd);
        }
        [HttpGet]
        public ActionResult Edit(string id1, string id2)
        {
            ChiTietHoaDon chitiet = db.ChiTietHoaDons.Find(id1, id2);
            return View(chitiet);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection f)
        {
            string cc1 = f.Get("MaDonGia");
            string cc2 = f.Get("SoHoaDon");
            ChiTietHoaDon cthd = db.ChiTietHoaDons.Find(cc1, cc2);

            cthd.SoLuongKW = f.Get("SoLuongKW");


            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(string id1, string id2)
        {
            ChiTietHoaDon i = db.ChiTietHoaDons.Find(id1, id2);
            return View(i);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1, string id2)
        {
            ChiTietHoaDon chit = db.ChiTietHoaDons.Find(id1, id2);
            db.ChiTietHoaDons.Remove(chit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchString, string search)
        {
            if (searchString == "MaDonGia")
                return View(db.ChiTietHoaDons.Where(s => s.MaDonGia.StartsWith(search)).ToList());
            else if (searchString == "SoHoaDon")
                return View(db.ChiTietHoaDons.Where(s => s.SoHoaDon.StartsWith(search)).ToList());
            else if (searchString == "SoLuongKW")
                return View(db.ChiTietHoaDons.Where(s => s.SoLuongKW.StartsWith(search)).ToList());
            return View(db.ChiTietHoaDons.ToList());
        }
    }
}