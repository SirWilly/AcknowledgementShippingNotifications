using AcknowledgementShippingNotificationWatcher.Domain;
using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationConverters;

public class ConvertProductInputTests
{
    [TestCaseSource(typeof(ConvertProductFailedResultTestCaseSource))]
    public void Convert_UnexpectedInput_ShouldReturnFailedResult(ConvertProductFailedResultTestCase testCase)
    {
        AcknowledgementShippingNotificationConverter.Convert(testCase.ProductInput)
            .ShouldBeOfType<ProductConvertFailureResult>()
            .Reason.ShouldBe(testCase.ExpectedReason);
    }

    [Test]
    public void Convert_ValidInput_ShouldReturnSuccessResult()
    {
        var input = new ProductInput
        {
            PoNumber = "P000001661",
            Isbn = "9781473663800",
            Quantity = 12
        };
        var expectedResult = new ProductConvertSuccessResult
        {
            Product = new Product
            {
                PoNumber = "P000001661",
                Isbn = "9781473663800",
                Quantity = 12
            }
        };
        AcknowledgementShippingNotificationConverter.Convert(input).ShouldBeEquivalentTo(expectedResult);
    }
}