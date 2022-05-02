using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Yahoo_Finance_Api.Handlers;
using Yahoo_Finance_Api.Helpers;

namespace Yahoo_Finance_Api.Tests;

public class CommandLineParserTests
{
    private readonly CommandLineParser _sut;
    private readonly Mock<IMediator> _mediator = new();
    private readonly Mock<IConsoleWriter> _consoleWriter = new();

    public CommandLineParserTests()
    {
        var types = new IBaseRequest[] { new StockHistoryRequest(), new StockSummaryRequest() };

        _sut=new(types, _mediator.Object, _consoleWriter.Object);
    }

    [Theory()]
    [InlineData("")]
    [InlineData("91345asf")]
    [InlineData("asdf asdf asdf")]
    public async Task Parser_ShouldFail_WhenInvalidInputAsync(string input)
    {
        // Arrange 
        var args = input.Split(' ');

        // Act
        await _sut.RunArgs(args);

        // Assert
        _consoleWriter.Verify(x => x.WriteLine(It.Is<string>(str => str.Contains("BadVerb"))));
        _mediator.Verify(x => x.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Never());
    }

    [Theory()]
    [InlineData("summary TSLA", "TSLA")]
    [InlineData("summary SONY", "SONY")]
    public async Task Parser_ShouldReceiveSummaryRequest_WhenValidInputAsync(string input, string symbol)
    {
        // Arrange 
        var args = input.Split(' ');

        // Act
        await _sut.RunArgs(args);

        // Assert
        _mediator.Verify(x => x.Send(It.Is<StockSummaryRequest>(r => r.Symbol == symbol), It.IsAny<CancellationToken>()), Times.Never());
    }

    [Theory()]
    [InlineData("history TSLA", "TSLA")]
    [InlineData("history SONY", "SONY")]
    public async Task Parser_ShouldReceiveHistoryRequest_WhenValidInputAsync(string input, string symbol)
    {
        // Arrange 
        var args = input.Split(' ');

        // Act
        await _sut.RunArgs(args);

        // Assert
        _mediator.Verify(x => x.Send(It.Is<StockSummaryRequest>(r => r.Symbol == symbol), It.IsAny<CancellationToken>()), Times.Never());
    }
}