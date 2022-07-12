using AgileBoard.Api.Controllers.V1;
using AgileBoard.Api.Domain;
using AgileBoard.Api.Enums;
using AgileBoard.Api.Services;
using AgileBoard.Api.Tests.Unit.Helpers;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileBoard.Api.Tests.Unit
{
    internal class UserStoriesControllerTests
    {
        private readonly UserStoriesController _sut;
        private readonly Mock<IUserStoryService> _userStoryServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        public UserStoriesControllerTests()
        {
            _sut = new UserStoriesController(_userStoryServiceMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersStoriesExist()
        {
            // Arrange
            _userStoryServiceMock.Setup(x => x.GetUserStoriesAsync()).ReturnsAsync(Enumerable.Empty<UserStory>().ToList());

            // Act
            var result = (OkObjectResult)await _sut.GetAllAsync();

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<UserStory>>().Should().BeNullOrEmpty();
        }

        [Test]
        public async Task GetAllAsync_ShouldContent_WhenUserStoryExist()
        {
            // Arrange
            var userStory = new UserStory
            {
                Id = Guid.NewGuid(),
                Title = "Test User Story Title",
                Owner = "Test Owner XYZ",
                Description = "Test User Story Description",
                AcceptanceCriteria = "Test AC",
                DefintionOfReady = "Test DoR",
                DefinitionOfDone = "Test DoD",
                Comments = null,
                StoryPoints = StoryPoints.Five,
                Priority = Priority.Two,
                Risk = Risk.Medium,
                Deadline = DateTime.Now.AddDays(3),
                Created = DateTime.Now,
                Updated = null
            };

            var expectedUserStories = new[] { userStory };

            //var userStoryResponse = _mapperMock.Setup(x => x.Map<IEnumerable<UserStoryResponse>>(expectedUserStories)).Returns(expectedUserStories.Select(x => x.ToUserStoryResponse()));
            var userStoryResponse = expectedUserStories.Select(x => x.ToUserStoryResponse());

            _userStoryServiceMock.Setup(x => x.GetUserStoriesAsync()).ReturnsAsync(expectedUserStories);
             
            // Act
            var result = (OkObjectResult)await _sut.GetAllAsync();

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<UserStory>>().Should().BeEquivalentTo(userStoryResponse);
        }

        [Test]
        public async Task GetById_ReturnOkAndObject_WhenUserExists()
        {
            // Arrange
            // Arrange
            var userStory = new UserStory
            {
                Id = Guid.NewGuid(),
                Title = "Test User Story Title",
                Owner = "Test Owner XYZ",
                Description = "Test User Story Description",
                AcceptanceCriteria = "Test AC",
                DefintionOfReady = "Test DoR",
                DefinitionOfDone = "Test DoD",
                Comments = null,
                StoryPoints = StoryPoints.Five,
                Priority = Priority.Two,
                Risk = Risk.Medium,
                Deadline = DateTime.Now.AddDays(3),
                Created = DateTime.Now,
                Updated = null
            };
            _userStoryServiceMock.Setup(x => x.GetUserStoryByIdAsync(userStory.Id)).ReturnsAsync(userStory);
            var userStoryResponse = userStory.ToUserStoryResponse();

            // Act
            var result = (OkObjectResult)await _sut.GetAsync(userStory.Id);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(userStoryResponse);
        }
    }
}
