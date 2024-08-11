
using Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;


namespace UserProvider
{
    public class GetUsers(ILogger<GetUsers> logger, DataContext context)
    {
        private readonly ILogger<GetUsers> _logger = logger;
        private readonly DataContext _context = context;


        [Function("GetUsers")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return new OkObjectResult(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return new BadRequestResult();
        }
    }


}
