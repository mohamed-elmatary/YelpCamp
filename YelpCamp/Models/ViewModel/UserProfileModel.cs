using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YelpCamp.Dtos;

namespace YelpCamp.Models.ViewModel
{
    public class UserProfileModel
    {
        public ApplicationUser User { get; set; }

        public List<CampgroundDTO> Campgrounds { get; set; }

    }
}