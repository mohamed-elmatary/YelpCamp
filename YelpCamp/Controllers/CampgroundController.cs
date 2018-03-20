using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YelpCamp.Dtos;
using YelpCamp.Models;

namespace YelpCamp.Controllers
{
    public class CampgroundController : Controller
    {
        private ApplicationDbContext _context;

        public CampgroundController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Camp
      
        [AllowAnonymous]
        public ActionResult GetAllCampgrounds(string search, int? page)
        {
            

            var campgrounds = _context.Campgrounds.Include("Comments").Where(c => c.Name.Contains(search) || search == null)
                 .ToList()
              .ToPagedList(page ?? 1 , 8);
                //.Select(C => new CampgroundDTO()
                //{ Id = C.Id, Image = C.Image , Name =C.Name ,Description = C.Description , Comments = C.Comments });
            
            return View("GetAllCampgrounds" , campgrounds) ;
        }
        [HttpGet]
        public ActionResult CreateCampground()
        {
            CampgroundDTO campgroundDto = new CampgroundDTO() {Lat= 30.044420,Long = 31.235712 };
            return View(campgroundDto);

        }
        [HttpPost]
        public ActionResult CreateCampground(CampgroundDTO campgroundDTO)
        {

            if(!ModelState.IsValid)
            {
                return View("CreateCampground", campgroundDTO);
            }
            //try
            //{
            var ext = Path.GetExtension(campgroundDTO.ImageFile.FileName);
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".pjpeg" && ext != ".gif" && ext != ".x-png" && ext != ".png")
            {
                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                    return View(campgroundDTO);
                
            }


            var path = Path.Combine(Server.MapPath("~/Upload/Images/"), campgroundDTO.ImageFile.FileName);
            campgroundDTO.ImageFile.SaveAs(path);

            Campground campground = Mapper.Map<CampgroundDTO, Campground>(campgroundDTO);
            campground.CreatedAt = DateTime.Now;
            campground.ApplicationUserId = User.Identity.GetUserId();
            campground.Image = "~/Upload/Images/" + campgroundDTO.ImageFile.FileName;
            _context.Campgrounds.Add(campground);
            _context.SaveChanges();
            return RedirectToAction("GetAllCampgrounds");
            //}
            //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            //{
            //    Exception raise = dbEx;
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            string message = string.Format("{0}:{1}",
            //                validationErrors.Entry.Entity.ToString(),
            //                validationError.ErrorMessage);
            //            // raise a new exception nesting  
            //            // the current instance as InnerException  
            //            raise = new InvalidOperationException(message, raise);
            //        }
            //    }
            //    throw raise;
            //}


        }
        [HttpGet]
        public ActionResult Details(int? Id )
       {
            if (Id == null)
                return RedirectToAction("GetAllCampgrounds");
            var campground = _context.Campgrounds.Include("Comments").Include("ApplicationUser").FirstOrDefault(C => C.Id == Id);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("GetAllCampgrounds");
            }
            CampgroundDTO campDTO = Mapper.Map<Campground, CampgroundDTO>(campground);
            campDTO.Image = campground.Image;

            campDTO.UserName = campground.ApplicationUser.UserName;
            
            //{ Id = campground.Id, Name = campground.Name, Image = campground.Image, Description = campground.Description , Comments = campground.Comments , UserName =campground.ApplicationUser.UserName , UserId = campground.ApplicationUserId };
            return View("Details", campDTO);
            
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Edit(int? CampId )
        {
            if (CampId == null)
                return RedirectToAction("GetAllCampgrounds");
            


            var campground = _context.Campgrounds.Include("Comments").FirstOrDefault(C => C.Id == CampId);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("GetAllCampgrounds");
            }
            if ((campground.ApplicationUser.Id != User.Identity.GetUserId()) && !(User.IsInRole("Admin")))
            {
                FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                return RedirectToAction("GetAllCampgrounds");             
            }
          
            CampgroundDTO campDTO = Mapper.Map<Campground, CampgroundDTO>(campground);

            // { Id = campground.Id, Name = campground.Name, Image = campground.Image, Description = campground.Description, Comments = campground.Comments, UserName = campground.ApplicationUser.UserName };
            return View("Edit", campDTO);

        }
        [HttpPost]
        public ActionResult Edit(CampgroundDTO CampDto)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", CampDto);
            }
            var campground = _context.Campgrounds.FirstOrDefault(C => C.Id == CampDto.Id);

            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);

                return RedirectToAction("GetAllCampgrounds");
            }
            if ((campground.ApplicationUser.Id != User.Identity.GetUserId()) && !(User.IsInRole("Admin")))
            {

                FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                return RedirectToAction("Details", new { Id = CampDto.Id });

            }
            //campground = Mapper.Map<CampgroundDTO, Campground>(CampDto);
            if (CampDto.ImageFile != null)
            {
                var ext = Path.GetExtension(CampDto.ImageFile.FileName);
                if (ext != ".jpg" && ext != ".jpeg" && ext != ".pjpeg" && ext != ".gif" && ext != ".x-png" && ext != ".png")
                {
                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                    return View(CampDto);

                }
                var path = Path.Combine(Server.MapPath("~/Upload/Images/"), CampDto.ImageFile.FileName);
                CampDto.ImageFile.SaveAs(path);
                campground.Image = "~/Upload/Images/" + CampDto.ImageFile.FileName;
            }

            campground.Description = CampDto.Description;
            campground.Name = CampDto.Name;
            campground.Price = CampDto.Price;
            campground.Lat = CampDto.Lat;
            campground.Long = CampDto.Long;
            campground.Address = CampDto.Address;

            _context.SaveChanges();

            return RedirectToAction("Details",new {Id = CampDto.Id });

        }
        public ActionResult Delete(int? CampId)
        {
            if(CampId == null)
            {
                return RedirectToAction("GetAllCampgrounds");
            }
            var campground = _context.Campgrounds.FirstOrDefault(C => C.Id == CampId);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("GetAllCampgrounds");

            }
            if ((campground.ApplicationUser.Id != User.Identity.GetUserId()) && !(User.IsInRole("Admin")))
            {
                FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                return RedirectToAction("GetAllCampgrounds");

            }

            _context.Campgrounds.Remove(campground);
            _context.SaveChanges();
            return RedirectToAction("GetAllCampgrounds");

        }
        private void FlashMessage(string message, FlashMessageType Type)
        {
            TempData["Message"] = message;
            TempData["cls"] = Type;
        }

    }
  
}