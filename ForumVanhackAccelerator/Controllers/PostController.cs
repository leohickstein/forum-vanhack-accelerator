using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.Services;
using ForumVanhackAccelerator.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ForumVanhackAccelerator.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostController(
            ITopicService topicService,
            IPostService postService,
            IUserService userService)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET: api/topic
        [HttpGet("{topicId}")]
        [ProducesResponseType(typeof(IEnumerable<PostViewModel>), 200)]
        public IActionResult Get(int topicId)
        {
            // Call the domain layer to handle the retrieve of data
            var posts = _postService.GetPosts(topicId);

            return new JsonResult(
                posts,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        // POST: api/topic
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] PostViewModel model)
        {
            // return a generic HTTP Status 500(Server Error)
            // if the client payload is invalid
            if (model == null) return new StatusCodeResult(500);

            // or if the user does not exist
            var user = _userService.GetUserByUserName(model.Username);
            if (user == null) return new StatusCodeResult(500);

            Post post = new Post()
            {
                TopicId = model.TopicId,
                Content = model.Content,
                UserId = user.Id,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            // Call the domain layer to handle the creation
            _postService.CreatePost(post);

            // return an HTTP Status 200 (OK).
            return new NoContentResult();
        }

        // POST: api/topic
        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] PostViewModel model)
        {
            // return a generic HTTP Status 500(Server Error)
            // if the client payload is invalid
            if (model == null) return new StatusCodeResult(500);

            // Call the domain layer to handle the update
            _postService.UpdatePost(model.Id, model.Content);

            // return an HTTP Status 200 (OK).
            return new NoContentResult();
        }

        // POST: api/post/
        [HttpDelete("{postId}")]
        [Authorize]
        public IActionResult Delete(int postId)
        {
            // retrieve the topic from the Database
            var post = _postService.GetPost(postId);
            if (post == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Post ID {0} has not been found", postId)
                });
            }

            // Call the domain layer to handle the delete
            _postService.DeletePost(post);

            // return an HTTP Status 200 (OK).
            return new NoContentResult();
        }
    }
}