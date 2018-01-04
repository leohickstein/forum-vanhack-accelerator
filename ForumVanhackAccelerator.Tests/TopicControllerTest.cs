using ForumVanhackAccelerator.Controllers;
using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Linq;

namespace ForumVanhackAccelerator.Tests
{
    public class TopicControllerTest
    {
        protected TopicController ControllerUnderTest { get; }
        protected Mock<ITopicService> TopicServiceMock { get; }

        public TopicControllerTest()
        {
            TopicServiceMock = new Mock<ITopicService>();
            ControllerUnderTest = new TopicController(TopicServiceMock.Object);
        }

        public class GetAllAsync : TopicControllerTest
        {
            [Fact]
            public async void Should_return_OkObjectResult_with_topics()
            {
                // Arrange
                var expectedTopics = new Topic[]
                {
                    new Topic { Id = 1, Title = "Titulo 1", Description = "Descrição 1", UserId = "1", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now },
                    new Topic { Id = 2, Title = "Titulo 2", Description = "Descrição 2", UserId = "2", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now },
                    new Topic { Id = 3, Title = "Titulo 3", Description = "Descrição 3", UserId = "3", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now }
                };
                TopicServiceMock
                    .Setup(x => x.GetAllAsync())
                    .ReturnsAsync(expectedTopics);  // Mocked the GetAllAsync() result

                // Act
                var result = await ControllerUnderTest.GetAllAsync();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedTopics, okResult.Value);
            }
        }
    }
}
