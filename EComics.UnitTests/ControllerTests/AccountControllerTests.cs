using eComics.Controllers;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using eComics.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EComics.UnitTests.ControllerTests
{
    public class AccountControllerTests
    {

        [Fact]
        public async Task Users_Returns_ViewResult_With_Users()
        {
            // Arrange
            var users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = "1", UserName = "user1@example.com" },
            new ApplicationUser { Id = "2", UserName = "user2@example.com" }
        }.AsQueryable();

            var userManagerMock = MockUserManager<ApplicationUser>();
            userManagerMock.Setup(x => x.Users).Returns(users);

            var controller = new AccountController(userManagerMock.Object, null, null);

            // Act
            var result = await controller.Users() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as IQueryable<ApplicationUser>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Login_Returns_ViewResult_With_LoginVM()
        {
            // Arrange
            var controller = new AccountController(null, null, null);

            // Act
            var result = controller.Login() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoginVM>(result.Model);
        }

        [Fact]
        public async Task Login_Returns_ViewResult_With_Model_When_ModelState_Invalid()
        {
            // Arrange
            var loginVM = new LoginVM();
            var controller = new AccountController(null, null, null);
            controller.ModelState.AddModelError("EmailAddress", "Email is required.");

            // Act
            var result = await controller.Login(loginVM) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoginVM>(result.Model);
        }

        [Fact]
        public async Task Login_Redirects_To_Books_Index_When_Credentials_Are_Valid()
        {
            // Arrange
            var loginVM = new LoginVM { EmailAddress = "test@example.com", Password = "password" };
            var user = new ApplicationUser { Email = "test@example.com" };
            var userManagerMock = MockUserManager<ApplicationUser>();
            userManagerMock.Setup(x => x.FindByEmailAsync(loginVM.EmailAddress)).ReturnsAsync(user);
            userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginVM.Password)).ReturnsAsync(true);

            var signInManagerMock = MockSignInManager<ApplicationUser>();

            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object, null);

            // Act
            var result = await controller.Login(loginVM) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Books", result.ControllerName);
        }

        [Fact]
        public async Task Login_Returns_ViewResult_With_Model_When_Credentials_Are_Invalid()
        {
            // Arrange
            var loginVM = new LoginVM { EmailAddress = "test@example.com", Password = "password" };
            var user = new ApplicationUser { Email = "test@example.com" };
            var userManagerMock = MockUserManager<ApplicationUser>();
            userManagerMock.Setup(x => x.FindByEmailAsync(loginVM.EmailAddress)).ReturnsAsync(user);
            userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginVM.Password)).ReturnsAsync(false);

            var signInManagerMock = MockSignInManager<ApplicationUser>();

            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object, null);

            // Act
            var result = await controller.Login(loginVM) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoginVM>(result.Model);
        }

        [Fact]
        public void Register_Returns_ViewResult_With_RegisterVM()
        {
            // Arrange
            var controller = new AccountController(null, null, null);

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.IsType<RegisterVM>(result.Model);
        }

        [Fact]
        public async Task Register_Returns_ViewResult_With_Model_When_ModelState_Invalid()
        {
            // Arrange
            var registerVM = new RegisterVM();
            var controller = new AccountController(null, null, null);
            controller.ModelState.AddModelError("EmailAddress", "Email is required.");

            // Act
            var result = await controller.Register(registerVM) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RegisterVM>(result.Model);
        }

        [Fact]
        public async Task Register_Returns_ViewResult_With_Model_When_Email_In_Use()
        {
            // Arrange
            var registerVM = new RegisterVM { EmailAddress = "test@example.com" };
            var user = new ApplicationUser { Email = "test@example.com" };
            var userManagerMock = MockUserManager<ApplicationUser>();
            userManagerMock.Setup(x => x.FindByEmailAsync(registerVM.EmailAddress)).ReturnsAsync(user);

            var controller = new AccountController(userManagerMock.Object, null, null);

            // Act
            var result = await controller.Register(registerVM) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("This email address is already in use!", controller.TempData["Error"]);
            Assert.IsType<RegisterVM>(result.Model);
        }

        [Fact]
        public async Task Register_Returns_ViewResult_With_RegisterCompleted_When_Successful()
        {
            // Arrange
            var registerVM = new RegisterVM
            {
                FullName = "John Doe",
                EmailAddress = "test@example.com",
                Password = "password"
            };
            var userManagerMock = MockUserManager<ApplicationUser>();
            userManagerMock.Setup(x => x.FindByEmailAsync(registerVM.EmailAddress)).ReturnsAsync((ApplicationUser)null);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerVM.Password))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new AccountController(userManagerMock.Object, null, null);

            // Act
            var result = await controller.Register(registerVM) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("RegisterCompleted", result.ViewName);
        }

        [Fact]
        public async Task Logout_Redirects_To_Books_Index()
        {
            // Arrange
            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>();
            var controller = new AccountController(null, signInManagerMock.Object, null);

            // Act
            var result = await controller.Logout() as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Books", result.ControllerName);
            signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once); // Verifies that SignOutAsync is called once
        }

        [Fact]
        public void AccessDenied_Returns_ViewResult()
        {
            // Arrange
            var controller = new AccountController(null, null, null);

            // Act
            var result = controller.AccessDenied() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }


        // Helper method to mock UserManager<ApplicationUser>
        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        // Helper method to mock SignInManager<ApplicationUser>
        private Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
        {
            var userManagerMock = MockUserManager<TUser>();
            return new Mock<SignInManager<TUser>>(userManagerMock.Object, null, null, null, null, null, null);
        }
    }
}
