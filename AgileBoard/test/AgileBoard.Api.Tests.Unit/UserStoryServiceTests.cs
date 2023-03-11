﻿using AgileBoard.Core.Domain;
using AgileBoard.Core.Logging;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.Services;
using AgileBoard.Core.ValueObjects;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AgileBoard.Api.Tests.Unit
{
    public class UserStoryServiceTests
    {
        #region Arrange

        private readonly UserStoryService _sut;
        private readonly Mock<IUserStoryRepository> _userStoryRepositoryMock = new();
        private readonly Mock<ILoggerAdapter<UserStoryService>> _loggerMock = new();

        public UserStoryServiceTests()
        {
            _sut = new UserStoryService(_userStoryRepositoryMock.Object, _loggerMock.Object);
        }

        #endregion

        [Test]
        public async Task GetUserStoriesAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            _userStoryRepositoryMock.Setup(x => x.GetUserStoriesAsync())
                .ReturnsAsync(Array.Empty<UserStory>());

            // Act
            var result = await _sut.GetUserStoriesAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetUserStoriesAsync_ShouldReturnAListOfUserStories_WhenUserStoriesExist()
        {
            // Arrange
            var deadline = Date.Now.AddDays(3);
            var created = Date.Now;

            var expectedUserStories = new[]
            {
                new UserStory
                (
                    UserStoryId.Create(),
                    "Test User Story Title",
                    "Test Owner XYZ",
                    "Test User Story Description",
                    "Test AC",
                    "Test DoD",
                    "Test DoR",
                    null,
                    5,
                    new Priority(2),
                    Risk.Medium(),
                    deadline,
                    created,
                    created
                )
            };

            _userStoryRepositoryMock.Setup(x => x.GetUserStoriesAsync())
                .ReturnsAsync(expectedUserStories);


            // Act
            var userStories = await _sut.GetUserStoriesAsync();

            // Assert
            userStories.Should().ContainSingle(x => x.Title == "Test User Story Title");
            userStories.Should().ContainSingle(x => x.Owner == "Test Owner XYZ");
            userStories.Should().ContainSingle(x => x.Description == "Test User Story Description");
            userStories.Should().ContainSingle(x => x.AcceptanceCriteria == "Test AC");
            userStories.Should().ContainSingle(x => x.DefinitionOfReady == "Test DoR");
            userStories.Should().ContainSingle(x => x.DefinitionOfDone == "Test DoD");
            userStories.Should().ContainSingle(x => x.Comment == null);
            userStories.Should().ContainSingle(x => x.StoryPoints == new StoryPoints(5));
            userStories.Should().ContainSingle(x => x.Priority == new Priority(2));
            userStories.Should().ContainSingle(x => x.Risk == Risk.Medium());
            userStories.Should().ContainSingle(x => x.Deadline!.Equals(Date.Now.AddDays(3)));
            userStories.Should().ContainSingle(x => x.Created.Equals(Date.Now));
            userStories.Should().ContainSingle(x => x.Updated.Equals(Date.Now));
        }

        [Test]
        public async Task GetUserStoriesAsync_ShouldLogMessages_WhenInvoked()
        {
            // Arrange
            _userStoryRepositoryMock.Setup(x => x.GetUserStoriesAsync())
                .ReturnsAsync(Array.Empty<UserStory>());

            // Act
            await _sut.GetUserStoriesAsync();

            // Assert
            _loggerMock.Verify(x => x.LogInformation(It.Is<string>(s => s.Equals("Retrieving all user stories"))), Times.AtLeastOnce);
        }

        [Test]
        public async Task GetUserStoriesAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var dbException = new ExternalException("Something went wrong", 500);

            _userStoryRepositoryMock.Setup(x => x.GetUserStoriesAsync())
                .ThrowsAsync(dbException);

            // Act
            var response = async () => await _sut.GetUserStoriesAsync();

            // Assert
            await response.Should().ThrowAsync<ExternalException>().WithMessage("Something went wrong");
            _loggerMock.Verify(x => x.LogError(It.Is<ExternalException>(s => s.Equals(dbException)), "Something went wrong while retrieving all user stories"), Times.Once);
        }

        [Test]
        public async Task GetUserStoryByIdAsync_ShouldNotReturnUserStory_WhenNoUserStoryExist()
        {
            // Arrange
            _userStoryRepositoryMock.Setup(x => x.GetUserStoryByIdAsync(It.IsAny<UserStoryId>()))
                .ReturnsAsync(null as UserStory);

            // Act
            var result = await _sut.GetUserStoryByIdAsync(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task GetUserStoryByIdAsync_ShouldExpectedReturnUserStory_WhenUserStoryExist()
        {
            // Arrange
            var existingUserStory = new UserStory
            (
                Guid.NewGuid(),
                "Test User Story Title",
                "Test Owner XYZ",
                "Test User Story Description",
                "Test AC",
                "Test DoR",
                "Test DoD",
                null,
                5,
                new Priority(2),
                Risk.Medium(),
                Date.Now.AddDays(3),
                Date.Now,
                null
            );

            _userStoryRepositoryMock.Setup(x => x.GetUserStoryByIdAsync(existingUserStory.Id))
                .ReturnsAsync(existingUserStory);


            // Act
            var userStory = await _sut.GetUserStoryByIdAsync(existingUserStory.Id);

            // Assert
            userStory.Should().BeEquivalentTo(existingUserStory);
        }

        [Test]
        public async Task GetUserStoryByIdAsync_ShouldLogMessages_WhenInvoked()
        {
            // Arrange
            var userStoryId = UserStoryId.Create();
            _userStoryRepositoryMock.Setup(x => x.GetUserStoryByIdAsync(userStoryId))
                .ReturnsAsync(null as UserStory);

            // Act
            await _sut.GetUserStoryByIdAsync(userStoryId);

            // Assert
            _loggerMock.Verify(x => x.LogInformation("Retrieving user with id: {0}", userStoryId), Times.Once);
        }

        [Test]
        public async Task GetUserStoryByIdAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var userStoryId = UserStoryId.Create();
            var dbException = new ExternalException("Something went wrong", 500);

            _userStoryRepositoryMock.Setup(x => x.GetUserStoryByIdAsync(userStoryId))
                .ThrowsAsync(dbException);

            // Act
            var requestAction = async () => await _sut.GetUserStoryByIdAsync(userStoryId);

            // Assert
            await requestAction.Should().ThrowAsync<ExternalException>().WithMessage("Something went wrong");
            _loggerMock.Verify(x => x.LogError(dbException, "Something went wrong while retrieving user story with id {0}", userStoryId), Times.Once);
        }

        [Test]
        public async Task CreateUserStoryAsync_ShouldCreateUser_WhenModelIsValid()
        {
            // Arrange
            var userStoryToCreate = new UserStory
            (
             Guid.NewGuid(),
             "Test User Story Title",
             "Test Owner XYZ",
             "Test User Story Description",
             "Test AC",
             "Test DoR",
             "Test DoD",
             null,
             5,
             new Priority(2),
             Risk.Medium(),
             Date.Now.AddDays(3),
             Date.Now,
             null
            );

            _userStoryRepositoryMock.Setup(x => x.CreateUserStoryAsync(userStoryToCreate))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.CreateUserStoryAsync(userStoryToCreate);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task CreateUserStoryAsync_ShouldLogMessages_WhenInvoked()
        {
            // Arrange
            var userStoryToCreate = new UserStory
            (
                Guid.NewGuid(),
                "Test User Story Title",
                "Test Owner XYZ",
                "Test User Story Description",
                "Test AC",
                "Test DoR",
                "Test DoD",
                null,
                5,
                new Priority(2),
                Risk.Medium(),
                Date.Now.AddDays(3),
                Date.Now,
                null
            );

            _userStoryRepositoryMock.Setup(x => x.CreateUserStoryAsync(userStoryToCreate))
                .ReturnsAsync(true);

            // Act
            await _sut.CreateUserStoryAsync(userStoryToCreate);

            // Assert
            _loggerMock.Verify(x => x.LogInformation("Creating user story with id {0} and title: {1}", userStoryToCreate.Id, userStoryToCreate.Title), Times.Once);
        }

        [Test]
        public async Task CreateUserStoryAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var dbException = new ExternalException("Something went wrong", 500);

            var userStoryToCreate = new UserStory
            (
                Guid.NewGuid(),
                "Test User Story Title",
                "Test Owner XYZ",
                "Test User Story Description",
                "Test AC",
                "Test DoR",
                "Test DoD",
                null,
                5,
                new Priority(2),
                Risk.Medium(),
                Date.Now.AddDays(3),
                Date.Now,
                null
            );

            _userStoryRepositoryMock.Setup(x => x.CreateUserStoryAsync(userStoryToCreate))
                .ThrowsAsync(dbException);

            // Act
            var result = async () => await _sut.CreateUserStoryAsync(userStoryToCreate);

            // Assert
            await result.Should().ThrowAsync<ExternalException>().WithMessage("Something went wrong");
            _loggerMock.Verify(x => x.LogError(dbException, "Something went wrong while creating a user story"), Times.Once);
        }

        [Test]
        public async Task DeleteUserStoryAsync_ShouldDeleteUserStory_WhenExists()
        {
            // Arrange
            var userStoryId = Guid.NewGuid();

            _userStoryRepositoryMock.Setup(x => x.DeleteUserStoryAsync(userStoryId))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.DeleteUserStoryAsync(userStoryId);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task DeleteUserStoryAsync_ShouldNotDeleteUserStory_WhenNotExist()
        {
            // Arrange
            var userStoryId = Guid.NewGuid();

            _userStoryRepositoryMock.Setup(x => x.DeleteUserStoryAsync(userStoryId))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.DeleteUserStoryAsync(userStoryId);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task DeleteUserStoryAsync_ShouldLogMessage_WhenInvoked()
        {
            // Arrange
            var userStoryId = UserStoryId.Create();

            _userStoryRepositoryMock.Setup(x => x.DeleteUserStoryAsync(userStoryId))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.DeleteUserStoryAsync(userStoryId);

            // Assert
            _loggerMock.Verify(x => x.LogInformation("Deleting user with id: {0}", userStoryId), Times.Once);
        }

        [Test]
        public async Task DeleteUserStoryAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var userStoryId = UserStoryId.Create();

            var dbException = new ExternalException("Something went wrong", 500);

            _userStoryRepositoryMock.Setup(x => x.DeleteUserStoryAsync(userStoryId))
                .ThrowsAsync(dbException);

            // Act
            var result = async () => await _sut.DeleteUserStoryAsync(userStoryId);

            // Assert
            await result.Should().ThrowAsync<ExternalException>().WithMessage("Something went wrong");
            _loggerMock.Verify(x => x.LogError(dbException, "Something went wrong while deleting user story with id {0}", userStoryId), Times.Once);
        }

        [Test]
        public async Task UpdateUserStoryAsync_ShouldUpdateUserStory_WhenExists()
        {
            // Arrange
            var userStoryToUpdate = new UserStory
            (
                Guid.NewGuid(),
                "Test User Story Title UPDATED",
                "Test Owner XYZ UPDATED",
                "Test User Story Description UPDATED",
                "Test AC UPDATED",
                "Test DoR UPDATED",
                "Test DoD UPDATED",
                null,
                5,
                new Priority(2),
                Risk.Medium(),
                Date.Now.AddDays(3),
                Date.Now,
                null
            );

            _userStoryRepositoryMock.Setup(x => x.UpdateUserStoryAsync(userStoryToUpdate))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.UpdateUserStoryAsync(userStoryToUpdate);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task UpdateUserStoryAsync_ShouldNotUpdateUserStory_WhenNotExist()
        {
            // Arrange
            var userStoryToUpdate = new UserStory
            (
                Guid.NewGuid(),
                "Test User Story Title UPDATED",
                "Test Owner XYZ UPDATED",
                "Test User Story Description UPDATED",
                "Test AC UPDATED",
                "Test DoR UPDATED",
                "Test DoD UPDATED",
                null,
                5,
                new Priority(2),
                Risk.Medium(),
                Date.Now.AddDays(3),
                Date.Now,
                null
            );

            _userStoryRepositoryMock.Setup(x => x.UpdateUserStoryAsync(userStoryToUpdate))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.UpdateUserStoryAsync(userStoryToUpdate);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task UpdateUserStoryAsync_ShouldLogMessage_WhenInvoked()
        {
            // Arrange
            var userStoryToUpdate = new UserStory
            (
                Guid.NewGuid(),
                "Test User Story Title UPDATED",
                "Test Owner XYZ UPDATED",
                "Test User Story Description UPDATED",
                "Test AC UPDATED",
                "Test DoR UPDATED",
                "Test DoD UPDATED",
                null,
                5,
                new Priority(2),
                Risk.Medium(),
                Date.Now.AddDays(3),
                Date.Now,
                null
            );

            _userStoryRepositoryMock.Setup(x => x.UpdateUserStoryAsync(userStoryToUpdate))
                .ReturnsAsync(true);

            // Act
            await _sut.UpdateUserStoryAsync(userStoryToUpdate);

            // Assert
            _loggerMock.Verify(x => x.LogInformation("Update user with id: {0}", userStoryToUpdate.Id), Times.Once);
        }

        [Test]
        public async Task PatchUpdateUserStoryAsync_ShouldPartiallyUpdateUserStory_WhenExists()
        {
            // Arrange
            var userStoryId = Guid.NewGuid();
            var operationStrings = "[ { \"op\": \"replace\", \"path\": \"/Title\", \"value\": \"NEW TITLE\" } ] ";
            var ops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Operation>>(operationStrings);

            var patchUpdate = new JsonPatchDocument(ops, new Newtonsoft.Json.Serialization.DefaultContractResolver());

            _userStoryRepositoryMock.Setup(x => x.PatchUpdateUserStoryAsync(userStoryId, patchUpdate))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.PatchUpdateUserStoryAsync(userStoryId, patchUpdate);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task PatchUpdateUserStoryAsync_ShouldNotPartiallyUpdateUserStory_WhenNotExist()
        {
            // Arrange
            var userStoryId = Guid.NewGuid();
            var operationStrings = "[ { \"op\": \"replace\", \"path\": \"/Title\", \"value\": \"NEW TITLE\" } ] ";
            var ops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Operation>>(operationStrings);

            var patchUpdate = new JsonPatchDocument(ops, new Newtonsoft.Json.Serialization.DefaultContractResolver());

            _userStoryRepositoryMock.Setup(x => x.PatchUpdateUserStoryAsync(userStoryId, patchUpdate))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.PatchUpdateUserStoryAsync(userStoryId, patchUpdate);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task PatchUpdateUserStoryAsync_ShouldLogMessage_WhenInvoked()
        {
            // Arrange
            var userStoryId = UserStoryId.Create();
            var operationStrings = "[ { \"op\": \"replace\", \"path\": \"/Title\", \"value\": \"NEW TITLE\" } ] ";
            var ops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Operation>>(operationStrings);

            var patchUpdate = new JsonPatchDocument(ops, new Newtonsoft.Json.Serialization.DefaultContractResolver());

            _userStoryRepositoryMock.Setup(x => x.PatchUpdateUserStoryAsync(userStoryId, patchUpdate))
                .ReturnsAsync(false);

            // Act
            await _sut.PatchUpdateUserStoryAsync(userStoryId, patchUpdate);

            // Assert
            _loggerMock.Verify(x => x.LogInformation("Patch update user with id: {0}", userStoryId), Times.Once);
        }

        [Test]
        public async Task PatchUpdateUserStoryAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var userStoryId = UserStoryId.Create();
            var operationStrings = "[ { \"op\": \"replace\", \"path\": \"/Title\", \"value\": \"NEW TITLE\" } ] ";
            var ops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Operation>>(operationStrings);

            var patchUpdate = new JsonPatchDocument(ops, new Newtonsoft.Json.Serialization.DefaultContractResolver());

            var dbException = new ExternalException("Something went wrong", 500);

            _userStoryRepositoryMock.Setup(x => x.PatchUpdateUserStoryAsync(userStoryId, patchUpdate))
                .ThrowsAsync(dbException);

            // Act
            var result = async () => await _sut.PatchUpdateUserStoryAsync(userStoryId, patchUpdate);

            // Assert
            await result.Should().ThrowAsync<ExternalException>().WithMessage("Something went wrong");
            _loggerMock.Verify(x => x.LogError(dbException, "Something went wrong while patch update user story with id {0}", userStoryId), Times.Once);
        }
    }
}
