using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ForumVanhackAccelerator.Data;
using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.Services;
using ForumVanhackAccelerator.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ForumVanhackAccelerator.Controllers
{
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<Topic, TopicViewModel>> AsTopicViewModel =
            x => new TopicViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Username = x.User.UserName,
                CreatedDate = x.CreatedDate,
                LastModifiedDate = x.LastModifiedDate
            };

        public TopicController(
            ITopicService topicService,
            IPostService postService,
            IUserService userService)
        {
            _topicService = topicService ?? throw new ArgumentNullException(nameof(topicService));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET: api/topic
        [HttpGet("~/api/topic/{filter?}")]
        [ProducesResponseType(typeof(IEnumerable<TopicViewModel>), StatusCodes.Status200OK)]
        public IActionResult GetTopics(string filter = "")
        {

            // Call the domain layer to handle the retrieve of data
            var topics = _topicService.GetTopics(filter).Select(AsTopicViewModel);

            return new JsonResult(
                topics,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        // GET: api/topic/5
        [HttpGet("~/api/topic/detail/{topicId}")]
        [ProducesResponseType(typeof(TopicViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int topicId)
        {
            // Call the domain layer to handle the retrieve of data
            var topic = _topicService.GetTopic(topicId);

            // handle requests asking for non-existing topics
            if (topic == null)
            {
               return NotFound(new
               {
                   Error = String.Format("Topic ID {0} has not been found", topicId)
               });
            }

            var topicVM = new TopicViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                Username = topic.User.UserName,
                CreatedDate = topic.CreatedDate,
                LastModifiedDate = topic.LastModifiedDate
            };

            // output the result in JSON format
            return new JsonResult(
                topicVM,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        // POST: api/topic
        [HttpPost]
        public IActionResult Create([FromBody] TopicViewModel model)
        {
            // return a generic HTTP Status 500(Server Error)
            // if the client payload is invalid
            if (model == null) return new StatusCodeResult(500);

            // or if the user does not exist
            var user = _userService.GetUserByUserName(model.Username);
            if (user == null) return new StatusCodeResult(500);

            Topic topic = new Topic()
            {
                Title = model.Title,
                Description = model.Description,
                UserId = user.Id,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            // Call the domain layer to handle the creation
            _topicService.CreateTopic(topic);

            // return an HTTP Status 200 (OK).
            return new NoContentResult();
        }

        // PUT: api/topic/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // POST: api/topic/
        [HttpDelete("{topicId}")]
        //[Authorize]
        public IActionResult Delete(int topicId)
        {
<<<<<<< HEAD
            // retrieve the topic from the Database
=======
            // retrieve the question from the Database
>>>>>>> 8fe180fdc8a30544fc3bd35b111117671477abe4
            var topic = _topicService.GetTopic(topicId);
            if (topic == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Topic ID {0} has not been found", topicId)
                });
            }

            // Call the domain layer to handle the delete
            _topicService.DeleteTopic(topic.Adapt<Topic>());

            // return an HTTP Status 200 (OK).
            return new NoContentResult();
        }
    }
}