namespace MMAC.Services.AuditLogService
{
    public interface IAuditLogService
    {
        Task LogAsync(string activity, object details, Guid travellerId);
    }
}
