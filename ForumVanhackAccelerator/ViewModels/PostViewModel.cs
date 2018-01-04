using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
