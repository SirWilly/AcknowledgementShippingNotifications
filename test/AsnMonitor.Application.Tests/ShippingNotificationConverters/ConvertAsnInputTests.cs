using AsnMonitor.Application.Converters;
using AsnMonitor.Application.Converters.Results;
using AsnMonitor.Application.NotificationInputs;
using Shouldly;

namespace AsnMonitor.Application.Tests.ShippingNotificationConverters;

public class ConvertAsnInputTests
{
    private readonly AsnConverter _asnConverter = new();

    [TestCaseSource(typeof(ConvertAsnFailedResultTestCaseSource))]
    public void Convert_UnexpectedInput_ShouldReturnFailedResult(ConvertAsnFailedResultTestCase testCase)
    {
        _asnConverter.Convert(testCase.Input)
            .ShouldBeOfType<AsnFailedConvertResult>()
            .Reason.ShouldBe(testCase.ExpectedReason);
    }

    [Test]
    public void Convert_ValidInput_ShouldReturnSuccessResult()
    {
        var input = new AsnInput
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
        var expectedResult = new AsnSuccessConvertResult
        {
            Asn = new Asn
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
        
        _asnConverter.Convert(input).ShouldBeEquivalentTo(expectedResult);
    }
}