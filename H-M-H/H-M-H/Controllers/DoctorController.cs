using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using H_M_H.Models;


namespace H_M_H.Controllers
{
    public class DoctorController : Controller
    {

        Entities db = new Entities();
        //
        // GET: /Doctor/

        public ActionResult AddTip()
        {
             if (Session["doc_object"] != null)
            {
            List<String> types = null;
            try
            {
                types = db.TipTypes.Select(t => t.Title).ToList();
            }
            catch (Exception e)
            {
                types = null; 
            }
            ViewBag.TipTypes = types;
            return View();
            }
             return RedirectToAction("Index", "Home");
        }



        [HttpPost]
      
         public ActionResult AddNewTip(String tips_Content, String Title, String tips_Type)
        {
            HealthTip tip = new HealthTip( );
            DateTime currentDate = DateTime.Now;
            tip.tips_Date = currentDate;
            tip.tips_Content = tips_Content;
            tip.Title = Title;
            tip.tips_Type = tips_Type;
           
            String ImageName = "" + currentDate.Month + "_" + currentDate.Day + "_" + currentDate.Year+ "_" + currentDate.Hour + "_" + currentDate.Minute + "_" + currentDate.Second + "_";
            int DrID = int.Parse(Session["DrID"].ToString());
            ImageName += DrID + ".jpg"; 
            tip.doc_Id = DrID;
            tip.ImageURL = @"~/Content/images/Tip/" + ImageName;

    try
            {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                       HttpPostedFileBase file = Request.Files[i];
                       file.SaveAs(Server.MapPath(@"~\Content\images\Tip\" + ImageName));


                    }

        
                db.HealthTips.Add(tip);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }


            return Redirect("/Doctor/AddTip");
        }

        public ActionResult Index()
        {
            if (Session["doc_object"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult HealthTips()
        {
            if (Session["doc_object"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Blogs()
        {
            if (Session["doc_object"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult WriteBlog()
        {
            if (Session["doc_object"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SearchDoctor()
        {
            if (Session["doc_object"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult View_Patient()
        {

            if (Session["doc_object"] != null)
            {
                List<Doctor> list = db.Doctors.ToList();

                return View(list);
            }
            return RedirectToAction("Index", "Home");
            
        }


        public ActionResult Signup(User u,Doctor doc)
        {
            u.Doctors.Add(doc);
            db.Users.Add(u);
          int res=  db.SaveChanges();
            if (res>0)
            {
                Session["doc_object"] = doc;
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
