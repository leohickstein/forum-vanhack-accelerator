using ForumVanhackAccelerator.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumVanhackAccelerator.ViewModels;

namespace ForumVanhackAccelerator.Repositories
{
    public interface ITopicRepository
    {
        IQueryable<Topic> GetTopics(string filter);
        Topic GetTopic(int topicId);
        void CreateTopic(Topic topic);
        void UpdateTopic(int topicId, string title, string description);
        void DeleteTopic(Topic topic);
    }
}
