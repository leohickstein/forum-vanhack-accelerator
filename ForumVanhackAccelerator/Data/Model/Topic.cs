using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Data.Model
{
    public class Topic
    {
        #region Constructor

        public Topic() { }

        #endregion

        #region Properties

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        #endregion

        #region Lazy-Load Properties

        /// <summary>
        /// The topic author: it will be loaded on first use by the EF Lazy-Loading feature.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// A list containing all the posts related to this topic.
        /// It will be populated on first use by the EF Lazy-Loading feature.
        /// </summary>
        public virtual List<Post> Posts { get; set; }

        #endregion
    }
}
