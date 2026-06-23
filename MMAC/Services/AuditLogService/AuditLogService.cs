using MMAC.Data;
using MMAC.Models.Audits;

namespace MMAC.Services.AuditLogService
{
    public class AuditLogService : IAuditLogService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext; 

        public AuditLogService(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task LogAsync(string activity, object details, Guid travellerId)
        {
            var log = new AuditLogs
            {
                LogTime = DateTime.UtcNow,
                TravellerId = travellerId,
                LogIPAddr = _httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
                Activity = activity,
                Inputted = System.Text.Json.JsonSerializer.Serialize(details)
            };

            await _context.AuditLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
