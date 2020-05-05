using System.Configuration;
using System.Xml;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace XMLWriterDemo
{
    public class T2033 : DIP
    {
        public T2033()
        {
            parent = "DocuSignEnvelopeInformation";

            keywords.Add(new KeywordType("Staff", ""));
            keywords.Add(new KeywordType("DocTypeName", ""));
            keywords.Add(new KeywordType("FileType", ""));
            keywords.Add(new KeywordType("FilePath", ""));
            keywords.Add(new KeywordType("FileName", ""));
            keywords.Add(new KeywordType("PolicyNumber", ""));
            keywords.Add(new KeywordType("RelinquishingCompanyRelinqInstFaxNumber", ""));
            keywords.Add(new KeywordType("WetSignature", ""));
            keywords.Add(new KeywordType("PolType", ""));
        }

        public void xmlBuild(string PATH)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = false;

            using (var writer = XmlWriter.Create(PATH, settings))
            {
                writer.WriteStartElement(parent, @"http://www.docusign.net/API/3.0");
                writer.WriteAttributeString("xmlns", @"http://www.docusign.net/API/3.0");
                writer.WriteAttributeString("xmlns", "xsd", null, @"http://www.w3.org/2001/XMLSchema");
                writer.WriteAttributeString("xmlns", "xsi", null, @"http://www.w3.org/2001/XMLSchema-instance");

                writer.WriteStartElement(newDocQualifier);

                foreach (var keyword in keywords)
                    writer.WriteElementString(keyword.keywordType, keyword.keywordValue);

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }
        }
        
        public void pdfBuild(string PATH)
        {
            PdfWriter writer = new PdfWriter(PATH);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph(keywords.Find(keyword => keyword.keywordType.Equals("PolicyNumber")).keywordValue)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            document.Add(header);
            document.Close();
        }
    }
}