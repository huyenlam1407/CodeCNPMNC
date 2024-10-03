using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaSachAdmin.Models;

namespace WebNhaSachAdmin.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC XuatDuLieuSachN");
            return View();
        }
       
        public ActionResult Details(String id)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC TIMKIEMSACHTHEOIDN " + id + ";");
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC KIEMTRADANGNHAP '" + username + "','" + password + "'");
            if (ViewBag.list.Count > 0)
            {
                Session["taikhoan"] = username;
                TempData["SuccessMessage"] = "Đăng nhập thành công!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return RedirectToAction("DangNhap", "Home");
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            TempData["SuccessMessage"] = "Bạn đã đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }

    }
}