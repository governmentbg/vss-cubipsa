using System;
using System.IO;
using System.Text;
using SautinSoft;

namespace Legalacts.Utils.Managers.Pdf
{
    public class PdfConvertManager
    {
        static readonly string PDF_METAMORPHOSIS_SERIAL = "10024747414";
        //static readonly string PDF_USEOFFICE_SERIAL = "10012424851";
        //static readonly string PDF_PDFVISION_SERIAL = "10020249753";
        //static readonly string PDF_SECURITY_PASSWORD = "superadmin@bigla";
        //static readonly object rtfLocker = new object();
        static readonly object txtLocker = new object();
        static readonly object htmlLocker = new object();
        //static readonly object docxLocker = new object();

        public static byte[] Convert(byte[] input, ref string mimeType)
        {
            byte[] pdf = null;
            switch (mimeType)
            {
                //case MimeTypeFileExtension.MIME_TEXT_RTF:
                //    {
                //        pdf = ConvertRtfToPdf(input);
                //        if (pdf != null)
                //            mimeType = MimeTypeFileExtension.MIME_APPLICATION_PDF;
                //        break;
                //    }
                //case MimeTypeFileExtension.MIME_APPLICATION_MSWORD: // TODO: Change IT when are FIXED mimetypes
                //    {
                //        pdf = ConvertRtfToPdf(input);
                //        if (pdf == null)
                //            pdf = ConvertDocxToPdf(input);
                //        if (pdf == null)
                //            pdf = ConvertDocToPdf(input);
                //
                //        if (pdf == null)
                //            return input;
                //
                //        if (pdf != null)
                //            mimeType = MimeTypeFileExtension.MIME_APPLICATION_PDF;
                //        break;
                //    }
                case MimeTypeFileExtension.MIME_TEXT_PLAIN:
                    {
                        try{ pdf = ConvertTxtToPdf(input); } catch {}
                        
                        if (pdf != null)
                            mimeType = MimeTypeFileExtension.MIME_APPLICATION_PDF;

                        break;
                    }
                case MimeTypeFileExtension.MIME_TEXT_HTML:
                    {
                        try{ pdf = ConvertHtmlToPdf(input); } catch {}

                        if (pdf != null)
                            mimeType = MimeTypeFileExtension.MIME_APPLICATION_PDF;

                        break;
                    }
                default:
                    {
                        return input;
                    }
            }

            return pdf;
        }

        private static byte[] ConvertHtmlToPdf(byte[] input)
        {
            try
            {
                //lock (htmlLocker)
                //{
                    PdfMetamorphosis pdfConverter = new PdfMetamorphosis();
                    pdfConverter.SetSerial(PDF_METAMORPHOSIS_SERIAL);

                    pdfConverter.PageStyle.PageSize.A4();
                    pdfConverter.PageStyle.PageOrientation.Portrait();
                    pdfConverter.PageStyle.PageMarginLeft.Inch(0.7f);
                    pdfConverter.PageStyle.PageMarginRight.Inch(0.7f);
                    pdfConverter.PageStyle.PageMarginTop.Inch(0.7f);
                    pdfConverter.PageStyle.PageMarginBottom.Inch(0.7f);

                    pdfConverter.UnicodeOptions.FontsDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts);

                    byte[] pdf = pdfConverter.HtmlToPdfConvertStringToByte(Encoding.GetEncoding("windows-1251").GetString(input));

                    return pdf;
                //}
            }
            catch
            {
                return null;
            }
        }

