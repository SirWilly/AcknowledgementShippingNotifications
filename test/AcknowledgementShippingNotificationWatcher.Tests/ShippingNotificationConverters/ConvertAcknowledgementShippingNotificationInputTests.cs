using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationConverters;

public class ConvertAcknowledgementShippingNotificationInputTests
{
    [TestCaseSource(typeof(ConvertAcknowledgementShippingNotificationFailedResultTestCaseSource))]
    public void Convert_UnexpectedInput_ShouldReturnFailedResult(ConvertAcknowledgementShippingNotificationFailedResultTestCase testCase)
    {
        AcknowledgementShippingNotificationConverter.Convert(testCase.Input)
            .ShouldBeOfType<AcknowledgementShippingNotificationFailedConvertResult>()
            .Reason.ShouldBe(testCase.ExpectedReason);
    }
}