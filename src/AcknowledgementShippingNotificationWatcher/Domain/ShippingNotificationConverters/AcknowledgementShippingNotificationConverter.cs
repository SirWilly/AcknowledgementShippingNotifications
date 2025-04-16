using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;

namespace AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters;

// According to business requirements we may want to allow some empty values but in this case I have considered that
// all the fields are mandatory and issues in parsing are not tolerated.
public class AcknowledgementShippingNotificationConverter : IAcknowledgementShippingNotificationConverter
{
    public IAcknowledgementShippingNotificationConvertResult Convert(AcknowledgementShippingNotificationInput input)
    {
        if (input.BoxHeader is null)
        {
            return new AcknowledgementShippingNotificationFailedConvertResult
            {
                Reason = $"Failed to convert shipping notification as {nameof(input.BoxHeader)} is null"
            };
        }

        if (input.BoxHeader.BoxId is null)
        {
            return new AcknowledgementShippingNotificationFailedConvertResult
            {
                Reason = $"Failed to convert shipping notification as {nameof(input.BoxHeader.BoxId)} is null"
            };
        }

        if (input.BoxHeader.SupplierId is null)
        {
            return new AcknowledgementShippingNotificationFailedConvertResult
            {
                Reason = $"Failed to convert shipping notification as {nameof(input.BoxHeader.SupplierId)} is null for BoxId {input.BoxHeader.BoxId}"
            };
        }

        if (input.Contents.Count == 0)
        {
            return new AcknowledgementShippingNotificationFailedConvertResult
            {
                Reason = $"Failed to convert shipping notification as no product inputs found for BoxId {input.BoxHeader.BoxId}"
            };
        }
        
        var productConvertResults = input.Contents.Select(Convert).ToList();
        var failedProductConvertResults = 
            productConvertResults.OfType<ProductConvertFailureResult>().ToList();

        if (failedProductConvertResults.Any())
        {
            return new AcknowledgementShippingNotificationFailedConvertResult
            {
                Reason =
                    $"Failed to convert {failedProductConvertResults.Count} products for box ID: {input.BoxHeader.BoxId}",
                FailedProductConvertResults = failedProductConvertResults
            };
        }
        
        return new AcknowledgementShippingNotificationSuccessConvertResult
        {
            AcknowledgementShippingNotification = new AcknowledgementShippingNotification
            {
                BoxId = input.BoxHeader.BoxId,
                SupplierId = input.BoxHeader.SupplierId,
                Contents = productConvertResults.OfType<ProductConvertSuccessResult>()
                    .Select(x => x.Product).ToList()
            }
        };
    }

    public IProductConvertResult Convert(ProductInput? input)
    {
        if (input == null)
        {
            return new ProductConvertFailureResult
            {
                Reason = $"Failed to convert product line as {nameof(ProductInput)} is null"
            };
        }

        if (input.PoNumber is null)
        {
            return new ProductConvertFailureResult
            {
                Reason = $"Failed to convert product line as {nameof(input.PoNumber)} is null"
            };
        }
        
        if (input.Isbn is null)
        {
            return new ProductConvertFailureResult
            {
                Reason = $"Failed to convert product line as {nameof(input.Isbn)} is null"
            };
        }
        
        if (input.Quantity is null)
        {
            return new ProductConvertFailureResult
            {
                Reason = $"Failed to convert product line as {nameof(input.Quantity)} is null"
            };
        }

        return new ProductConvertSuccessResult
        {
            Product = new Product
            {
                PoNumber = input.PoNumber,
                Isbn = input.Isbn,
                Quantity = input.Quantity.Value
            }
        };
    }
}