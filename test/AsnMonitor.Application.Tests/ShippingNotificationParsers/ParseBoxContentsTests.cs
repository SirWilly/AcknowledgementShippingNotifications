using AsnMonitor.Application.NotificationInputs;
using AsnMonitor.Application.Parsers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace AsnMonitor.Application.Tests.ShippingNotificationParsers;

public class ParseBoxContentsTests
{
    private readonly AsnParser _asnParser;

    public ParseBoxContentsTests()
    {
        var logger = Substitute.For<ILogger<AsnParser>>();
        _asnParser = new AsnParser(logger);
    }
    
    [Test]
    public void ParseBoxContents_NoProductItemLinesInInput_ReturnsEmptyCollection()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I                           

                                   """;
        
        var contents = _asnParser.ParseBoxContents(inputString);
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
        var contents = _asnParser.ParseBoxContents(inputString);
        contents.ShouldBe(expectedContents);
    }
}