using AsnMonitor.Application.Converters.Results;
using AsnMonitor.Application.NotificationInputs;

namespace AsnMonitor.Application.Converters;

public interface IAsnConverter
{
    IAsnConvertResult Convert(AsnInput input);
    
}