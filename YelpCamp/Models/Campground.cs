using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YelpCamp.Models
{
    public class Campground
    {
        public Campground()
        {
            Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Description { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }



    }
    public enum FlashMessageType
    {
       success,
        warning,
        danger,
        info
    };
}