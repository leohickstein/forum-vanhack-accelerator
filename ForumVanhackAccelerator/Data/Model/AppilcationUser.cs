using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        #region Constructor

        public ApplicationUser() { }

        #endregion

        #region Properties

        // Already included in the ASP.NET Core Identity mechanism
        //[Key]
        //[Required]
        //public string Id { get; set; }

        // Already included in the ASP.NET Core Identity mechanism
        //[Required]
        //[MaxLength(10)]
        //public string UserName { get; set; }

        #endregion

        #region Lazy-Load Properties

        /// <summary>
        /// A list containing all the topics related to this user.
        /// It will be populated on first use by the EF Lazy-Loading feature.
        /// </summary>
        public virtual List<Topic> Topics { get; set; }

        /// <summary>
        /// A list containing all the posts related to this user.
        /// It will be populated on first use by the EF Lazy-Loading feature.
        /// </summary>
        public virtual List<Post> Posts { get; set; }

        #endregion
    }
}
