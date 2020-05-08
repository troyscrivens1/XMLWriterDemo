using System;
using System.Configuration;
using System.IO;

namespace XMLWriterDemo
{
    /// <summary>
    /// Demonstration to show the possibilities to regression test XML document import processes in OnBase.
    ///
    /// In this example I will be using a Advisor Portal T2033
    /// </summary>
    internal class XMLWriterDemo
    {
        public static void Main(string[] args)
        {
            //Advisor Portal T2033
            var T2033 = new T2033();

            //Adding keyword values to our test case (this would typically be added per instance of each unit test.
            //My idea is that to test various elements of advisor portal T2033 (depending on our outcome), I can create different instances of T2033 Advisor Portals
            //  and change the values as needed.
            T2033.keywords.Find(w => w.keywordType.Equals("Staff")).keywordValue = "NONSTAFF";
            T2033.keywords.Find(w => w.keywordType.Equals("DocTypeName")).keywordValue = "T2033 Transfer Form";
            T2033.keywords.Find(w => w.keywordType.Equals("FileType")).keywordValue = "16";
            T2033.keywords.Find(w => w.keywordType.Equals("FilePath")).keywordValue =
                @"D:\Data\Inbound-for-DIP\AdvisorPortalT2033";
            T2033.keywords.Find(w => w.keywordType.Equals("FileName")).keywordValue =
                @"XMLWriterDemo15.pdf";
            T2033.keywords.Find(w => w.keywordType.Equals("PolicyNumber")).keywordValue = "T2033XML15";
            T2033.keywords.Find(w => w.keywordType.Equals("RelinquishingCompanyRelinqInstFaxNumber")).keywordValue =
                "14169790638";
            T2033.keywords.Find(w => w.keywordType.Equals("PolType")).keywordValue = "RRSP";
            T2033.keywords.Find(w => w.keywordType.Equals("WetSignature")).keywordValue = "NO";

            T2033.xmlBuild(ConfigurationManager.AppSettings["T2033_XML_PATH"]);
            T2033.pdfBuild(ConfigurationManager.AppSettings["T2033_DOC_PATH"]);

            
            //TODO: Find a better way to await file deletion before checking OnBase for the document.
            while (File.Exists(ConfigurationManager.AppSettings["T2033_XML_PATH"]) && File.Exists(ConfigurationManager.AppSettings["T2033_DOC_PATH"]))
            {
                Console.WriteLine("Waiting...");
                System.Threading.Thread.Sleep(1000);
            }
            
            System.Threading.Thread.Sleep(5000);
            
            OnBase.OnBase.fetchDocumentID(T2033.keywords.Find(w => w.keywordType.Equals("DocTypeName")).keywordValue, T2033.keywords);

            Console.ReadKey();
        }
    }
}