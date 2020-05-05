using System.Configuration;

namespace XMLWriterDemo
{
    internal class XMLWriterDemo
    {
        public static void Main(string[] args)
        {
            var T2033 = new T2033();

            T2033.keywords.Find(w => w.keywordType.Equals("Staff")).keywordValue = "NONSTAFF";
            T2033.keywords.Find(w => w.keywordType.Equals("DocTypeName")).keywordValue = "T2033 Transfer Form";
            T2033.keywords.Find(w => w.keywordType.Equals("FileType")).keywordValue = "16";
            T2033.keywords.Find(w => w.keywordType.Equals("FilePath")).keywordValue =
                @"D:\Data\Inbound-for-DIP\AdvisorPortalT2033";
            T2033.keywords.Find(w => w.keywordType.Equals("FileName")).keywordValue =
                @"Docusign-SEG-LO001624P-T2033-38b4b33d-e2dc-4128-96cb-ae7207fcf10d-DOCID-2.pdf";
            T2033.keywords.Find(w => w.keywordType.Equals("PolicyNumber")).keywordValue = "T2033XMLT2";
            T2033.keywords.Find(w => w.keywordType.Equals("RelinquishingCompanyRelinqInstFaxNumber")).keywordValue =
                "14169790638";
            T2033.keywords.Find(w => w.keywordType.Equals("PolType")).keywordValue = "RRSP";
            T2033.keywords.Find(w => w.keywordType.Equals("WetSignature")).keywordValue = "NO";

            T2033.xmlBuild(ConfigurationManager.AppSettings["T2033_XML_PATH"]);
            T2033.pdfBuild(ConfigurationManager.AppSettings["T2033_DOC_PATH"]);
        }
    }
}