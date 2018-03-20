using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using YelpCamp.Models;
using System.ComponentModel.DataAnnotations;

namespace YelpCamp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //[Required, MaxLength(15)]
        //public override string UserName { get; set; }
        //[ForeignKey("CommentId")]
        //public virtual Comment Comment { get; set; }
        public ApplicationUser()
        {
            Campgrounds = new HashSet<Campground>();
            Comments = new HashSet<Comment>();

        }
        public virtual ICollection<Campground> Campgrounds { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        //[ForeignKey("Comment")]
        //public int? CommentId { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}