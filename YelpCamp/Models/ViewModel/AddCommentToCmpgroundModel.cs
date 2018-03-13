using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YelpCamp.Dtos;

namespace YelpCamp.Models.ViewModel
{
    public class AddCommentToCmpgroundModel
    {
        public CampgroundDTO CampgroundDto { get; set; }
        public CommentDTO CommentDto { get; set; }

    }
}

