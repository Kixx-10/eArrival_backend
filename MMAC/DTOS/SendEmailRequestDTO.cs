namespace MMAC.DTOS
{
    public class SendEmailRequestDTO
    {
        
            public CompleteArrivalDTO? Model { get; set; }
            public Guid ApplicationNo { get; set; }
            public required string ReferenceNo { get; set; }
            public required string TargetEmail { get; set; }
        
    }
}
