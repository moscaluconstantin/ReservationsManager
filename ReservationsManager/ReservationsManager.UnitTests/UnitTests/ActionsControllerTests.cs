using Microsoft.AspNetCore.Mvc;
using ReservationsManager.API.Controllers;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;
using ReservationsManager.DAL.Interfaces;
using System.Net;
using Action = ReservationsManager.Domain.Models.Action;

namespace ReservationsManager.Tests.UnitTests
{
    public class ActionsControllerIntegrationTests
    {
        private readonly ActionsController _controller;
        private readonly Mock<IActionsRepository> _repositoryMock;
        private readonly Mock<IActionEmployeesService> _serviceMock;

        public ActionsControllerIntegrationTests()
        {
            _repositoryMock = new Mock<IActionsRepository>();
            _serviceMock=new Mock<IActionEmployeesService>();


            List<Action> testActions = new List<Action>()
            {
                new Action()
                {
                    Id = 0,
                    Name = "First Test Manicure"
                },
                new Action()
                {
                    Id = 1,
                    Name = "Second Test Manicure"
                },
                new Action()
                {
                    Id = 2,
                    Name = "Third Test Manicure"
                }
            };

            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(testActions);
            _repositoryMock.Setup(x => x.GetByIdAsync(It.Is<int>(i => i == 1))).ReturnsAsync(testActions[1]);
            _repositoryMock.Setup(x => x.GetByIdAsync(It.Is<int>(i => i == 21))).ReturnsAsync((Action)null);

            _controller = new ActionsController(_repositoryMock.Object, _serviceMock.Object);
        }

        [Fact]
        public async Task Get_AllActions_Returns_ListOfActions()
        {
            int expectedCount = 3;

            var result = await _controller.Get() as ObjectResult;
            var actions = result.Value as List<Action>;

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedCount, actions.Count);
        }

        [Fact]
        public async Task Get_ById_Returns_Action()
        {
            var expectedResult = new Action
            {
                Id = 1,
                Name = "Second Test Manicure"
            };

            var result = await _controller.Get(expectedResult.Id) as ObjectResult;
            var resultAction = result.Value as Action;

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedResult.Id, resultAction.Id);
            Assert.Equal(expectedResult.Name, resultAction.Name);
        }

        [Fact]
        public async Task Get_ById_Returns_NotFound()
        {
            var expectedLog = ActionControllerLogMessages.InexistentAction;

            var result = await _controller.Get(21) as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Get_ById_WithNegativeId_Returns_BadRequest()
        {
            var expectedLog = ActionControllerLogMessages.NegetiveId;

            var result = await _controller.Get(-1) as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Post_WithEmptyName_Returns_BadRequest()
        {
            var expectedLog = ActionControllerLogMessages.EmptyName;

            var result = await _controller.Post("") as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Post_WithValidData_Returns_Ok()
        {
            var expectedLog = ActionControllerLogMessages.Created;

            var result = await _controller.Post("Valid Name") as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Put_WithNegativeId_Returns_BadRequest()
        {
            var expectedLog = ActionControllerLogMessages.NegetiveId;

            var result = await _controller.Put(-1, "some name") as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Put_WithEmptyName_Returns_BadRequest()
        {
            var expectedLog = ActionControllerLogMessages.EmptyName;

            var result = await _controller.Put(1, "") as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Put_WithInexistentId_Returns_BadRequest()
        {
            var expectedLog = ActionControllerLogMessages.InexistentAction;

            var result = await _controller.Put(21, "some name") as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Put_WithValidData_Returns_Ok()
        {
            var expectedLog = ActionControllerLogMessages.Updated;
            var expectedName = "Updated Name";
            int updateId = 1;

            var result = await _controller.Put(updateId, expectedName) as ObjectResult;
            var resultLog = result.Value as string;
            var updatedAction = await _repositoryMock.Object.GetByIdAsync(updateId);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
            Assert.NotNull(updatedAction);
            Assert.Equal(expectedName, updatedAction.Name);
        }

        [Fact]
        public async Task Delete_WithNegativeId_Returns_BadRequest()
        {
            var expectedLog = ActionControllerLogMessages.NegetiveId;

            var result = await _controller.Delete(-1) as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Delete_WithInexistentId_Returns_BadRequest()
        {
            var expectedLog = ActionControllerLogMessages.InexistentAction;

            var result = await _controller.Delete(21) as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Delete_WithValidData_Returns_Ok()
        {
            var expectedLog = ActionControllerLogMessages.Removed;
            int deleteId = 1;

            var result = await _controller.Delete(deleteId) as ObjectResult;
            var resultLog = result.Value as string;

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }
    }
}