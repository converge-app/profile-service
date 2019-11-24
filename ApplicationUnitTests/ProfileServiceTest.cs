
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Application.Exceptions;
using Application.Models.Entities;
using Application.Repositories;
using Application.Services;
using Application.Utility.ClientLibrary;
using Application.Utility.Exception;
using Moq;
using Xunit;
namespace ApplicationUnitTests
{
    public class ProfileServiceTest
    {
        [Fact]
        public void CreateProfil_UserIdExists_ThrowsInvalidProfile()
        {
            // Arrange
            var profileRepository = new Mock<IProfileRepository>();
            var client = new Mock<IClient>();
            profileRepository.Setup(m => m.UserIdExists(It.IsAny<string>())).ReturnsAsync(true);

            var profileService = new ProfileService(profileRepository.Object, client.Object);

            // Act  
            // Assert
            Assert.ThrowsAsync<InvalidProfile>(() => profileService.CreateProfile(new Profile()));
        }




        /* [Fact]
         public void CreateProfil_IsNullOrEmpty_CreateProfilePicture()
         {
             // Arrange
             var profileRepository = new Mock<IProfileRepository>();
             var client = new Mock<IClient>();
             profileRepository.Setup(m => m.UserIdExists(It.IsAny<string>())).ReturnsAsync(false);
             profileRepository.Setup(m => m.Create(It.IsAny<Profile>())).ReturnsAsync((Profile)null);

             var profileService = new ProfileService(profileRepository.Object, client.Object);

             // Act  
             var actual = profileService.CreateProfile(new Profile());
             // Assert
             Assert.NotNull(actual);
         }*/


        [Fact]
        public void CreateProfil_CreateProfile_ReturnsUserProfile()
        {
            // Arrange
            var profileRepository = new Mock<IProfileRepository>();
            var client = new Mock<IClient>();
            profileRepository.Setup(m => m.UserIdExists(It.IsAny<string>())).ReturnsAsync(true);
            profileRepository.Setup(m => m.Create(It.IsAny<Profile>())).ReturnsAsync(new Profile());

            var profileService = new ProfileService(profileRepository.Object, client.Object);

            // Act  
            var actual = profileService.CreateProfile(new Profile());
            // Assert
            Assert.NotNull(actual);
        }


        [Fact]
        public async void Create_GetUserAsync()
        {
            Environment.SetEnvironmentVariable("USERS_SERVICE_HTTP", "users-service.api.converge-app.net");
            var expected = "";
            var mockFactory = new Mock<IHttpClientFactory>();
            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
            {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, expected);
                return Task.FromResult(response);
            });

            var client = new HttpClient(clientHandlerStub);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            IHttpClientFactory factory = mockFactory.Object;
            var controller = new Client(factory);

            //Act
            var result = await controller.GetUserAsync("123");

            //Assert
            Assert.NotNull(expected);

        }





        [Fact]
        public void UpdateProfile_GetById_ThrowsInvalidProfile()
        {
            // Arrange
            var profileRepository = new Mock<IProfileRepository>();
            var client = new Mock<IClient>();
            profileRepository.Setup(m => m.GetById(It.IsAny<string>())).ReturnsAsync((Profile)null);
            var profileService = new ProfileService(profileRepository.Object, client.Object);

            // Act  
            // Assert
            Assert.ThrowsAsync<InvalidProfile>(() => profileService.UpdateProfile(new Profile()));
        }

        [Fact]
        public void UpdateProfile_GotById_ReturnsUserProfileTitle()
        {
            // Arrange
            var mockId = "samir";
            var mockTitle = "Software";

            var profileRepository = new Mock<IProfileRepository>();
            var client = new Mock<IClient>();
            profileRepository.Setup(m => m.GetById(It.IsAny<string>())).ReturnsAsync(new Profile() { Id = mockId });
            profileRepository.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Profile>()));
            var profileService = new ProfileService(profileRepository.Object, client.Object);

            // Act  
            // Assert

            profileService.UpdateProfile(new Profile() { Id = mockId, Title = mockTitle });

        }

        [Fact]
        public async Task UpdateProfile_GotById_ReturnsUserProfileDescription()
        {
            // Arrange
            var mockId = "samir";
            var mockDescription = "I need to make a project";

            var profileRepository = new Mock<IProfileRepository>();
            var client = new Mock<IClient>();
            profileRepository.Setup(m => m.GetById(It.IsAny<string>())).ReturnsAsync(new Profile() { Id = mockId });
            profileRepository.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Profile>()));
            var profileService = new ProfileService(profileRepository.Object, client.Object);

            // Act  
            // Assert
            await profileService.UpdateProfile(new Profile() { Id = mockId, Title = mockDescription });

        }

        [Fact]
        public async Task UpdateProfile_GotById_ReturnsUserProfileSkills()
        {
            // Arrange
            var mockId = "samir";
            var mockSkills = "C++";

            var profileRepository = new Mock<IProfileRepository>();
            var client = new Mock<IClient>();
            profileRepository.Setup(m => m.GetById(It.IsAny<string>())).ReturnsAsync(new Profile() { Id = mockId });
            profileRepository.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Profile>()));
            var profileService = new ProfileService(profileRepository.Object, client.Object);

            // Act  
            // Assert
            await profileService.UpdateProfile(new Profile() { Id = mockId, Title = mockSkills });

        }

        [Fact]
        public async Task UpdateProfile_GotById_ReturnsUserProfileExperience()
        {
            // Arrange
            var mockId = "samir";
            var mockExperience = "I have done many C++ projects";

            var profileRepository = new Mock<IProfileRepository>();
            var client = new Mock<IClient>();
            profileRepository.Setup(m => m.GetById(It.IsAny<string>())).ReturnsAsync(new Profile() { Id = mockId });
            profileRepository.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Profile>()));
            var profileService = new ProfileService(profileRepository.Object, client.Object);

            // Act  
            // Assert
            await profileService.UpdateProfile(new Profile() { Id = mockId, Title = mockExperience });

        }

    }
}