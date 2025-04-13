using AcknowledgementShippingNotificationWatcher.Parsers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationParsers;

public class ParseBoxHeaderTests
{
    private readonly ShippingNotificationParser _shippingNotificationParser;

    public ParseBoxHeaderTests()
    {
        var logger = Substitute.For<ILogger<ShippingNotificationParser>>();
        _shippingNotificationParser = new ShippingNotificationParser(logger);
    }

    [Test]
    public void GetBoxHeader_ValidInputString_ShouldReturnBoxHeaderDto()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I                           

                                   LINE P000001661                           9781473663800                     12     

                                   """;
 
        var boxHeader = _shippingNotificationParser.ParseBoxHeader(inputString);
        using (Assert.EnterMultipleScope())
        {
            boxHeader?.BoxId.ShouldBe("6874453I");
            boxHeader?.SupplierId.ShouldBe("TRSP117");
        }
    }

    [Test]
    public void GetBoxHeader_EmptyInputString_ShouldReturnNullBoxHeaderDto()
    {
        const string inputString = "    ";
        var boxHeader = _shippingNotificationParser.ParseBoxHeader(inputString);
        boxHeader.ShouldBeNull();
    }

    [Test]
    public void GetBoxHeader_MissingHdrKeyword_ShouldReturnNullBoxHeaderDto()
    {
        const string inputString = """
                                   ADR  TRSP117                                                                                     6874453I                           

                                   """;
        var boxHeader = _shippingNotificationParser.ParseBoxHeader(inputString);
        boxHeader.ShouldBeNull();
    }

    [Test]
    public void GetBoxHeader_TooManyHeaderParts_ShouldThrow()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I       abc                    

                                   """;
        _shippingNotificationParser.ParseBoxHeader(inputString).ShouldBeNull();
    }
    
    [Test]
    public void GetBoxHeader_TooFewHeaderParts_ShouldThrow()
    {
        const string inputString = """
                                   HDR  TRSP117                      

                                   """;
        _shippingNotificationParser.ParseBoxHeader(inputString).ShouldBeNull();
    }
}