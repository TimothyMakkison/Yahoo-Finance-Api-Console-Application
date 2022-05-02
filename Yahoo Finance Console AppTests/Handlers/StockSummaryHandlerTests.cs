using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Newtonsoft.Json;
using Refit;
using RichardSzalay.MockHttp;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Yahoo_Finance_Api.Apis;
using Yahoo_Finance_Api.Errors;
using Yahoo_Finance_Api.Helpers;
using Yahoo_Finance_Api.Models.Responses;
using Yahoo_Finance_Api.Models.Results;

namespace Yahoo_Finance_Api.Handlers.Tests;

public class StockSummaryHandlerTests
{
    private readonly StockSummaryHandler _sut;
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IConsoleWriter> _console = new();

    private readonly IStockApi _stockApi;
    private readonly MockHttpMessageHandler _server;

    public StockSummaryHandlerTests()
    {
        _server = new MockHttpMessageHandler();
        var client = _server.ToHttpClient();
        client.BaseAddress =new System.Uri("http://localhost");

        _stockApi = RestService.For<IStockApi>(client);

        _sut=new(_stockApi, _mapper.Object, _console.Object);
    }

    [Theory, AutoData]
    public async Task SummaryHandler_ShouldWritePrices_WhenValidRequestAsync(StockSummaryRequest request, CancellationToken token, StockSummaryResponse response, StockSummaryResult result)
    {
        // Arrange
        var ser = JsonConvert.SerializeObject(response);
        _server.When("http://localhost/stock/v2/get-summary*").Respond("application/json", ser);
        _mapper.Setup(m => m.Map<StockSummaryResult>(It.IsAny<StockSummaryResponse>())).Returns(result);

        // Act
        await _sut.Handle(request, token);

        // Assert
        _mapper.Verify(m => m.Map<StockSummaryResult>(It.IsAny<StockSummaryResponse>()));
    }

    [Theory, AutoData]
    public async Task SummaryHandler_ShouldThrow_WhenInvalidRequestAsync(StockSummaryRequest request, CancellationToken token)
    {
        // Arrange
        _server.When("http://localhost/stock/v2/get-summary*").Respond(statusCode: System.Net.HttpStatusCode.BadRequest);

        // Act Assert
        await Assert.ThrowsAnyAsync<NotSuccessException>(async () => await _sut.Handle(request, token));
    }
}
