using System.Collections.Generic;

namespace XMLWriterDemo
{
    public class DIP
    {
        public DIP()
        {
            keywords = new List<KeywordType>();
            newDocQualifier = "NewDoc";
        }
        /// <summary>
        /// This is the root xml tag, it may differ based on the test case.
        /// </summary>
        public string parent { get; set; }
        /// <summary>
        /// NewDoc qualifier should always be "NewDoc", this is just in case this ever changes as OnBase updates.
        /// </summary>
        public string newDocQualifier { get; set; }
        /// <summary>
        /// Associated keywords for child case.
        /// </summary>
        public List<KeywordType> keywords { get; set; }
    }
}