        private static byte[] ConvertTxtToPdf(byte[] input)
        {
            try
            {
                //lock (txtLocker)
                //{
                    PdfMetamorphosis pdfConverter = new PdfMetamorphosis();
                    pdfConverter.SetSerial(PDF_METAMORPHOSIS_SERIAL);

                    pdfConverter.PageStyle.PageSize.A4();
                    pdfConverter.PageStyle.PageOrientation.Portrait();
                    pdfConverter.PageStyle.PageMarginLeft.Inch(0.7f);
                    pdfConverter.PageStyle.PageMarginRight.Inch(0.7f);
                    pdfConverter.PageStyle.PageMarginTop.Inch(0.7f);
                    pdfConverter.PageStyle.PageMarginBottom.Inch(0.7f);

                    pdfConverter.UnicodeOptions.FontsDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts);

                    byte[] pdf = pdfConverter.HtmlToPdfConvertStringToByte("<pre>" + Encoding.GetEncoding("windows-1251").GetString(input) + "</pre>");

                    return pdf;
                //}
            }
            catch
            {
                return null;
            }
        }

        //private static byte[] ConvertRtfToPdf(byte[] input)
        //{
        //    try
        //    {
        //        lock (rtfLocker)
        //        {
        //            MemoryStream rtfMemoryStream = new MemoryStream(input);
        //            MemoryStream pdfMemoryStream = new MemoryStream();

        //            PdfMetamorphosis pdfConverter = new PdfMetamorphosis();
        //            pdfConverter.SetSerial(PDF_METAMORPHOSIS_SERIAL);
        //            RichTextBox rtfParser = new RichTextBox();

        //            rtfParser.LoadFile(rtfMemoryStream, RichTextBoxStreamType.RichText);
        //            rtfParser.SaveFile(pdfMemoryStream, RichTextBoxStreamType.RichText);

        //            byte[] rtf = pdfMemoryStream.ToArray();
        //            byte[] pdf = pdfConverter.RtfToPdfConvertByte(rtf);

        //            return pdf;
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //private static byte[] ConvertDocxToPdf(byte[] input)
        //{
        //    try
        //    {
        //        lock (docxLocker)
        //        {
        //            UseOffice pdfConverter = new UseOffice();
        //            pdfConverter.Serial = PDF_USEOFFICE_SERIAL;

        //            //Prepare UseOffice .Net, loads MS Word in memory
        //            int ret = pdfConverter.InitWord();			

        //            //Return values:
        //            //0 - Loading successfully
        //            //1 - Can't load MS Word® library in memory 
        //            if (ret==1)
        //                throw new Exception("UseOffice: Can't load MS Word® library in memory");

        //            //Converting
        //            string projectDir = System.Web.HttpContext.Current.Server.MapPath("/");
        //            string wordDocument = projectDir + "word.docx";
        //            string pdfDocument = projectDir + "pdf.pdf";

        //            File.WriteAllBytes(wordDocument, input);

        //            //0 - Converting successfully
        //            //1 - Can't open input file. Check that you are using full local path to input file, URL and relative path are not supported
        //            //2 - Can't create output file. Please check that you have permissions to write by this path or probably this path already used by another application
        //            //3 - Converting failed, please contact with our Support Team
        //            //4 - MS Office isn't installed. The component requires that any of these versions of MS Office should be installed: 2000, XP, 2003, 2007 or 2010
        //            ret = pdfConverter.ConvertFile(wordDocument, pdfDocument, UseOffice.eDirection.DOCX_to_PDF);

        //            //Release MS Word from memory
        //            pdfConverter.CloseWord();

        //            if(File.Exists(wordDocument))
        //                File.Delete(wordDocument);

        //            byte[] pdf = null;

        //            if (File.Exists(pdfDocument))
        //            {
        //                pdf = File.ReadAllBytes(pdfDocument);
        //                File.Delete(pdfDocument);
        //            }
        //            else
        //            {
        //                if (ret == 1)
        //                    throw new Exception("UseOffice: Can't open input file. Check that you are using full local path to input file, URL and relative path are not supported");
        //                else if (ret == 2)
        //                    throw new Exception("UseOffice: Can't create output file. Please check that you have permissions to write by this path or probably this path already used by another application");
        //                else if (ret == 3)
        //                    throw new Exception("UseOffice: Converting failed, please contact with our Support Team");
        //                else if (ret == 4)
        //                    throw new Exception("UseOffice: MS Office isn't installed. The component requires that any of these versions of MS Office should be installed: 2000, XP, 2003, 2007 or 2010");
        //            }

        //            return pdf;
        //        }
        //    }
        //    catch(Exception)
        //    {
        //        return null;
        //    }
        //}

        //private static byte[] ConvertDocToPdf(byte[] input)
        //{
        //    try
        //    {
        //        lock (docxLocker)
        //        {
        //            UseOffice pdfConverter = new UseOffice();
        //            pdfConverter.Serial = PDF_USEOFFICE_SERIAL;

        //            //Prepare UseOffice .Net, loads MS Word in memory
        //            int ret = pdfConverter.InitWord();

        //            //Return values:
        //            //0 - Loading successfully
        //            //1 - Can't load MS Word® library in memory 
        //            if (ret == 1)
        //                throw new Exception("UseOffice: Can't load MS Word® library in memory");

        //            //Converting
        //            string projectDir = System.Web.HttpContext.Current.Server.MapPath("/");
        //            string wordDocument = projectDir + "word.doc";
        //            string pdfDocument = projectDir + "pdf.pdf";

        //            File.WriteAllBytes(wordDocument, input);

        //            //0 - Converting successfully
        //            //1 - Can't open input file. Check that you are using full local path to input file, URL and relative path are not supported
        //            //2 - Can't create output file. Please check that you have permissions to write by this path or probably this path already used by another application
        //            //3 - Converting failed, please contact with our Support Team
        //            //4 - MS Office isn't installed. The component requires that any of these versions of MS Office should be installed: 2000, XP, 2003, 2007 or 2010
        //            ret = pdfConverter.ConvertFile(wordDocument, pdfDocument, UseOffice.eDirection.DOC_to_PDF);

        //            //Release MS Word from memory
        //            pdfConverter.CloseWord();

        //            if (File.Exists(wordDocument))
        //                File.Delete(wordDocument);

        //            byte[] pdf = null;

        //            if (File.Exists(pdfDocument))
        //            {
        //                pdf = File.ReadAllBytes(pdfDocument);
        //                File.Delete(pdfDocument);
        //            }
        //            else
        //            {
        //                if (ret == 1)
        //                    throw new Exception("UseOffice: Can't open input file. Check that you are using full local path to input file, URL and relative path are not supported");
        //                else if (ret == 2)
        //                    throw new Exception("UseOffice: Can't create output file. Please check that you have permissions to write by this path or probably this path already used by another application");
        //                else if (ret == 3)
        //                    throw new Exception("UseOffice: Converting failed, please contact with our Support Team");
        //                else if (ret == 4)
        //                    throw new Exception("UseOffice: MS Office isn't installed. The component requires that any of these versions of MS Office should be installed: 2000, XP, 2003, 2007 or 2010");
        //            }

        //            return pdf;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //private static byte[] EncryptPdf(byte[] input)
        //{
        //    using (MemoryStream inputStream = new MemoryStream(input))
        //    {
        //        using (MemoryStream outputStream = new MemoryStream())
        //        {
        //            try
        //            {
        //                PdfReader reader = new PdfReader(inputStream);
        //                PdfEncryptor.Encrypt(reader, outputStream, true, null, PDF_SECURITY_PASSWORD,
        //                    PdfWriter.ALLOW_PRINTING &
        //                    ~PdfWriter.ALLOW_COPY &
        //                    ~PdfWriter.ALLOW_ASSEMBLY &
        //                    PdfWriter.ALLOW_DEGRADED_PRINTING &
        //                    ~PdfWriter.ALLOW_FILL_IN &
        //                    ~PdfWriter.ALLOW_MODIFY_ANNOTATIONS &
        //                    ~PdfWriter.ALLOW_MODIFY_CONTENTS &
        //                    ~PdfWriter.ALLOW_SCREENREADERS);
        //
        //                return outputStream.ToArray();
        //            }
        //            catch
        //            {
        //                return input;
        //            }
        //        }
        //    }
        //}
    }
}
