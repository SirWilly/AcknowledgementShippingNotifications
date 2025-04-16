using System.Collections;
using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;

namespace AsnMonitor.Application.Tests.ShippingNotificationConverters;

public class ConvertAcknowledgementShippingNotificationFailedResultTestCaseSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new TestCaseData(
            new ConvertAcknowledgementShippingNotificationFailedResultTestCase
            {
                Input = new AcknowledgementShippingNotificationInput
                {
                    BoxHeader = null,
                    Contents = new List<ProductInput?>()
                },
                ExpectedReason = "Failed to convert shipping notification as BoxHeader is null"
            }).SetName("BoxHeader is null");
        
        yield return new TestCaseData(
            new ConvertAcknowledgementShippingNotificationFailedResultTestCase
            {
                Input = new AcknowledgementShippingNotificationInput
                {
                    BoxHeader = new BoxHeaderInput
                    {
                        BoxId = null,
                        SupplierId = "TRSP117"
                    },
                    Contents = new List<ProductInput?>()
                },
                ExpectedReason = $"Failed to convert shipping notification as BoxId is null"
            }).SetName("BoxId is null");
        
        yield return new TestCaseData(
            new ConvertAcknowledgementShippingNotificationFailedResultTestCase
            {
                Input = new AcknowledgementShippingNotificationInput
                {
                    BoxHeader = new BoxHeaderInput
                    {
                        BoxId = "6874453I",
                        SupplierId = null
                    },
                    Contents = new List<ProductInput?>()
                },
                ExpectedReason = "Failed to convert shipping notification as SupplierId is null for BoxId 6874453I"
            }).SetName("SupplierId is null");
        
        yield return new TestCaseData(
            new ConvertAcknowledgementShippingNotificationFailedResultTestCase
            {
                Input = new AcknowledgementShippingNotificationInput
                {
                    BoxHeader = new BoxHeaderInput
                    {
                        BoxId = "6874453I",
                        SupplierId = "TRSP117"
                    },
                    Contents = new List<ProductInput?>()
                },
                ExpectedReason = "Failed to convert shipping notification as no product inputs found for BoxId 6874453I"
            }).SetName("No product lines");
        
        yield return new TestCaseData(
            new ConvertAcknowledgementShippingNotificationFailedResultTestCase
            {
                Input = new AcknowledgementShippingNotificationInput
                {
                    BoxHeader = new BoxHeaderInput
                    {
                        BoxId = "6874453I",
                        SupplierId = "TRSP117"
                    },
                    Contents = new List<ProductInput?> { null }
                },
                ExpectedReason = "Failed to convert 1 products for box ID: 6874453I"
            }).SetName("Product input is null");
    }
}