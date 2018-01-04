using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumVanhackAccelerator.Data;
using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.Repositories;
using ForumVanhackAccelerator.ViewModels;

namespace ForumVanhackAccelerator.Services
{
    public class TopicService : ITopicService
    {
        private ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository ?? throw new ArgumentNullException(nameof(topicRepository));
        }

        public IQueryable<Topic> GetTopics(string filter)
        {
            return _topicRepository.GetTopics(filter);
        }

        public Topic GetTopic(int topicId) 
        {
            return _topicRepository.GetTopic(topicId);
        }

        public void CreateTopic(Topic topic)
        {
            _topicRepository.CreateTopic(topic);
        }
        
        public void UpdateTopic(int topicId, string title, string description)
        {
            _topicRepository.UpdateTopic(topicId, title, description);
        }
        
        public void DeleteTopic(Topic topic)
        {
            _topicRepository.DeleteTopic(topic);
        }
    }
}
