using ForumVanhackAccelerator.Data;
using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumDbContext _db;

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<Post, PostViewModel>> AsPostViewModel =
            x => new PostViewModel
            {
                Id = x.Id,
                TopicId = x.TopicId,
                Content = x.Content,
                Username = x.User.UserName,
                CreatedDate = x.CreatedDate,
                LastModifiedDate = x.LastModifiedDate
            };

        public PostRepository(ForumDbContext db)
        {
            _db = db;
        }

        public IQueryable<PostViewModel> GetPosts(int topicId)
        {
            return _db.Posts.Include(p => p.User).Where(p => p.TopicId == topicId).Select(AsPostViewModel);
        }

        public Post GetPost(int postId)
        {
            return _db.Posts.Where(p => p.Id == postId).SingleOrDefault();
        }

        public void CreatePost(Post post)
        {
            // add the new post
            _db.Posts.Add(post);

            // persist the changes into the database
            _db.SaveChanges();
        }

        public void UpdatePost(int postId, string newContent)
        {
            // retrieve the post to edit
            var post = _db.Posts.Where(q => q.Id == postId)
                .FirstOrDefault();

            // update the post
            post.Content = newContent;

            // update last modified date
            post.LastModifiedDate = DateTime.Now;

            // persist the changes into the database
            _db.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            // delete the post
            _db.Posts.Remove(post);

            // persist the changes into the database
            _db.SaveChanges();
        }
    }
}
