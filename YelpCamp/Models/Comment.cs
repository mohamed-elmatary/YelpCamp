using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace YelpCamp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public Campground Campground { get; set; }
        [ForeignKey("Campground")]
        public int CampgroundId { get; set; }


        public DateTime CreatedAt { get; set; }
    }
}