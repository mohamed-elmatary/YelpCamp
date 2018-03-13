using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YelpCamp.Models;

namespace YelpCamp.Dtos
{
    public class CampgroundDTO
    {
        public CampgroundDTO()
        {

        }
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }


        public string UserName { get; set; }

        public string UserId { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}