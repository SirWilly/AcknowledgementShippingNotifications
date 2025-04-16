using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;
using AcknowledgementShippingNotificationWatcher.Parsers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace AsnMonitor.Application.Tests.ShippingNotificationParsers;

public class ParseBoxContentsTests
{
    private readonly ShippingNotificationParser _shippingNotificationParser;

    public ParseBoxContentsTests()
    {
        var logger = Substitute.For<ILogger<ShippingNotificationParser>>();
        _shippingNotificationParser = new ShippingNotificationParser(logger);
    }
    
    [Test]
    public void ParseBoxContents_NoProductItemLinesInInput_ReturnsEmptyCollection()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I                           

                                   """;
        
        var contents = _shippingNotificationParser.ParseBoxContents(inputString);
        contents.ShouldBeEmpty();
    }

    [Test]
    public void ParseBoxContents_MultipleProductItemLinesInInput_ReturnsExpectedContents()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I                           
                                   
                                   LINE P000001661                           9781473663800                     12     
                                   
                                   LINE P000001661                           9781473667273                     2      
                                   
                                   """;
        var expectedContents = new List<ProductInput?>
        {
            new()
            {
                PoNumber = "P000001661",
                Isbn = "9781473663800",
                Quantity = 12
            },
            new()
            {
                PoNumber = "P000001661",
                Isbn = "9781473667273",
                Quantity = 2
            }
        };
        var contents = _shippingNotificationParser.ParseBoxContents(inputString);
        contents.ShouldBe(expectedContents);
    }
}