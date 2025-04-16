using AcknowledgementShippingNotificationWatcher.Domain;
using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;
using NSubstitute;
using Shouldly;

namespace AcknowledgementShippingNotificationWatcher.Tests.AuditServices;

public class AuditNotificationTests
{
    private ShippingNotificationAuditService _shippingNotificationAuditService;
    private IShippingNotificationAuditRepository _auditRepository;
    private IAcknowledgementShippingNotificationConverter _acknowledgementShippingNotificationConverter;

    [SetUp]
    public void Setup()
    {
        _acknowledgementShippingNotificationConverter = Substitute.For<IAcknowledgementShippingNotificationConverter>();
        _auditRepository = Substitute.For<IShippingNotificationAuditRepository>();
        _shippingNotificationAuditService = new ShippingNotificationAuditService(
            _acknowledgementShippingNotificationConverter, _auditRepository);
    }

    [Test]
    public void AuditNotification_FailedResult_ShouldThrow()
    {
        var input = new AcknowledgementShippingNotificationInput
        {
            Contents = new List<ProductInput?>()
        };
        _acknowledgementShippingNotificationConverter.Convert(input)
            .Returns(new AcknowledgementShippingNotificationFailedConvertResult
            {
                Reason = "Failed"
            });

        Should.Throw<FormatException>(() =>
                _shippingNotificationAuditService.AuditNotification(input))
            .Message.ShouldStartWith("Failed to convert shipping notification input. Reason:");
    }

    [Test]
    public void AuditNotification_SuccessResult_ShouldSave()
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
        var convertSuccessResult = new AcknowledgementShippingNotificationSuccessConvertResult
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
        _acknowledgementShippingNotificationConverter.Convert(input)
            .Returns(convertSuccessResult);

        _shippingNotificationAuditService.AuditNotification(input);
        _auditRepository.Received(1).Add(convertSuccessResult.AcknowledgementShippingNotification);
    }

    [Test]
    public void AuditNotification_UnknownResultType_ShouldThrow()
    {
        var input = new AcknowledgementShippingNotificationInput
        {
            Contents = new List<ProductInput?>()
        };
        _acknowledgementShippingNotificationConverter.Convert(input)
            .Returns(new UnknownShippingNotificationConvertResult());
        
        Should.Throw<Exception>(() =>
                _shippingNotificationAuditService.AuditNotification(input))
            .Message.ShouldBe("Unexpected result from AcknowledgementShippingNotificationConverter");
    }
}