using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;
using AcknowledgementShippingNotificationWatcher.Parsers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationParsers;

public class ParseProductTests
{
    private readonly ShippingNotificationParser _shippingNotificationParser;

    public ParseProductTests()
    {
        var logger = Substitute.For<ILogger<ShippingNotificationParser>>();
        _shippingNotificationParser = new ShippingNotificationParser(logger);
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
        
        var productDto = _shippingNotificationParser.ParseProduct(inputString);
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
        
        var productDto = _shippingNotificationParser.ParseProduct(inputString);
        productDto.ShouldBeEquivalentTo(expectedProductDto);
    }

    [Test]
    public void ParseProduct_TooManyProductLineParts_ShouldReturnNull()
    {
        const string inputString = "LINE P000001661                           9781473663800                     12      AB";
        var productDto = _shippingNotificationParser.ParseProduct(inputString);
        productDto.ShouldBeNull();
    }

    [Test]
    public void ParseProduct_TooFewProductLineParts_ShouldReturnNull()
    {
        const string inputString = "LINE P000001661                           9781473663800                  ";
        var productDto = _shippingNotificationParser.ParseProduct(inputString);
        productDto.ShouldBeNull();
    }
}