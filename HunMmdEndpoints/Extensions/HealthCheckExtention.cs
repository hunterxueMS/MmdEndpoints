using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;
using System.Text.Json;

namespace HunMmdEndpoints.Extensions;

public static class HealthCheckExtention
{
    public static void ConfigureHealthCheck(this WebApplication app, ILogger logger)
    {
        var resposneWriter = delegate (HttpContext context, HealthReport healthReport)
            {
                context.Response.ContentType = "application/json; charset=utf-8";

                var options = new JsonWriterOptions { Indented = true };

                using var memoryStream = new MemoryStream();
                using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
                {
                    jsonWriter.WriteStartObject();
                    jsonWriter.WriteString("status", healthReport.Status.ToString());
                    jsonWriter.WriteStartObject("results");

                    foreach (var healthReportEntry in healthReport.Entries)
                    {
                        jsonWriter.WriteStartObject(healthReportEntry.Key);
                        jsonWriter.WriteString("status",
                            healthReportEntry.Value.Status.ToString());
                        jsonWriter.WriteString("description",
                            healthReportEntry.Value.Description);
                        jsonWriter.WriteStartObject("data");

                        foreach (var item in healthReportEntry.Value.Data)
                        {
                            jsonWriter.WritePropertyName(item.Key);

                            JsonSerializer.Serialize(jsonWriter, item.Value,
                                item.Value?.GetType() ?? typeof(object));
                        }

                        jsonWriter.WriteEndObject();
                        jsonWriter.WriteEndObject();
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }
                return context.Response.WriteAsync(
                    Encoding.UTF8.GetString(memoryStream.ToArray()));
            };

        app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
        {
            ResponseWriter = resposneWriter,
            Predicate = healthCheck => healthCheck.Tags.Contains("ready")
        });

        app.MapHealthChecks("/healthz/live", new HealthCheckOptions
        {
            ResponseWriter = resposneWriter,
            Predicate = _ => false
        });
    }
}
