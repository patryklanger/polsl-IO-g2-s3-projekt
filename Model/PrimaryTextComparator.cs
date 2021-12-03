using System;
using System.Collections.Generic;
using DiffMatchPatch;

namespace FileComparator
{
    public class PrimaryTextComparator : ITextComparator
    {
        private int currentDecisionId;
        private bool mergeReady = false;
        private List<KeyValuePair<int, string>> listOfTexts;
        public List<KeyValuePair<int, string>> ListOfTexts
        {
            private set => listOfTexts = value;
            get => listOfTexts;
        }

        public PrimaryTextComparator(Text firstText, Text secondText)
        {
            MakeComparison(firstText, secondText);
        }

        private List<Diff> DiffLineMode(string text1, string text2)
        {
            var dmp = new diff_match_patch();
            var a = dmp.diff_linesToChars(text1, text2);
            var lineText1 = (string)a[0];
            var lineText2 = (string)a[1];
            var lineArray = (IList<string>)a[2];
            var diffs = dmp.diff_main(lineText1, lineText2, false);
            dmp.diff_charsToLines(diffs, lineArray);
            return diffs;
        }

        private void MakeComparison(Text text1, Text text2)
        {
            var dmp = new diff_match_patch();
            var diff = DiffLineMode(text1.Content, text2.Content);
            dmp.diff_cleanupSemantic(diff);
            SplitToBlocks(diff);
        } 

        private void SplitToBlocks(List<Diff> diffSToSplit)
        {
            var currentId = 0;
            List<KeyValuePair<int, string>> diffDict = new List<KeyValuePair<int, string>>();

            for (int i = 0; i < diffSToSplit.Count; i++)
            {
                if ((diffSToSplit[i].operation == Operation.DELETE && diffSToSplit[i + 1].operation == Operation.INSERT))
                {

                    var firstElementToAdd = new KeyValuePair<int, string>(currentId, diffSToSplit[i].text);
                    var secondElementToAdd = new KeyValuePair<int, string>(currentId++, diffSToSplit[i + 1].text);
                    diffDict.Add(firstElementToAdd);
                    diffDict.Add(secondElementToAdd);
                    i++;
                }
                else if (diffSToSplit[i].operation == Operation.INSERT)
                {
                    var firstElementToAdd = new KeyValuePair<int, string>(currentId, diffSToSplit[i].text);
                    var secondElementToAdd = new KeyValuePair<int, string>(currentId++, "");
                    diffDict.Add(firstElementToAdd);
                    diffDict.Add(secondElementToAdd);
                }
                else
                {
                    var elementToAdd = new KeyValuePair<int, string>(currentId++, diffSToSplit[i].text);
                    diffDict.Add(elementToAdd);
                }
            }
            this.listOfTexts = diffDict;
        }

        public void MakeDecision()
        {
            throw new NotImplementedException();
        }

        public Text CreateNewText()
        {
            if (!this.mergeReady) throw new NotMergedException();
            var resultText = new Text();
            foreach(var blockOfText in this.listOfTexts) resultText.Content += blockOfText;
            return resultText;
        }
    }
}
