using System;
using System.Collections.Generic;
using System.IO;
using Hyland.Unity;
using Hyland.Unity.Extensions;
using System.Security.Cryptography;

namespace XMLWriterDemo.OnBase
{
    /// <summary>
    /// This is a controller class for all things OnBase related
    ///
    /// I will be using this class to build out methods to access document / lifecycle and queue data.
    /// </summary>
    public class OnBase
    {
        /// <summary>
        /// Building connection authentication properties
        /// </summary>
        /// <returns>OnBaseAuthenticationProperties authenticationProperties</returns>
        private static OnBaseAuthenticationProperties GetAuthenticationProperties()
        {
            var authenticationProperties =
                Application.CreateOnBaseAuthenticationProperties(
                    System.Configuration.ConfigurationManager.AppSettings["url"], 
                    System.Configuration.ConfigurationManager.AppSettings["username"], 
                    System.Configuration.ConfigurationManager.AppSettings["password"], 
                    System.Configuration.ConfigurationManager.AppSettings["dataSource"]
                );

            authenticationProperties.LicenseType = LicenseType.EnterpriseCoreAPI;

            return authenticationProperties;
        }
        
        /// <summary>
        /// Attempts to retrieve a document from onbase based on documentType and associated keywords
        ///
        /// TODO: Add dictionary for keywords, or limit keyword filtering. This could be done on method call instead as well.
        /// </summary>
        /// <param name="docType"></param>
        /// <param name="keywords"></param>
        public static void fetchDocumentID(string docType, List<KeywordType> keywords)
        {
            //Need this initialized outside of using statement ot handle try/catch exception close.
            Application _OnBaseApplication;

            try
            {
                //Open connect using authenticationProperties defined in App.config
                using (_OnBaseApplication = Application.Connect(GetAuthenticationProperties()))
                {
                    //Specify DocumentType for query filter
                    var documentType = _OnBaseApplication.Core.DocumentTypes.Find(docType);
                    var documentQuery = _OnBaseApplication.Core.CreateDocumentQuery();

                    documentQuery.AddDocumentType(documentType);
                    
                    //We only want one result (in this example), so I'm limiting the datetime to 1 day (today).
                    documentQuery.AddDateRange(DateTime.Now.AddDays(-1), DateTime.Now);

                    //Grab policy number from keywords parameter (this may be expanded later).
                    foreach (var keyword in keywords)
                    {
                        if (keyword.keywordType.Equals("PolicyNumber"))
                        {
                            documentQuery.AddKeyword("Policy #", keyword.keywordValue);
                        }
                    }

                    //Specify a max documents limit, may change this to be BIGINT. For testing purposes 10 is fine.
                    //TODO: Inquire about changing max documents limit on query results.
                    var documentList = documentQuery.Execute(10);
                    if (documentList.Count > 0)
                    {
                        foreach (var variable in documentList)
                        {
                            Console.WriteLine("Query returned document: " + variable.ID); //Document Handle
                            Console.WriteLine("Query time stored: " + variable.DateStored); //Datetime file stored in OnBase
                            var queueList = _OnBaseApplication.Workflow.GetQueues(variable); //Create list of queueList data associated with document.

                            foreach (var queue in queueList)
                            {
                                Console.WriteLine("Query returned item in LC: " + queue.LifeCycle.Name); //Lifecycle Name
                                Console.WriteLine("                    Queue: " + queue.Name); //Queue Name
                                Console.WriteLine("");
                            }

                            Console.WriteLine("It took: " + (DateTime.Now - variable.DateStored).TotalMinutes + "to upload."); //Time it took to find document in upload (from ingestion to query).
                            if ((DateTime.Now - variable.DateStored).TotalMinutes <= 5)
                            {
                                Console.WriteLine("Storage time was less than 5 minutes ago"); //Specifies if the time was under 5 minutes (this is just for testing). Average time is 1.5 minutes
                            }
                            
                            //TODO: feature - find all associated documents and purge?
                            //Console.WriteLine("Purging Document");
                            //_OnBaseApplication.Core.Storage.PurgeDocument();

                        }
                    }

                    _OnBaseApplication.Disconnect();
                }
            }
            catch (Exception ex)
            {
                //Need to close connection on exception, this creates an error, as _OnBaseApplication considers itself not connected, and unable to dispose of the object.
                //TODO: Try .Close() instead of dispose due to error on exception handling.
                //if(_OnBaseApplication.IsConnected)
                    //_OnBaseApplication.Dispose();
                
                Console.WriteLine(ex.Message);
            }
        }
    }
}