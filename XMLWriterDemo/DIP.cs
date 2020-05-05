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

        public string parent { get; set; }
        public string newDocQualifier { get; set; }
        public List<KeywordType> keywords { get; set; }
    }
}