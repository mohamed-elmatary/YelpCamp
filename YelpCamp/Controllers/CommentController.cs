using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YelpCamp.Dtos;
using YelpCamp.Models;
using YelpCamp.Models.ViewModel;
using Microsoft.AspNet.Identity;


namespace YelpCamp.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext _context;

        public CommentController()
        {
            _context = new ApplicationDbContext();
           
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View();
           
        }
        //[HttpGet]
        //public ActionResult CreateComment(int? CampgroundId)
        //{

        //    var campground =_context.Campgrounds.FirstOrDefault(c => c.Id == CampgroundId);
        //    if (campground == null)
        //    {
        //        FlashMessage("Campground Not Found", FlashMessageType.info);
        //        return RedirectToAction("GetAllCampgrounds", "Campground", new { Id = CampgroundId });

        //    }

        //    CommentDTO commentDto = new CommentDTO() { CampgroundId = campground.Id, CampgroundName = campground.Name  };
        //    return View("CreateComment", commentDto);

        //}
        [HttpPost]
        public ActionResult CreateComment(FormCollection Form , int? CampgroundId)
        {
            var campground = _context.Campgrounds.FirstOrDefault(c => c.Id == CampgroundId);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }
            Comment comment = new Comment() { Text = Form["commentText"] ,CreatedAt = DateTime.Now};
            string currentUserId = User.Identity.GetUserId();
            comment.ApplicationUserId = currentUserId;
            campground.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Campground", new { Id = CampgroundId });
        }

       // [HttpGet]
        //public ActionResult Edit(int CampId , int CommentId)
        //{
        //    var campground = _context.Campgrounds.Include("Comments").FirstOrDefault(C => C.Id == CampId);
        //    if (campground == null)
        //    {
        //        FlashMessage("Campground Not Found", FlashMessageType.info);
        //        return RedirectToAction("GetAllCampgrounds", "Campground");

        //    }
        //    var comment = campground.Comments.FirstOrDefault(c => c.Id == CommentId);
        //    if (comment == null)
        //    {
        //        FlashMessage("Comment Not Found", FlashMessageType.info);
        //        return RedirectToAction("Details", "Campground", new { Id = campground.Id });

        //    }
        //    if ((comment.ApplicationUser.Id != User.Identity.GetUserId()) && !(User.IsInRole("Admin")))
        //    {
        //        FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
        //        return RedirectToAction("Details", "Campground", new { Id = campground.Id });

        //    }

        //    CommentDTO CommentDto = new CommentDTO() { Id = comment.Id, Text = comment.Text , CampgroundId = CampId};

        //    return View("Edit", CommentDto);

        //}
        [HttpPost]
        public ActionResult Edit(FormCollection Form, int CampId, int CommentId)
        {


            var s =Request.Params["commentText"];

            var campground = _context.Campgrounds.Include("Comments").FirstOrDefault(C => C.Id == CampId);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }
            var comment = campground.Comments.FirstOrDefault(c => c.Id == CommentId);
            if (comment == null)
            {
                FlashMessage("Comment Not Found", FlashMessageType.info);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }
            if ((comment.ApplicationUser.Id != User.Identity.GetUserId()) && !(User.IsInRole("Admin")))
            {
                {
                    FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                    return RedirectToAction("Details", "Campground", new { Id = campground.Id });

                }
            }

             comment.Text = Form["commentText"];
            _context.SaveChanges();

            return RedirectToAction("Details","Campground", new { Id = CampId});

        }
        public ActionResult Delete(int CampId, int CommentId)
        {
            var campground = _context.Campgrounds.Include("Comments").FirstOrDefault(C => C.Id == CampId);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("GetAllCampgrounds", "Campground");

            }
            var comment = campground.Comments.FirstOrDefault(c => c.Id == CommentId);
            if (comment == null)
            {
                FlashMessage("Comment Not Found", FlashMessageType.info);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }
            if ((comment.ApplicationUser.Id != User.Identity.GetUserId()) && !(User.IsInRole("Admin")))
            {
                FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }
            campground.Comments.Remove(comment);

            _context.Comments.Remove(comment);
            _context.SaveChanges();



            return RedirectToAction("Details", "Campground", new { Id = CampId });

        }
        private void FlashMessage(string message , FlashMessageType Type)
        {
            TempData["Message"] = message;
            TempData["cls"] = Type;
        }
    }
}