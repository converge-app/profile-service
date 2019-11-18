
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Models.Entities;
using Application.Repositories;
using Application.Services;
using Application.Utility.ClientLibrary;
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
         public void CreateProfil_IsNullOrEmpty_ThrowsInvalidProfile()
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
            profileRepository.Setup(m => m.UserIdExists(It.IsAny<string>())).ReturnsAsync(false);
            profileRepository.Setup(m => m.Create(It.IsAny<Profile>())).ReturnsAsync(new Profile());

            var profileService = new ProfileService(profileRepository.Object, client.Object);

            // Act  
            var actual = profileService.CreateProfile(new Profile());
            // Assert
            Assert.NotNull(actual);
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