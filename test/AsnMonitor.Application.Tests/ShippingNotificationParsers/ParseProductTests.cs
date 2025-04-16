using AsnMonitor.Application.NotificationInputs;
using AsnMonitor.Application.Parsers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace AsnMonitor.Application.Tests.ShippingNotificationParsers;

public class ParseProductTests
{
    private readonly AsnParser _asnParser;

    public ParseProductTests()
    {
        var logger = Substitute.For<ILogger<AsnParser>>();
        _asnParser = new AsnParser(logger);
    }
    
    [Test]
    public void ParseProduct_ValidInput_ShouldReturnProductDto()
    {
        const string inputString = "LINE P000001661                           9781473663800                     12   ";
        var expectedProductDto = new ProductInput
        {
            PoNumber = "P000001661",
            Isbn = "9781473663800",
            Quantity = 12
        };
        
        var productDto = _asnParser.ParseProduct(inputString);
        productDto.ShouldBeEquivalentTo(expectedProductDto);
    }

    [Test]
    public void ParseProduct_UnexpectedQuantityValue_ShouldSetQuantityToNull()
    {
        const string inputString = "LINE P000001661                           9781473663800                     AB   ";
        var expectedProductDto = new ProductInput
        {
            PoNumber = "P000001661",
            Isbn = "9781473663800",
            Quantity = null
        };
        
        var productDto = _asnParser.ParseProduct(inputString);
        productDto.ShouldBeEquivalentTo(expectedProductDto);
    }

    [Test]
    public void ParseProduct_TooManyProductLineParts_ShouldReturnNull()
    {
        const string inputString = "LINE P000001661                           9781473663800                     12      AB";
        var productDto = _asnParser.ParseProduct(inputString);
        productDto.ShouldBeNull();
    }

    [Test]
    public void ParseProduct_TooFewProductLineParts_ShouldReturnNull()
    {
        const string inputString = "LINE P000001661                           9781473663800                  ";
        var productDto = _asnParser.ParseProduct(inputString);
        productDto.ShouldBeNull();
    }
}