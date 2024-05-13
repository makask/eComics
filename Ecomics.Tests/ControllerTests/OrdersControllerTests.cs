using eComics.Controllers;
using eComics.Data.Cart;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using eComics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace EComics.UnitTests.ControllerTests
{
    public class OrdersControllerTests
    {
        [Fact]
        public async Task Index_Returns_ViewResult_With_Orders()
        {
            // Arrange
            var userId = "user1";
            var userRole = "Customer";

            var orders = new List<Order>
        {
            new Order { Id = 1, UserId = userId, Email = "test1@example.com", /* other properties */ },
            new Order { Id = 2, UserId = userId, Email = "test2@example.com", /* other properties */ }
        };

            var ordersServiceMock = new Mock<IOrdersService>();
            ordersServiceMock.Setup(x => x.GetOrdersByUserIdAndRoleAsync(userId, userRole)).ReturnsAsync(orders);

            var controller = new OrdersController(null, null, ordersServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, userRole)
                    }, "mock"))
                }
            };

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as List<Order>;
            Assert.NotNull(model);
            Assert.Equal(orders.Count, model.Count);
        }

       
    }
}
