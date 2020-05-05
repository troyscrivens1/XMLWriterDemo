namespace XMLWriterDemo
{
    public class KeywordType
    {
        public KeywordType(string keywordType, string keywordValue)
        {
            this.keywordType = keywordType;
            this.keywordValue = keywordValue;
        }

        public string keywordType { get; set; }
        public string keywordValue { get; set; }
    }
}