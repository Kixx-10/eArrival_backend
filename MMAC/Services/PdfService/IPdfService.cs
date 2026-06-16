using MMAC.DTOS;

namespace MMAC.Services.PdfService
{
    public interface IPdfService
    {
        Task<byte[]> GenerateArrivalPdfAsync(CompleteArrivalDTO model, Guid applicationNo, string referenceNo);
        void SendPdfEmailInBackground(string toEmail, string applicationId, byte[] pdfBytes);
    }
}
