using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H_M_H.Models;


namespace H_M_H.Controllers
{
    public class PatientController : Controller
    {
        //
        // GET: /Patient/
        Entities context = new Entities();
        //
        // GET: /Doctor/

        public ActionResult Index()
        {
            if (Session["p_object"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult HealthTips()
        {
            if (Session["p_object"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        //show all doctors list
        public ActionResult Appointment()
        {
            if (Session["p_object"] !=null)
            {
                List<Object> list = new List<object>();
                List<Doctor> doc_List = context.Doctors.ToList();
                list.Add(doc_List);
                //parsing user object 
                User us =(User)Session["p_object"];

                Patient pa = context.Patients.FirstOrDefault(p=>p.u_Id==us.Id);
                list.Add(pa);
                return View(list);
            }
            return RedirectToAction("Index", "Home");
        }
        //save appointment request
        public JsonResult Save_APPointment_Request(Appoint_Request ap_req)
        {
            //add object to memory
            context.Appoint_Request.Add(ap_req);
            //save to db
            int result=context.SaveChanges();
            if(result==1)//if data has been saved
            {
                return this.Json(true,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(false,JsonRequestBehavior.AllowGet);
            }            
        }
        public ActionResult Blogs()
        {
            if (Session["p_object"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SearchDoctor()
        {
            if (Session["p_object"] != null)
            {
                List<object> dog_list = new List<object>();
                dog_list.Add(context.Doctors.ToList());
                return View(dog_list);
            }
            return RedirectToAction("Index", "Home");
            
        }

        public ActionResult Signup(User u,Patient p)
        {

            u.Patients.Add(p);
            context.Users.Add(u);
            int res = context.SaveChanges();
            if (res > 0)
            {
                Session["p_object"] = p;
            }
            return Redirect("Index");
        }

        public ActionResult LogOut()
        {
            Session.Remove("Name");
            Session.Remove("Id");
            Session["Name"] = null;
            Session["Id"] = null;
            ViewBag.SignInMessage = "You are loged out.";
            return View("Index");
        }

    }
}
