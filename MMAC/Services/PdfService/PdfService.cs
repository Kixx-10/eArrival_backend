using Aspose.Slides;
using MMAC.DTOS;
using QRCoder;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace MMAC.Services.PdfService
{
    public class PdfService: IPdfService
    {
        private readonly IConfiguration _configuration;

        public PdfService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<byte[]> GenerateArrivalPdfAsync(CompleteArrivalDTO model, Guid applicationNo, string referenceNo)
        {
            return await Task.Run(() =>
            {
                // 1. Spire PDF Document အသစ်ဆောက်ခြင်း
                PdfDocument doc = new PdfDocument();
                PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, new PdfMargins(40));

                // Fonts & Colors သတ်မှတ်ခြင်း
                PdfFont titleFont = new PdfFont(PdfFontFamily.Helvetica, 22f, PdfFontStyle.Bold);
                PdfFont labelFont = new PdfFont(PdfFontFamily.Helvetica, 11f, PdfFontStyle.Bold);
                PdfFont valueFont = new PdfFont(PdfFontFamily.Helvetica, 11f, PdfFontStyle.Regular);
                PdfBrush blackBrush = PdfBrushes.Black;
                PdfBrush grayBrush = PdfBrushes.DarkGray;

                float currentY = 10f;

                // 2. QRCoder သုံးပြီး ရလာတဲ့ ApplicationNo ကို QR Code (Image) အဖြစ် ဆွဲခြင်း
                byte[] qrBytes = GenerateQrCodeBytes(applicationNo.ToString());
                using (MemoryStream qrStream = new MemoryStream(qrBytes))
                {
                    PdfImage qrImage = PdfImage.FromStream(qrStream);
                    float qrX = (page.Canvas.ClientSize.Width - 120) / 2; // Center alignment
                    page.Canvas.DrawImage(qrImage, qrX, currentY, 120, 120);
                    currentY += 130f;
                }

                // 3. Title (Arrival Form)
                PdfStringFormat centerFormat = new PdfStringFormat(PdfTextAlignment.Center);
                page.Canvas.DrawString("Arrival Form", titleFont, blackBrush, page.Canvas.ClientSize.Width / 2, currentY, centerFormat);
                currentY += 40f;

                // 4. Personal Information (Key-Value) စာသားများ ရေးခြင်း
                // Application ID
                page.Canvas.DrawString("Application ID No.", labelFont, blackBrush, 10, currentY);
                currentY += 18f;
                page.Canvas.DrawString(applicationNo.ToString(), valueFont, grayBrush, 10, currentY);
                currentY += 25f;

                // Name
                page.Canvas.DrawString("Name", labelFont, blackBrush, 10, currentY);
                currentY += 18f;
                page.Canvas.DrawString(model.FullName, valueFont, blackBrush, 10, currentY);
                currentY += 25f;

                // Gender
                page.Canvas.DrawString("Gender", labelFont, blackBrush, 10, currentY);
                currentY += 18f;
                page.Canvas.DrawString(model.Gender, valueFont, blackBrush, 10, currentY);
                currentY += 25f;

                // Nationality
                page.Canvas.DrawString("Nationality", labelFont, blackBrush, 10, currentY);
                currentY += 18f;
                page.Canvas.DrawString(model.CountryOfBirthCode, valueFont, blackBrush, 10, currentY);
                currentY += 25f;

                // Passport / Visa No.
                page.Canvas.DrawString("Passport / Visa No.", labelFont, blackBrush, 10, currentY);
                currentY += 18f;
                page.Canvas.DrawString(model.PassportNo, valueFont, blackBrush, 10, currentY);
                currentY += 40f;

                // 5. Grid Table အောက်ခြေအပိုင်း တည်ဆောက်ခြင်း
                PdfTable table = new PdfTable();
                DataTable dt = new DataTable();
                dt.Columns.Add("Key");
                dt.Columns.Add("Value");

                dt.Rows.Add("Arrival Date", model.ArrivalDate);
                dt.Rows.Add("Purpose of Visit", model.PurposeOfVisit);
                dt.Rows.Add("Contact (MM)", model.MobileNumber);
                dt.Rows.Add("Address in Myanmar", model.AddressInMyanmar);

                table.DataSource = dt;
                table.Style.CellPadding = 8f;
                table.Style.ShowHeader = false;// Header ပိတ်ထားမည်
                table.Style.BorderPen = new PdfPen(Color.LightGray, 0.5f);
                table.Style.DefaultStyle.Font = valueFont;

                table.Draw(page, new PointF(10, currentY));

                // 6. Memory Stream ထဲသို့ သိမ်းဆည်းပြီး Byte Array ပြန်ထုတ်ပေးခြင်း
                using MemoryStream outputStream = new MemoryStream();
                doc.SaveToStream(outputStream, FileFormat.PDF);
                doc.Close();

                return outputStream.ToArray();
            });
        }

        // QRCoder Helper
        private byte[] GenerateQrCodeBytes(string text)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        // Email sending Background Task
        public void SendPdfEmailInBackground(string toEmail, string applicationId, byte[] pdfBytes)
        {
            if (string.IsNullOrEmpty(toEmail)) return;
            string senderEmail = _configuration["EmailSettings:SenderEmail"]!;
            string senderName = _configuration["EmailSettings:SenderName"]!;
            string appPassword = _configuration["EmailSettings:AppPassword"]!;

            Task.Run(async () =>
            {
                try
                {
                    var fromAddress = new MailAddress(senderEmail, senderName);
                    var toAddress = new MailAddress(toEmail);
                    const string subject = "Your Arrival Form Registration Success";
                    const string body = "Dear Applicant,\n\nYour application has been submitted successfully. Please find your Arrival Form PDF attached below.";

                    using var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, appPassword)
                    };

                    using var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    };

                    using var ms = new MemoryStream(pdfBytes);
                    message.Attachments.Add(new Attachment(ms, $"ArrivalForm_{applicationId}.pdf", "application/pdf"));

                    await smtp.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SMTP Email Error]: {ex.Message}");
                }
            });
        }
    }
}
