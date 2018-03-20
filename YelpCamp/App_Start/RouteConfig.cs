using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YelpCamp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("EditCommentInCampground", 
                "Campground/{CampId}/Comments/Edit/{CommentId}",
                new {controller = "Comment" , action = "Edit"}
                );
            routes.MapRoute("DeleteCommentInCampground",
                "Campground/{CampId}/Comments/Delete/{CommentId}",
                new { controller = "Comment", action = "Delete" }
                );
            routes.MapRoute("UserProfile",
                "Account/GetUserProfile/{UserId}",
                new { controller = "Account", action = "GetUserProfile" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           

        }
    }
}
