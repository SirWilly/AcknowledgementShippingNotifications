using AsnMonitor.Application.Parsers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace AsnMonitor.Application.Tests.ShippingNotificationParsers;

public class ParseBoxHeaderTests
{
    private readonly AsnParser _asnParser;

    public ParseBoxHeaderTests()
    {
        var logger = Substitute.For<ILogger<AsnParser>>();
        _asnParser = new AsnParser(logger);
    }

    [Test]
    public void GetBoxHeader_ValidInputString_ShouldReturnBoxHeaderDto()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I                           

                                   LINE P000001661                           9781473663800                     12     

                                   """;
 
        var boxHeader = _asnParser.ParseBoxHeader(inputString);
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
        var boxHeader = _asnParser.ParseBoxHeader(inputString);
        boxHeader.ShouldBeNull();
    }

    [Test]
    public void GetBoxHeader_MissingHdrKeyword_ShouldReturnNullBoxHeaderDto()
    {
        const string inputString = """
                                   ADR  TRSP117                                                                                     6874453I                           

                                   """;
        var boxHeader = _asnParser.ParseBoxHeader(inputString);
        boxHeader.ShouldBeNull();
    }

    [Test]
    public void GetBoxHeader_TooManyHeaderParts_ShouldThrow()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I       abc                    

                                   """;
        _asnParser.ParseBoxHeader(inputString).ShouldBeNull();
    }
    
    [Test]
    public void GetBoxHeader_TooFewHeaderParts_ShouldThrow()
    {
        const string inputString = """
                                   HDR  TRSP117                      

                                   """;
        _asnParser.ParseBoxHeader(inputString).ShouldBeNull();
    }
}