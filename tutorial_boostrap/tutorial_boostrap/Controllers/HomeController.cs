using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using System.Data.Entity;
using tutorial_boostrap.Models;

namespace BootstrapMvcSample.Controllers
{

    public class HomeController : BootstrapBaseController
    {
        private static List<HomeInputModel> _models = ModelIntializer.CreateHomeInputModels();
        private PBSEntities db = new PBSEntities();    
        public ActionResult Index()
        {
           
            var homeInputModels = _models;                                      
            return View(homeInputModels);
        }


        public ActionResult IndexForHome() {
            
            var stylemasters = db.departments.Include(s =>s.subvisions);

            return View(stylemasters.ToList());
        
        }
        
        public ActionResult  SearchForHome(string subName, string depId, string subChef, string subID, string sortBy/*, CancellationToken cancelToken = default(CancellationToken )*/)
        {
           
            //using (HttpClient httpClient = new HttpClient())
            {
                var sub = from s in db.subvisions
                          select s;
                var chef = from z in db.subvisions
                           select z;

                try
                {
                    /* departent sort */

                    ViewBag.DeparName = sortBy == "Department name" ? "DName desc" : "Department name";
                    ViewBag.DeparChef = sortBy == "Department chef" ? "DChef desc" : "Department chef";
                    ViewBag.DeparPhone = sortBy == "Department phone" ? "DPhone desc" : "Department phone";
                    ViewBag.DeparNote = sortBy == "Department note" ? "NDote desc" : "Department note";
                    /*subvision sort */
                    ViewBag.SubName = sortBy == "Subvision name" ? "SName desc" : "Subvision name";
                    ViewBag.SubChef = sortBy == "Subvision chef" ? "SChef desc" : "Subvision chef";
                    ViewBag.SubPhone = sortBy == "Subvision phone" ? "SPhone desc" : "Subvision phone";
                    ViewBag.SubNote = sortBy == "Subvision note" ? "SNote desc" : "Subvision note";


                    //dropdownlist data
                    ViewBag.subID = new SelectList(db.subvisions, "id_subvision", "cheif_subvision");
                    ViewBag.depId = new SelectList(db.departments, "id_department", "department_name");





                    //textbox data search
                    if (!String.IsNullOrEmpty(subName))
                    {
                        sub = sub.Where(c => c.subvision_name.Contains(subName));
                    }
                    if (!String.IsNullOrEmpty(subChef))
                    {

                        return View(sub.Where(v => v.cheif_subvision.Contains(subChef)));
                    }
                    if (!String.IsNullOrEmpty(depId))
                    {
                        int dep = Convert.ToInt32(depId);
                        return View(sub.Where(x => x.department.id_department == dep));
                    }
                    if (!String.IsNullOrEmpty(subID))
                    {
                        int subphone = Convert.ToInt32(subID);
                        return View(sub.Where(y => y.id_subvision == subphone));
                    }


                        //sort 
                    else
                        switch (sortBy)
                        {

                            case "DName desc":
                                sub = sub.OrderByDescending(s => s.department_name);
                                break;
                            case "Department name":
                                sub = sub.OrderBy(s => s.department_name);
                                break;

                            case "DChef desc":
                                sub = sub.OrderByDescending(s => s.department.cheif_department);
                                break;
                            case "Department chef":
                                sub = sub.OrderBy(s => s.department.cheif_department);
                                break;

                            case "DPhone":
                                sub = sub.OrderByDescending(s => s.department.phone_department);
                                break;
                            case "Department phone":
                                sub = sub.OrderBy(s => s.department.phone_department);
                                break;

                            case "DNote":
                                sub = sub.OrderByDescending(s => s.department.note_department);
                                break;
                            case "Department note":
                                sub = sub.OrderBy(s => s.department.note_department);
                                break;

                            //subvision sort
                            case "SName":
                                sub = sub.OrderByDescending(s => s.subvision_name);
                                break;
                            case "Subvision name":
                                sub = sub.OrderBy(s => s.subvision_name);
                                break;

                            case "SChef":
                                sub = sub.OrderByDescending(s => s.phone_subvision);
                                break;
                            case "Subvision chef":
                                sub = sub.OrderBy(s => s.cheif_subvision);
                                break;

                            case "SPhone":
                                sub = sub.OrderByDescending(s => s.phone_subvision);
                                break;
                            case "Subvision phone":
                                sub = sub.OrderBy(s => s.phone_subvision);
                                break;

                            case "SNote":
                                sub = sub.OrderByDescending(s => s.note_subvision);
                                break;
                            case "Subvision note":
                                sub = sub.OrderBy(s => s.note_subvision);
                                break;

                        }
                }
                catch (Exception ex)
                {
                    ViewBag.Message =

               "I'm sorry, but I couldn't load the page," +
               " possibly due to network problems." +
               "Here's the error message I received: "
               + ex.ToString();
                   
                }
                //await Task.WaitAny(tasks:,timeout:30);

                {
                    return View(sub);
                }
            }
        }



        public ActionResult CreateDepartment()
        {
            ViewBag.department_name = new SelectList(db.departments, "department_name", "cheif_department");
            return View();
        }


        //
        // POST: /handbook/Create/department

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDepartment(department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.departments.Add(department);
                    db.SaveChanges();
                }
                        Success("Your information was saved!");
                        return RedirectToAction("CreateDepartment");
                       
                    }

            catch (Exception ex)
            {
               
                ViewBag.department_name = new SelectList(db.departments, "department_name", "cheif_department");
                Error("there were some errors in your form."+ex.ToString());
            }

            
           
            return View(department);
            
        }



        [HttpPost]
        public ActionResult Create(HomeInputModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _models.Count==0?1:_models.Select(x => x.Id).Max() + 1;
                _models.Add(model);
                Success("Your information was saved!");
                return RedirectToAction("Index");
            }
            Error("there were some errors in your form.");
            return View(model);
        }



        public ActionResult Create()
        {
            return View(new HomeInputModel());
        }

        public ActionResult Delete(int id)
        {
            _models.Remove(_models.Get(id));
            Information("Your widget was deleted");
            if(_models.Count==0)
            {
                Attention("You have deleted all the models! Create a new one to continue the demo.");
            }
            return RedirectToAction("index");
        }
        public ActionResult Edit(int id)
        {
            var model = _models.Get(id);
            return View("Create", model);
        }
        [HttpPost]        
        public ActionResult Edit(HomeInputModel model,int id)
        {
            if(ModelState.IsValid)
            {
                _models.Remove(_models.Get(id));
                model.Id = id;
                _models.Add(model);
                Success("The model was updated!");
                return RedirectToAction("index");
            }
            return View("Create", model);
        }

		public ActionResult Details(int id)
        {
            var model = _models.Get(id);
            return View(model);
        }

    }
}
