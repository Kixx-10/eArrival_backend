namespace MMAC.DTOS
{
    public class RequestUpdateStatusDTO
    {
        public Guid AppNo { get; set; }
        public string AppStatus { get; set; } = string.Empty;
        public string ApproveUser { get; set; } = string.Empty; 
    }
}