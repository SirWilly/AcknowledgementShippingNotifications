using AcknowledgementShippingNotificationWatcher.Domain;
using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationConverters;

public class ConvertAcknowledgementShippingNotificationInputTests
{
    private readonly AcknowledgementShippingNotificationConverter _acknowledgementShippingNotificationConverter = new();

    [TestCaseSource(typeof(ConvertAcknowledgementShippingNotificationFailedResultTestCaseSource))]
    public void Convert_UnexpectedInput_ShouldReturnFailedResult(ConvertAcknowledgementShippingNotificationFailedResultTestCase testCase)
    {
        _acknowledgementShippingNotificationConverter.Convert(testCase.Input)
            .ShouldBeOfType<AcknowledgementShippingNotificationFailedConvertResult>()
            .Reason.ShouldBe(testCase.ExpectedReason);
    }

    [Test]
    public void Convert_ValidInput_ShouldReturnSuccessResult()
    {
        var input = new AcknowledgementShippingNotificationInput
        {
            BoxHeader = new BoxHeaderInput
            {
                BoxId = "6874453I",
                SupplierId = "TRSP117"
            },
            Contents = new List<ProductInput?>
            {
                new()
                {
                    PoNumber = "P000001661",
                    Isbn = "9781473663800",
                    Quantity = 12
                }
            }
        };
        var expectedResult = new AcknowledgementShippingNotificationSuccessConvertResult
        {
            AcknowledgementShippingNotification = new AcknowledgementShippingNotification
            {
                BoxId = "6874453I",
                SupplierId = "TRSP117",
                Contents = new List<Product>
                {
                    new()
                    {
                        PoNumber = "P000001661",
                        Isbn = "9781473663800",
                        Quantity = 12
                    }
                }
            }
        };
        
        _acknowledgementShippingNotificationConverter.Convert(input).ShouldBeEquivalentTo(expectedResult);
    }
}