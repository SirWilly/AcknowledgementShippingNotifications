using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationConverters;

public class ConvertAcknowledgementShippingNotificationInputTests
{
    [TestCaseSource(typeof(ConvertAcknowledgementShippingNotificationFailedResultTestCaseSource))]
    public void Convert_UnexpectedInput_ShouldReturnFailedResult(ConvertAcknowledgementShippingNotificationFailedResultTestCase testCase)
    {
        var converter = new AcknowledgementShippingNotificationConverter();
        converter.Convert(testCase.Input)
            .ShouldBeOfType<AcknowledgementShippingNotificationFailedConvertResult>()
            .Reason.ShouldBe(testCase.ExpectedReason);
    }
}