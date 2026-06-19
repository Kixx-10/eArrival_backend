using MMAC.Models.Cores;

namespace MMAC.DTOS
{
    public class DashboardDTO
    {
        public int SubmitedApplicationCount { get; set; }
        public int SubmitedApplicationByForeignerCount { get; set; }
        public int SubmitedApplicationByMyanmarCount { get; set; }
        public int ApprovedApplicationCount { get; set; }
        public int ApprovedApplicationByForeignerCount { get; set; }
        public int ApprovedApplicationByMyanmarCount { get; set; }
        public List<Traveller>Travellers { get; set; } = new List<Traveller>();
        public List<ArrivalApplication> Applications { get; set; } = new List<ArrivalApplication>();

    }
}
