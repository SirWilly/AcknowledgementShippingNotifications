using AcknowledgementShippingNotificationWatcher.Domain;
using AcknowledgementShippingNotificationWatcher.Parsers;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationParsers;

public class ShippingNotificationParserTests
{
    [Test]
    public void Parse_ValidInputString_ReturnsAcknowledgementShippingNotificationDto()
    { 
        const string inputString = """
                                   HDR  TRSP117                                                                                     6874453I                           

                                   LINE P000001661                           9781473663800                     12     

                                   LINE P000001661                           9781473667273                     2      

                                   LINE P000001661                           9781473665798                     1      

                                   """;
        var expectedOutput = new AcknowledgementShippingNotificationDto
        {
            BoxHeader = new BoxHeaderDto
            {
                BoxId = "6874453I",
                SupplierId = "TRSP117",                
            },
            Contents = new List<ProductDto>
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
        
        

        var shippingNotificationParser = new ShippingNotificationParser();
        var acknowledgementShippingNotificationDto = shippingNotificationParser.Parse(inputString);
        acknowledgementShippingNotificationDto.ShouldBeEquivalentTo(expectedOutput);
    }
}