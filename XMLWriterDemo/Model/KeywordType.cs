namespace XMLWriterDemo
{
    public class KeywordType
    {
        public KeywordType(string keywordType, string keywordValue)
        {
            this.keywordType = keywordType;
            this.keywordValue = keywordValue;
        }
        /// <summary>
        /// KeywordType is the XML TAG
        /// </summary>
        public string keywordType { get; set; }
        /// <summary>
        /// KeywordValue is the XML TAG VALUE
        /// </summary>
        public string keywordValue { get; set; }
    }
}