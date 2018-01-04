using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Data.Model
{
    public class Post
    {
        #region Constructor

        public Post() { }

        #endregion

        #region Properties

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TopicId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        #endregion

        #region Lazy-Load Properties

        /// <summary>
        /// The post author: it will be loaded on first use by the EF Lazy-Loading feature.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// The post topic: it will be loaded on first use by the EF Lazy-Loading feature.
        /// </summary>
        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }

        #endregion
    }
}
