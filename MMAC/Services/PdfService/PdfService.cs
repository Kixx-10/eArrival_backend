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
using MMAC.Services.AuditLogService;
using MMAC.Models.Cores;

namespace MMAC.Services.PdfService
{
    public class PdfService : IPdfService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuditLogService _auditLogService;
        private readonly IServiceScopeFactory _scopeFactory;

        public PdfService(IConfiguration configuration, IAuditLogService auditLogService, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _auditLogService = auditLogService;
            _scopeFactory = scopeFactory;
        }

        public async Task<byte[]> GenerateArrivalPdfAsync(CompleteArrivalDTO model, Guid applicationNo, string referenceNo)
        {
            return await Task.Run(() =>
            {
                PdfDocument doc = new PdfDocument();
                PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, new PdfMargins(40));

                // Font definitions
                PdfFont titleFont = new PdfFont(PdfFontFamily.Helvetica, 16f, PdfFontStyle.Bold);
                PdfFont subTitleFont = new PdfFont(PdfFontFamily.Helvetica, 12f, PdfFontStyle.Regular);
                PdfFont sectionFont = new PdfFont(PdfFontFamily.Helvetica, 12f, PdfFontStyle.Bold);
                PdfFont labelFont = new PdfFont(PdfFontFamily.Helvetica, 9f, PdfFontStyle.Bold);
                PdfFont valueFont = new PdfFont(PdfFontFamily.Helvetica, 10f, PdfFontStyle.Regular);

                PdfBrush blackBrush = PdfBrushes.Black;
                PdfBrush redBrush = PdfBrushes.DarkRed;
                PdfBrush grayBrush = PdfBrushes.DarkGray;

                float currentY = 10f;
                float pageWidth = page.Canvas.ClientSize.Width;

                // --- HEADER ---
                PdfStringFormat centerFormat = new PdfStringFormat(PdfTextAlignment.Center);
                page.Canvas.DrawString("REPUBLIC OF THE UNION OF MYANMAR", titleFont, redBrush, pageWidth / 2, currentY, centerFormat);
                currentY += 20f;
                page.Canvas.DrawString("Electronic Arrival Declaration (e-Arrival)", subTitleFont, grayBrush, pageWidth / 2, currentY, centerFormat);
                currentY += 40f;

                // --- QR CODE & DE NUMBER ---
                byte[] qrBytes = GenerateQrCodeBytes(applicationNo.ToString());
                using (MemoryStream qrStream = new MemoryStream(qrBytes))
                {
                    PdfImage qrImage = PdfImage.FromStream(qrStream);
                    page.Canvas.DrawImage(qrImage, pageWidth - 100, currentY, 90, 90);
                }

                page.Canvas.DrawString("Arrival Approval ID (DE Number):", labelFont, blackBrush, 10, currentY);
                currentY += 15f;
                page.Canvas.DrawString(referenceNo.ToString(), new PdfFont(PdfFontFamily.Helvetica, 14f, PdfFontStyle.Bold), redBrush, 10, currentY);
                currentY += 25f;

                string infoText = "Please present this approval code and QR code, along with your valid passport and visa (if applicable), to the Myanmar Immigration Officer upon arrival. Valid for a single entry.";
                PdfTextWidget textWidget = new PdfTextWidget(infoText, new PdfFont(PdfFontFamily.Helvetica, 9f, PdfFontStyle.Regular), grayBrush);
                PdfTextLayout textLayout = new PdfTextLayout();
                textLayout.Layout = PdfLayoutType.Paginate;
                textWidget.Draw(page, new RectangleF(10, currentY, pageWidth - 120, 50), textLayout);
                currentY += 60f;

                // --- PART I: Personal Particulars ---
                page.Canvas.DrawRectangle(PdfPens.DarkRed, PdfBrushes.LightGray, new RectangleF(10, currentY, pageWidth - 20, 20));
                page.Canvas.DrawString("PART I: Personal Particulars", sectionFont, blackBrush, 15, currentY + 3);
                currentY += 30f;

                DrawField(page, "FULL NAME", model.FullName, labelFont, valueFont, 10, currentY);
                DrawField(page, "PASSPORT NUMBER", model.PassportNo, labelFont, valueFont, pageWidth / 2, currentY);
                currentY += 35f;

                DrawField(page, "NATIONALITY", model.CountryOfBirthCode, labelFont, valueFont, 10, currentY);
                DrawField(page, "GENDER", model.Gender, labelFont, valueFont, pageWidth / 2, currentY);
                currentY += 35f;

                DrawField(page, "MOBILE NUMBER", model.MobileNumber, labelFont, valueFont, 10, currentY);
                currentY += 45f;

                // --- PART II: Trip Details ---
                page.Canvas.DrawRectangle(PdfPens.DarkRed, PdfBrushes.LightGray, new RectangleF(10, currentY, pageWidth - 20, 20));
                page.Canvas.DrawString("PART II: Trip Details", sectionFont, blackBrush, 15, currentY + 3);
                currentY += 30f;

                DrawField(page, "DATE OF ARRIVAL", model.ArrivalDate.ToString("dd MMM yyyy"), labelFont, valueFont, 10, currentY);
                DrawField(page, "PURPOSE OF VISIT", model.PurposeOfVisit, labelFont, valueFont, pageWidth / 2, currentY);
                currentY += 35f;

                DrawField(page, "ADDRESS IN DESTINATION", model.AddressInMyanmar, labelFont, valueFont, 10, currentY);
                currentY += 55f;

                // --- PART III: Health & Customs Declaration ---
                page.Canvas.DrawRectangle(PdfPens.DarkRed, PdfBrushes.LightGray, new RectangleF(10, currentY, pageWidth - 20, 20));
                page.Canvas.DrawString("PART III: Health & Customs Declaration", sectionFont, blackBrush, 15, currentY + 3);
                currentY += 30f;

                PdfTextWidget noticeWidget = new PdfTextWidget("NOTICE: False declarations are subject to prosecution under the laws of the Republic of the Union of Myanmar.", new PdfFont(PdfFontFamily.Helvetica, 9f, PdfFontStyle.Bold), PdfBrushes.DarkGoldenrod);
                noticeWidget.Draw(page, new RectangleF(10, currentY, pageWidth - 20, 30), textLayout);
                currentY += 30f;

                page.Canvas.DrawString("Are you carrying currency exceeding USD 10,000 or equivalent?", valueFont, blackBrush, 10, currentY);
                page.Canvas.DrawString("NO", labelFont, blackBrush, pageWidth - 40, currentY);
                currentY += 20f;

                page.Canvas.DrawString("Are you currently experiencing fever, cough, or shortness of breath?", valueFont, blackBrush, 10, currentY);
                page.Canvas.DrawString("NO", labelFont, blackBrush, pageWidth - 40, currentY);
                currentY += 50f;

                // --- FOOTER ---
                page.Canvas.DrawLine(PdfPens.Gray, 10, currentY, pageWidth - 10, currentY);
                currentY += 10f;
                string footerText = "IMPORTANT: This acknowledgment does not guarantee entry into Myanmar. The Department of Immigration and Population officers will assess your eligibility for entry upon arrival. Ensure your passport is valid for at least 6 months and you possess a valid visa if required. This document was generated electronically.";
                PdfTextWidget footerWidget = new PdfTextWidget(footerText, new PdfFont(PdfFontFamily.Helvetica, 8f, PdfFontStyle.Regular), grayBrush);
                footerWidget.Draw(page, new RectangleF(10, currentY, pageWidth - 20, 50), textLayout);

                using MemoryStream outputStream = new MemoryStream();
                doc.SaveToStream(outputStream, FileFormat.PDF);
                doc.Close();

                return outputStream.ToArray();
            });
        }

        // Helper Method to Draw Labels and Values neatly
        private void DrawField(PdfPageBase page, string label, string value, PdfFont labelFont, PdfFont valueFont, float x, float y)
        {
            page.Canvas.DrawString(label, labelFont, PdfBrushes.DarkGray, x, y);
            page.Canvas.DrawString(string.IsNullOrEmpty(value) ? "N/A" : value, valueFont, PdfBrushes.Black, x, y + 15f);
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
        public void SendPdfEmailInBackground(string toEmail, string applicationId, byte[] pdfBytes, string referenceNo,Guid travellerId)
        {

            Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();

                var scopedAuditLogService = scope.ServiceProvider.GetRequiredService<IAuditLogService>();

                try
                {
                    string senderEmail = _configuration["EmailSettings:SenderEmail"] ?? "jr.paingwaiyankhant@gmail.com";
                    string senderName = _configuration["EmailSettings:SenderName"] ?? "MMAC Arrival System";
                    string appPassword = _configuration["EmailSettings:AppPassword"] ?? "";

                    var fromAddress = new MailAddress(senderEmail, senderName);
                    var toAddress = new MailAddress(toEmail);
                    string subject = "Your e-Arrival Form Submission";
                    string body = "Dear Applicant,\n\nYour application has been submitted successfully. Please find your official e-Arrival Form PDF attached below.";

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
                    message.Attachments.Add(new Attachment(ms, $"MM_ArrivalForm_{referenceNo}.pdf", "application/pdf"));

                    await smtp.SendMailAsync(message);

                    var successLogObj = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "To", toEmail },
                    { "AppId", applicationId },
                    { "Status", "Success" }
                };

                    await scopedAuditLogService.LogAsync("EMAIL_SENT_SUCCESS", successLogObj, travellerId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SMTP Email Error]: {ex.Message}");
                    try
                    {
                        var errorLogObj = new System.Collections.Generic.Dictionary<string, string>
                    {
                        { "To", toEmail },
                        { "ErrorMessage", ex.Message }
                    };

                        await scopedAuditLogService.LogAsync("EMAIL_SENT_FAILED", errorLogObj, travellerId);
                    }
                    catch (Exception logEx)
                    {
                        Console.WriteLine($"[Critical Audit Log Error]: {logEx.Message}");
                    }
                }
            });
        }
    }
}