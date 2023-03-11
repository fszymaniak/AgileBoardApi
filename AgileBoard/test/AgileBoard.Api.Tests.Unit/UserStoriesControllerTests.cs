using AgileBoard.Api.Controllers.V1;
using AgileBoard.Api.Tests.Unit.Helpers;
using AgileBoard.Core.Abstractions;
using AgileBoard.Core.Contracts.Responses;
using AgileBoard.Core.Domain;
using AgileBoard.Core.Services;
using AgileBoard.Core.ValueObjects;
using AgileBoard.Infrastructure.MappingProfiles;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileBoard.Api.Tests.Unit
{
    public class UserStoriesControllerTests
    {
        #region Arrange 

        private readonly UserStoriesController _sut;
        private readonly Mock<IUserStoryService> _userStoryServiceMock = new();
        private static IMapper _mapper;
        private static Mock<IClock> _clockMock = new();
        private static UserStory _userStory;
        private static HttpContext _httpContex;
        private readonly Date _now;
        

        public UserStoriesControllerTests(IHttpContextAccessor httpContextAccessor)
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DomainToResponseProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _sut = new UserStoriesController(_userStoryServiceMock.Object, _mapper, _clockMock.Object);

            _httpContex = httpContextAccessor.HttpContext;

            _now = Date.Now;

            //_userStory =  new UserStory
            //(
            //    Guid.NewGuid(),
            //    "Test User Story Title",
            //    "Test Owner XYZ",
            //    "Test User Story Description",
            //    "Test AC",
            //    "Test DoR",
            //    "Test DoD",
            //    null,
            //    5,
            //    new Priority(2),
            //    Risk.Medium(),
            //    _now.AddDays(3),
            //    Date.Now,
            //    Date.Now
            //);
        }

        #endregion

        [Test]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersStoriesExist()
        {
            // Arrange
            _userStoryServiceMock.Setup(x => x.GetUserStoriesAsync()).ReturnsAsync(Enumerable.Empty<UserStory>().ToList());

            // Act
            var result = (OkObjectResult)await _sut.GetAllAsync();

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<UserStoryResponse>>().Should().BeNullOrEmpty();
        }

        [Test]
        public async Task GetAllAsync_ShouldContent_WhenUserStoryExist()
        {
            // Arrange
            var expectedUserStories = new[] { _userStory };

            var userStoryResponse = expectedUserStories.Select(x => x.ToUserStoryResponse());

            _userStoryServiceMock.Setup(x => x.GetUserStoriesAsync()).ReturnsAsync(expectedUserStories);
             
            // Act
            var result = (OkObjectResult)await _sut.GetAllAsync();

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<UserStoryResponse>>().Should().BeEquivalentTo(userStoryResponse);
        }

        [Test]
        public async Task GetAsync_ReturnOkAndObject_WhenUserExists()
        {
            // Arrange
            _userStoryServiceMock.Setup(x => x.GetUserStoryByIdAsync(_userStory.Id)).ReturnsAsync(_userStory);
            var userStoryResponse = _userStory.ToUserStoryResponse();

            // Act
            var result = (OkObjectResult)await _sut.GetAsync(_userStory.Id);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(userStoryResponse);
        }

        [Test]
        public async Task GetAsync_ReturnNotFound_WhenUserDoesNotExists()
        {
            // Arrange
            _userStoryServiceMock.Setup(x => x.GetUserStoryByIdAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

            // Act
            var result = async () => await _sut.GetAsync(Guid.NewGuid());

            // Assert
            await result.Should().ThrowAsync<KeyNotFoundException>().WithMessage("User Story cannot be found");
        }

        [Test]
        public async Task CreateAsync_ShouldCreateUserStory_WhenUserStoryIsValid()
        {
            // Arrange
            _userStoryServiceMock.Setup(x => x.CreateUserStoryAsync(_userStory)).ReturnsAsync(true);
            var userStoryRequest= _userStory.ToCreateUserStoryRequest();
            var expectedUserStoryResponse = _userStory.ToUserStoryResponse();

            // Act
            var result = (CreatedAtActionResult)await _sut.CreateAsync(userStoryRequest);

            // Assert
            result.StatusCode.Should().Be(201);
            result.Value.As<UserStoryResponse>().Should().BeEquivalentTo(expectedUserStoryResponse);
            result.RouteValues!["id"].Should().BeEquivalentTo(_userStory.Id);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateUserStory_WhenUserStoryIsValid()
        {
            // Arrange
            _userStoryServiceMock.Setup(x => x.UpdateUserStoryAsync(_userStory)).ReturnsAsync(true);
            var userStoryUpdateRequest = _userStory.ToUpdateUserStoryRequest();
            var expectedUserStoryResponse = _userStory.ToUserStoryResponse();

            // Act
            var result = (CreatedAtActionResult) await _sut.UpdateAsync(_userStory.Id, userStoryUpdateRequest);

            // Assert
            result.StatusCode.Should().Be(201);
            result.Value.As<UserStoryResponse>().Should().BeEquivalentTo(expectedUserStoryResponse);
            result.RouteValues!["id"].Should().BeEquivalentTo(_userStory.Id);
        }
    }
}
