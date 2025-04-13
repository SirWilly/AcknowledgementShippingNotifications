using AcknowledgementShippingNotificationWatcher.Parsers;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationParsers;

public class GetBoxHeaderDtoTests
{
    [Test]
    public void GetBoxHeader_ValidInputString_ShouldReturnBoxHeaderDto()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I                           

                                   LINE P000001661                           9781473663800                     12     

                                   """;
 
        var boxHeader = inputString.GetBoxHeader();
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
        var boxHeader = inputString.GetBoxHeader();
        boxHeader.ShouldBeNull();
    }

    [Test]
    public void GetBoxHeader_MissingHdrKeyword_ShouldReturnNullBoxHeaderDto()
    {
        const string inputString = """
                                   ADR  TRSP117                                                                                     6874453I                           

                                   """;
        var boxHeader = inputString.GetBoxHeader();
        boxHeader.ShouldBeNull();
    }

    [Test]
    public void GetBoxHeader_TooManyHeaderParts_ShouldThrow()
    {
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I       abc                    

                                   """;
        Should.Throw<Exception>(() => inputString.GetBoxHeader()).Message.ShouldStartWith("Invalid box header line:");
    }
    
    [Test]
    public void GetBoxHeader_TooFewHeaderParts_ShouldThrow()
    {
        const string inputString = """
                                   HDR  TRSP117                      

                                   """;
        Should.Throw<Exception>(() => inputString.GetBoxHeader()).Message.ShouldStartWith("Invalid box header line:");
    }
}