using AsnMonitor.Application.Converters;
using AsnMonitor.Application.Converters.Results;
using AsnMonitor.Application.NotificationInputs;
using NSubstitute;
using Shouldly;

namespace AsnMonitor.Application.Tests.AuditServices;

public class AuditNotificationTests
{
    private AsnAuditService _asnAuditService;
    private IAsnAuditRepository _auditRepository;
    private IAsnConverter _asnConverter;

    [SetUp]
    public void Setup()
    {
        _asnConverter = Substitute.For<IAsnConverter>();
        _auditRepository = Substitute.For<IAsnAuditRepository>();
        _asnAuditService = new AsnAuditService(
            _asnConverter, _auditRepository);
    }

    [Test]
    public void AuditNotification_FailedResult_ShouldThrow()
    {
        var input = new AsnInput
        {
            Contents = new List<ProductInput?>()
        };
        _asnConverter.Convert(input)
            .Returns(new AsnFailedConvertResult
            {
                Reason = "Failed"
            });

        Should.Throw<FormatException>(() =>
                _asnAuditService.AuditNotification(input))
            .Message.ShouldStartWith("Failed to convert shipping notification input. Reason:");
    }

    [Test]
    public void AuditNotification_SuccessResult_ShouldSave()
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
        var convertSuccessResult = new AsnSuccessConvertResult
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
        _asnConverter.Convert(input)
            .Returns(convertSuccessResult);

        _asnAuditService.AuditNotification(input);
        _auditRepository.Received(1).Add(convertSuccessResult.Asn);
    }

    [Test]
    public void AuditNotification_UnknownResultType_ShouldThrow()
    {
        var input = new AsnInput
        {
            Contents = new List<ProductInput?>()
        };
        _asnConverter.Convert(input)
            .Returns(new UnknownShippingNotificationConvertResult());
        
        Should.Throw<Exception>(() =>
                _asnAuditService.AuditNotification(input))
            .Message.ShouldBe("Unexpected result from AsnConverter");
    }
}