using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H_M_H.Models;

namespace H_M_H.Controllers
{

    public class HomeController : Controller
    {
        Entities ent = new Entities();
        //
        // GET: /Home/

        
        public ActionResult Index()
        {
            
           // 
            try {

                List<Service1> serv = ent.Service1.ToList();
                ViewBag.Services = serv;

                List<object> doc_list = new List<object>();
                doc_list.Add(ent.Doctors.ToList());
                return View(doc_list);
            }
            catch (Exception ex) { ViewBag.Services = null; }
            return View();
        }
        public ActionResult Features()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Login()
        {
            String useName = Request["uname"];
            String passw=Request["pass"];
            User ob = null;    
       
            if ((useName=="admin") && (passw== "admin123"))
            {
                Session["uname"] = "Admin";
                try
                {                    
                    return RedirectToAction("Index", "Admin");// RedirectToAction("Doctor", "Index");
                }
                catch(Exception e)
                {}
            }
            ob = ent.Users.FirstOrDefault(u=>u.userName ==useName && u.password==passw);
            if(ob!=null)
            {
                
                try
                {
                    if((ent.Doctors.FirstOrDefault(d=>d.u_Id==ob.Id) !=null))//if user is doctor
                    { 
                            Session["uname"] = useName;
                            Session["doc_object"] = ob;
                            Session["DrID"] = ob.Id;

                            return RedirectToAction("Index", "Doctor");// RedirectToAction("Doctor", "Index");
                    }
                    else if((ent.Patients.FirstOrDefault(p=>p.u_Id==ob.Id) !=null)) //if user is patient
                    {
                        Session["uname"] = useName;
                        Session["p_object"]=ob;
                        return RedirectToAction("Index", "Patient");
                    }
                }
                catch(Exception e)
                {}
                
            }                                          
            return View("Index");

        }



        public ActionResult ServiceDetails(int id)
        {


            Service1 serv = null;

            try
            {
                serv = ent.Service1.First(s => s.Id == id);
            }
            catch (Exception ex) { }

            return View(serv);

        }

        public ActionResult Monitoring()
        {

            return View();
        }
        public ActionResult Therapy()
        {
            return View();
        }
        public ActionResult HealthCare()
        {
            return View();
        }
        public ActionResult Serve24_7()
        {
            return View();
        }
        public ActionResult error404()
        {
            return View();
        }
        public ActionResult Medics()
        {
            return View();
        }
        public ActionResult Special_Doctor()
        {
            return View();
        }

        public ActionResult HealthTips()
        {


            List<HealthTip> health_Tips = null;
            List<String> TipsTypes = new List<string>();
            
            TipsTypes.Add("EYE");
            TipsTypes.Add("EAR");
            TipsTypes.Add("Skin");
            TipsTypes.Add("Hands");
            TipsTypes.Add("Foot");
            TipsTypes.Add("Blood_pressure");
            TipsTypes.Add("Head");
            TipsTypes.Add("Hair");
            TipsTypes.Add("Heart");
            

            try
            {
                health_Tips = ent.HealthTips.ToList();
            }catch(Exception ex){
                health_Tips = null;
            }

            ViewBag.TipsTYPES = TipsTypes;

            return View(health_Tips);
        }


        public ActionResult logout()
        {

           // Session["uname"];
            Session.Remove("uname");
            return View("Index");
        }

    }
}
