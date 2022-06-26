using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManipulaPdf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var pdfs = new List<byte[]>
            {
                File.ReadAllBytes(@"file1.pdf"),
                File.ReadAllBytes(@"file2.pdf")
            };

            var pdfResult = MergePdf(pdfs);
            Console.ReadKey();
        }

        public static byte[] MergePdf(List<byte[]> pdfs)
        {
            var pdfDocuments = new List<PdfDocument>();
            pdfs.ForEach(pdf => pdfDocuments.Add(PdfReader.Open(new MemoryStream(pdf), PdfDocumentOpenMode.Import)));

            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var outPdf = new PdfDocument())
            {
                for (int i = 1; i <= pdfDocuments.Count; i++)
                {
                    foreach (var page in pdfDocuments[i - 1].Pages)
                    {
                        outPdf.AddPage(page);
                    }
                }
                
                var stream = new MemoryStream();
                outPdf.Save(stream, false);
                //outPdf.Save("file1and2.pdf");

                return stream.ToArray();
            }
        }
    }
}