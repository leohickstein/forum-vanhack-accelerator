using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.Repositories;
using ForumVanhackAccelerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IQueryable<PostViewModel> GetPosts(int topicId)
        {
            return _postRepository.GetPosts(topicId);
        }

        public Post GetPost(int postId)
        {
            return _postRepository.GetPost(postId);
        }

        public void CreatePost(Post post)
        {
            _postRepository.CreatePost(post);
        }

        public void UpdatePost(int postId, string newContent)
        {
            _postRepository.UpdatePost(postId, newContent);
        }

        public void DeletePost(Post post)
        {
            _postRepository.DeletePost(post);
        }
    }
}
