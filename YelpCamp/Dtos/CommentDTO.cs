using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YelpCamp.Dtos
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }

        public string UserId { get; set; }
        public int CampgroundId { get; set; }

        public string CampgroundName { get; set; }

    }
}