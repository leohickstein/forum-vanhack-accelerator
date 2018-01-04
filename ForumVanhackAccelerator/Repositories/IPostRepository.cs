using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Repositories
{
    public interface IPostRepository
    {
        IQueryable<PostViewModel> GetPosts(int topicId);
        Post GetPost(int postId);
        void CreatePost(Post post);
        void UpdatePost(int postId, string newContent);
        void DeletePost(Post post);
    }
}
