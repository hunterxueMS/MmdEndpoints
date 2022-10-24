using System.Text.Json.Serialization;

namespace HunMmdEndpoints.Utils;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MMDServiceType
{
    customer = 0,
    partner = 1,
    tm = 2,
    device = 3,
    operation = 4,
    art = 5,
    um = 6,
}
