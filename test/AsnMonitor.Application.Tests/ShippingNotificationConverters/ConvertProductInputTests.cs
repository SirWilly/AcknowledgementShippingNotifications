using AsnMonitor.Application.Converters;
using AsnMonitor.Application.Converters.Results;
using AsnMonitor.Application.NotificationInputs;
using Shouldly;

namespace AsnMonitor.Application.Tests.ShippingNotificationConverters;

public class ConvertProductInputTests
{
    private readonly AsnConverter _asnConverter = new();

    [TestCaseSource(typeof(ConvertProductFailedResultTestCaseSource))]
    public void Convert_UnexpectedInput_ShouldReturnFailedResult(ConvertProductFailedResultTestCase testCase)
    {
        _asnConverter.Convert(testCase.ProductInput)
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
        _asnConverter.Convert(input).ShouldBeEquivalentTo(expectedResult);
    }
}