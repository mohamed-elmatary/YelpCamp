using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
        public ActionResult GetAllCampgrounds()
        {

    

            var campgrounds = _context.Campgrounds.Include("Comments")
                 .ToList()
              .Select(Mapper.Map<Campground, CampgroundDTO>);
                //.Select(C => new CampgroundDTO()
                //{ Id = C.Id, Image = C.Image , Name =C.Name ,Description = C.Description , Comments = C.Comments });
            
            return View("GetAllCampgrounds" , campgrounds) ;
        }
        [HttpGet]
        public ActionResult CreateCampground()
        {
            return View();

        }
        [HttpPost]
        public ActionResult CreateCampground(CampgroundDTO campgroundDTO)
        {

            if(!ModelState.IsValid)
            {
                return View("CreateCampground", campgroundDTO);
            }

            //Campground newCampground = new Campground()
            //{ Name = campgroundDTO.Name, Image = campgroundDTO.Image ,Description = campgroundDTO.Description,Comments =campgroundDTO.Comments , ApplicationUserId = User.Identity.GetUserId()};
            var campground = Mapper.Map<CampgroundDTO, Campground>(campgroundDTO);
            _context.Campgrounds.Add(campground);
            _context.SaveChanges();
            return RedirectToAction("GetAllCampgrounds");

        }
        [HttpGet]
        public ActionResult Details(int Id )
        {
           
            var campground = _context.Campgrounds.Include("Comments").FirstOrDefault(C => C.Id == Id);
            if (campground == null)
            {


                FlashMessage("Campground Not Found", FlashMessageType.info);


                return RedirectToAction("GetAllCampgrounds");

                //return Content("you are not allowed to do this");

            }
            CampgroundDTO campDTO = Mapper.Map<Campground, CampgroundDTO>(campground);
            //{ Id = campground.Id, Name = campground.Name, Image = campground.Image, Description = campground.Description , Comments = campground.Comments , UserName =campground.ApplicationUser.UserName , UserId = campground.ApplicationUserId };
            return View("Details", campDTO);
            
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Edit(int CampId )
        {

            
            var campground = _context.Campgrounds.Include("Comments").FirstOrDefault(C => C.Id == CampId);
            if(campground.ApplicationUserId != User.Identity.GetUserId())
            {
                FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                return RedirectToAction("Details", new { Id = CampId });             
            }
            if (campground == null)
                return HttpNotFound("There is Some error");
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

            if (campground.ApplicationUserId != User.Identity.GetUserId())
            {

                FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                return RedirectToAction("Details", new { Id = CampDto.Id });

            }

            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);

                return RedirectToAction("Details", new { Id = CampDto.Id });
            }
            campground.Image = CampDto.Image;
            campground.Description = CampDto.Description;
            campground.Name = CampDto.Name;
            campground.Price = CampDto.Price;

            _context.SaveChanges();

            return RedirectToAction("Details",new {Id = CampDto.Id });

        }
        public ActionResult Delete(int CampId)
        {
            var campground = _context.Campgrounds.FirstOrDefault(C => C.Id == CampId);
            if (campground.ApplicationUserId != User.Identity.GetUserId())
            {
                FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                return RedirectToAction("Details", new { Id = CampId });

            }
            if (campground == null)
                return HttpNotFound("There is Some error");
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