using System.Collections;
using AsnMonitor.Application.NotificationInputs;

namespace AsnMonitor.Application.Tests.ShippingNotificationConverters;

public class ConvertProductFailedResultTestCaseSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new TestCaseData(
            new ConvertProductFailedResultTestCase
            {
                ProductInput = null,
                ExpectedReason = "Failed to convert product line as ProductInput is null",
            }).SetName("Input is null");
        
        yield return new TestCaseData(
            new ConvertProductFailedResultTestCase
            {
                ProductInput = new ProductInput
                {
                    PoNumber = null,
                    Isbn = "9781473663800",
                    Quantity = 12
                },
                ExpectedReason = "Failed to convert product line as PoNumber is null",
            }).SetName("PO number is null");
        
        yield return new TestCaseData(
            new ConvertProductFailedResultTestCase
            {
                ProductInput = new ProductInput
                {
                    PoNumber = "P000001661",
                    Isbn = null,
                    Quantity = 12
                },
                ExpectedReason = "Failed to convert product line as Isbn is null",
            }).SetName("Isbn is null");
        
        yield return new TestCaseData(
            new ConvertProductFailedResultTestCase
            {
                ProductInput = new ProductInput
                {
                    PoNumber = "P000001661",
                    Isbn = "9781473663800",
                    Quantity = null
                },
                ExpectedReason = "Failed to convert product line as Quantity is null",
            }).SetName("Quantity is null");
    }
}