using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H_M_H.Models;

namespace H_M_H.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        Entities db = new Entities();
        public ActionResult Index()
        {
            return View("Login");
        }
        public ActionResult Search()
        {


            return View();
        }

        public ActionResult Search2(string Id)
        {
            int id = int.Parse(Id);
            Doctor doc = null;
            try
            {
                doc = db.Doctors.First(a => a.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (doc != null)
            {
                RedirectToAction("Admin/ViewDoctor", doc);
            }
            return View("Search");


        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="cust_det"></param>
        /// <returns></returns>
        /// 


        [HttpPost]
        public ActionResult ADMIN_LOGIN(User cust_det)
        {
            User cd = db.Users.FirstOrDefault(c => c.Email == cust_det.Email && c.password == cust_det.password);
            if (cd != null)
            {
                if (cd.Admin_Approval == True)
                {
                    Session["admin_id"] = cd.Id;
                    Session["admin_name"] = cd.f_Name + " " + cd.l_Name;
                    Session["message"] = "Access Granted! Welcome Admin.";

                    return View("Index");
                }

            }
            Session["check"] = "2";
            Session["message"] = "Incorrect EMAIL/PASSWORD!";
            return View("Login");

        }




        /// <summary>
        /// //
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            Session.Remove("Name");
            Session["Name"] = null;
            ViewBag.SignInMessage = "You are loged out.";
            return RedirectToAction("Home/LogIn");

        }
        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult Adduser1(User us)
        {
            db.Users.Add(us);
            db.SaveChanges();
            return View("Index");
        }



        public ActionResult Viewuser()
        {
            List<User> list = db.Users.ToList();
            return View("View1", list);

        }

        public ActionResult DeleteUser(int id)
        {
            Entities db1 = new Entities();
            User i = db1.Users.First(it => it.Id == id);
            db1.Users.Remove(i);
            db1.SaveChanges();
            return RedirectToAction("DeleteUser");
        }
        public ActionResult ViewAllDoctors()
        {
            List<Doctor> list = db.Doctors.ToList();
            return View(list);

        }
        public ActionResult View1()
        {
            return View();
        }

        public ActionResult DelUser()
        {
            return View("DeleteUser", db.Users.ToList());

        }

        public ActionResult Delete2(int Id)
        {
            int uid = Id;
            Doctor dr = new Doctor();
            dr = db.Doctors.First(a => a.Id == uid);
            if (dr != null)
            {
                db.Doctors.Remove(dr);
                db.SaveChanges();
            }
            return View("DeleteUser");

        }

        public ActionResult FileManage()
        {

            return View();
        }




        public ActionResult EditUser()
        {
            ViewBag.Title = "Edit User";

            return View(db.Doctors.ToList());
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit User";

            Doctor dr = db.Doctors.First(x => x.Id == Id);

            return View(dr);
        }

        [HttpPost]
        public ActionResult SaveEdit(User us)
        {
            User temp = db.Users.First(x => x.Id == us.Id);

            temp.f_Name = us.f_Name;
            temp.l_Name = us.l_Name;
            temp.password = us.password;
            temp.PhoneNo = us.PhoneNo;
            db.SaveChanges();

            return RedirectToAction("EditUser");
        }
    }
}
