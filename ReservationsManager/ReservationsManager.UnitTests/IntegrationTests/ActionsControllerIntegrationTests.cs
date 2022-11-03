using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ReservationsManager.Common;
using System.Net;
using Action = ReservationsManager.Domain.Action;

namespace ReservationsManager.Tests.IntegrationTests
{
    public class ActionsControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public ActionsControllerIntegrationTests()
        {
            var application = new WebApplicationFactory<Program>();
            _client = application.CreateClient();
        }

        [Fact]
        public async Task Get_AllActions_Returns_ListOfActions()
        {
            var request = "api/actions";
            int expectedCount = 6;

            var response = await _client.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseJason = await response.Content.ReadAsStringAsync();
                var resultActions = JsonConvert.DeserializeObject<List<Action>>(responseJason);

                Assert.Equal(expectedCount, resultActions.Count);
            }
           
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_ById_Returns_Action()
        {
            var expectedResult = new Action
            {
                Id = 2,
                Name = "Hard Gel"
            };
            var request = $"api/actions/{expectedResult.Id}";

            var response = await _client.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseJason = await response.Content.ReadAsStringAsync();
                var resultAction = JsonConvert.DeserializeObject<Action>(responseJason);

                Assert.Equal(expectedResult.Id, resultAction.Id);
                Assert.Equal(expectedResult.Name, resultAction.Name);
            }

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_ById_Returns_NotFound()
        {
            var request = "api/actions/21";
            var expectedLog = ActionControllerLogMessages.InexistentAction;

            var response = await _client.GetAsync(request);
            var resultLog = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Get_ById_WithNegativeId_Returns_BadRequest()
        {
            var request = "api/actions/-21";
            var expectedLog = ActionControllerLogMessages.NegetiveId;

            var result = await _client.GetAsync(request);
            var resultLog = await result.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Post_WithEmptyName_Returns_BadRequest()
        {
            var request = "api/actions";
            var expectedLog = ActionControllerLogMessages.EmptyName;

            var result = await _client.PostAsync(request, ContentHelper.GetStringContent(""));
            var resultLog = await result.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact(Skip = "Skip this test to not add a new record")]
        public async Task Post_WithValidData_Returns_Ok()
        {
            var request = "api/actions";
            var expectedLog = ActionControllerLogMessages.Created;
            var nameToCreate = "Test Name";

            var result = await _client.PostAsync(request, nameToCreate.GetStringContent());
            var resultLog = await result.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Put_WithNegativeId_Returns_BadRequest()
        {
            var actionToUpdate = new Action()
            {
                Id = -1,
                Name = "Test Name"
            };
            var request = $"api/actions/{actionToUpdate.Id}";
            var expectedLog = ActionControllerLogMessages.NegetiveId;

            var result = await _client.PutAsync(request, actionToUpdate.Name.GetStringContent());
            var resultLog = await result.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        [Fact]
        public async Task Put_WithEmptyName_Returns_BadRequest()
        {
            var actionToUpdate = new Action()
            {
                Id = 1,
                Name = ""
            };
            var request = $"api/actions/{actionToUpdate.Id}";
            var expectedLog = ActionControllerLogMessages.EmptyName;

            var result = await _client.PutAsync(request, actionToUpdate.Name.GetStringContent());
            var resultLog = await result.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedLog, resultLog);
        }

        //[Fact]
        //public async Task Put_WithInexistentId_Returns_BadRequest()
        //{
        //    var expectedLog = ActionControllerLogMessages.InexistentAction;

        //    var result = await _controller.Put(21, "some name") as ObjectResult;
        //    var resultLog = result.Value as string;

        //    Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        //    Assert.Equal(expectedLog, resultLog);
        //}

        //[Fact]
        //public async Task Put_WithValidData_Returns_Ok()
        //{
        //    var expectedLog = ActionControllerLogMessages.Updated;
        //    var expectedName = "Updated Name";
        //    int updateId = 1;

        //    var result = await _controller.Put(updateId, expectedName) as ObjectResult;
        //    var resultLog = result.Value as string;
        //    var updatedAction = await _repositoryMock.Object.GetByIdAsync(updateId);

        //    Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        //    Assert.Equal(expectedLog, resultLog);
        //    Assert.NotNull(updatedAction);
        //    Assert.Equal(expectedName, updatedAction.Name);
        //}

        //[Fact]
        //public async Task Delete_WithNegativeId_Returns_BadRequest()
        //{
        //    var expectedLog = ActionControllerLogMessages.NegetiveId;

        //    var result = await _controller.Delete(-1) as ObjectResult;
        //    var resultLog = result.Value as string;

        //    Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        //    Assert.Equal(expectedLog, resultLog);
        //}

        //[Fact]
        //public async Task Delete_WithInexistentId_Returns_BadRequest()
        //{
        //    var expectedLog = ActionControllerLogMessages.InexistentAction;

        //    var result = await _controller.Delete(21) as ObjectResult;
        //    var resultLog = result.Value as string;

        //    Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        //    Assert.Equal(expectedLog, resultLog);
        //}

        //[Fact]
        //public async Task Delete_WithValidData_Returns_Ok()
        //{
        //    var expectedLog = ActionControllerLogMessages.Removed;
        //    int deleteId = 1;

        //    var result = await _controller.Delete(deleteId) as ObjectResult;
        //    var resultLog = result.Value as string;

        //    Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        //    Assert.Equal(expectedLog, resultLog);
        //}
    }
}