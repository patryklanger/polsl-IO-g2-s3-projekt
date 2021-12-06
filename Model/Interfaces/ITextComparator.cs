using System;
using System.Collections.Generic;

namespace FileComparator
{
    public interface ITextComparator
    {
        public abstract Text CreateNewText();
        public void MakeComparison(Text text1, Text text2);
        public bool MakeDecision();
        public void ResolveConfivt(int index);
        public List<KeyValuePair<int, string>> ListOfTexts
        {
            get;
        }
        public Text ResultText
        {
            get;
        }
        public bool MergeReady
        {
            get;
        }
        public (Text, Text) Conflict
        {
            get;
        }
    }
}
