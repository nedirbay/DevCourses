using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DevCourses.API.Data
{
    public class CustomHealthCheck: IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default(CancellationToken))
        {
            var isHealthy = true;

            if (isHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("Özel kontrol başarılı."));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("Özel kontrol başarısız oldu."));
        }
    }
}
