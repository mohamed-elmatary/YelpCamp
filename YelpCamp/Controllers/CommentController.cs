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
        [HttpGet]
        public ActionResult CreateComment(int CampgroundId)
        {
             
            var campground =_context.Campgrounds.FirstOrDefault(c => c.Id == CampgroundId);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("Details","Campground", new { Id = CampgroundId });

            }

            CommentDTO commentDto = new CommentDTO() { CampgroundId = CampgroundId, CampgroundName = campground.Name  };
            return View("CreateComment", commentDto);

        }
        [HttpPost]
        public ActionResult CreateComment(CommentDTO CommetDto)
        {
            var campground = _context.Campgrounds.FirstOrDefault(c => c.Id == CommetDto.CampgroundId);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }
            Comment comment = new Comment() { Text = CommetDto.Text, ApplicationUserId = User.Identity.GetUserId() ,CreatedAt = DateTime.Now};
            campground.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Campground", new { Id = CommetDto.CampgroundId });
        }

        [HttpGet]
        public ActionResult Edit(int CampId , int CommentId)
        {
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
            if (comment.ApplicationUserId != User.Identity.GetUserId())
            {
                FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }

            CommentDTO CommentDto = new CommentDTO() { Id = comment.Id, Text = comment.Text , CampgroundId = CampId};

            return View("Edit", CommentDto);

        }
        [HttpPost]
        public ActionResult Edit(CommentDTO CommentDto)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", CommentDto);
            }


            var campground = _context.Campgrounds.Include("Comments").FirstOrDefault(C => C.Id == CommentDto.CampgroundId);
            if (campground == null)
            {
                FlashMessage("Campground Not Found", FlashMessageType.info);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }
            var comment = campground.Comments.FirstOrDefault(c => c.Id == CommentDto.Id);
            if (comment == null)
            {
                FlashMessage("Comment Not Found", FlashMessageType.info);
                return RedirectToAction("Details", "Campground", new { Id = campground.Id });

            }
            if (comment.ApplicationUserId != User.Identity.GetUserId())
            {
                {
                    FlashMessage("You Do Not Have Permission To Do This", FlashMessageType.warning);
                    return RedirectToAction("Details", "Campground", new { Id = campground.Id });

                }
            }

            comment.Text = CommentDto.Text;
            _context.SaveChanges();

            return RedirectToAction("Details","Campground", new { Id = CommentDto.CampgroundId });

        }
        public ActionResult Delete(int CampId, int CommentId)
        {
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
            if (comment.ApplicationUserId != User.Identity.GetUserId())
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