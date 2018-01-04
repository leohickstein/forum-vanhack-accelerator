using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ForumVanhackAccelerator.Data;
using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.Repositories;
using ForumVanhackAccelerator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ForumVanhackAccelerator.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ForumDbContext _db;

        public TopicRepository(ForumDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IQueryable<Topic> GetTopics(string filter)
        {
            if (!String.IsNullOrEmpty(filter))
                return _db.Topics.Include(t => t.User).Where(t => (t.Title.Contains(filter) || t.Description.Contains(filter)));
            else
                return _db.Topics.Include(t => t.User);
        }

        public Topic GetTopic(int topicId) 
        {
            Topic topic = _db.Topics.Include(t => t.User)
                .Where(t => t.Id == topicId)
                .FirstOrDefault();

            return topic;
        }

        public void CreateTopic(Topic topic)
        {
            // add the new topic
            _db.Topics.Add(topic);

            // persist the changes into the database
            _db.SaveChanges();
        }
        
        public void UpdateTopic(int topicId, string title, string description)
        {
            // retrieve the topic to edit
            var topic = _db.Topics.Where(q => q.Id == topicId)
                .FirstOrDefault();

            // update the post
            topic.Title = title;
            topic.Description = description;

            // update last modified date
            topic.LastModifiedDate = DateTime.Now;

            // persist the changes into the database
            _db.SaveChanges();
        }
        
        public void DeleteTopic(Topic topic)
        {
            // delete the topic
            _db.Topics.Remove(topic);

            // persist the changes into the database
            _db.SaveChanges();
        }
    }
}
