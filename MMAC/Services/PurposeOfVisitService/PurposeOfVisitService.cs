using MMAC.Models.Master;
using MMAC.Repositories;

namespace MMAC.Services.PurposeOfVisitService
{
    public class PurposeOfVisitService : IPurposeOfVisitService
    {
        private readonly IPurposeOfVisitRepository _repository;

        public PurposeOfVisitService(IPurposeOfVisitRepository repository)
        {
            _repository = repository;
        }


        public async Task<PurposeOfVisit?> CreatePurposeOfVisitAsync(PurposeOfVisit purposeOfVisit)
        {
            try
            {
                return await _repository.AddPurposeOfVisitAsync(purposeOfVisit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }
        public async Task<IEnumerable<PurposeOfVisit>> GetPurposeOfVisitAsync()
        {
            try
            {
                return await _repository.GetAllPurposeOfVisitsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<PurposeOfVisit>();

        }
    }
}