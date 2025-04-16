using AsnMonitor.Application.NotificationInputs;
using AsnMonitor.Application.Parsers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace AsnMonitor.Application.Tests.ShippingNotificationParsers;

public class AsnParserTests
{
    private readonly AsnParser _asnParser;

    public AsnParserTests()
    {
        var logger = Substitute.For<ILogger<AsnParser>>();
        _asnParser = new AsnParser(logger);
    }

    [Test]
    public void Parse_SingleBoxString_ReturnsAsnInput()
    { 
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I                           

                                   LINE P000001661                           9781473663800                     12     

                                   LINE P000001661                           9781473667273                     2      

                                   LINE P000001661                           9781473665798                     1      

                                   """;
        var expectedOutput = new AsnInput
        {
            BoxHeader = new BoxHeaderInput
            {
                BoxId = "6874453I",
                SupplierId = "TRSP117",                
            },
            Contents = new List<ProductInput>
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
                },
                new()
                {
                    PoNumber = "P000001661",
                    Isbn = "9781473665798",
                    Quantity = 1
                }
            }
        };
        
        var asnInput = _asnParser.ParseBox(inputString);
        asnInput.ShouldBeEquivalentTo(expectedOutput);
    }

    [Test]
    public void Parse_AcceptanceInputFileText_ReturnsCollectionOfAsnInputs()
    {
        _asnParser.Parse(AcceptanceTestData.InputFileContents).Count.ShouldBe(8);
    }
}