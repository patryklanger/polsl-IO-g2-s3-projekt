using System;
using System.Collections.Generic;
using DiffMatchPatch;

namespace FileComparator
{
    public class PrimaryTextComparator : ITextComparator
    {
        private int currentDecisionId;
        private bool mergeReady = false;
        private bool compared = false;
        private bool unsolvedConflict = false;
        private List<KeyValuePair<int, string>> listOfTexts;
        private Text resultText;
        private (Text, Text) conflict = (new Text(), new Text());
        public List<KeyValuePair<int, string>> ListOfTexts
        {
            private set => listOfTexts = value;
            get
            {
                try
                {
                    if (!compared) throw new NotComparedException();
                    return listOfTexts;
                }
                catch
                {
                    return new List<KeyValuePair<int, string>>();
                }
            }
        }
        public Text ResultText
        {
            get
            {
                try
                {
                    if (!mergeReady) throw new NotMergedException();
                    return resultText;
                }
                catch
                {
                    return new Text();
                }
            }
            private set => resultText = value;
        }
        public bool MergeReady
        {
            private set => mergeReady = value;
            get => mergeReady;
        }
        public (Text,Text) Conflict
        {
            private set => conflict = value;
            get => conflict;
        }
        public PrimaryTextComparator()
        {
            currentDecisionId = 0;
            resultText = new Text();

        }
        public void ResolveConfivt(int index)
        {
            if (index == 0) this.resultText.Content += conflict.Item1.Content;
            else if (index == 1) this.resultText.Content += conflict.Item2.Content;
            else throw new IndexOutOfRangeException();
            conflict = (new Text(), new Text());
            unsolvedConflict = false;
            if (currentDecisionId == listOfTexts[^1].Key) mergeReady = true;
            else currentDecisionId++;
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

        public void MakeComparison(Text text1, Text text2)
        {
            var dmp = new diff_match_patch();
            var diff = DiffLineMode(text1.Content, text2.Content);
            dmp.diff_cleanupSemantic(diff);
            SplitToBlocks(diff);
            this.compared = true;
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

        public bool MakeDecision()
        {
            if (unsolvedConflict) throw new UnresolvedConflictException();
            if (mergeReady) return true;
            for (int i = 0; i < listOfTexts.Count; i++)
            {
                if(i == listOfTexts.Count - 1 && listOfTexts[listOfTexts.Count-1].Key == currentDecisionId)
                {
                    resultText.Content += listOfTexts[i].Value;
                    mergeReady = true;
                    return true;
                }
                if (listOfTexts[i].Key == listOfTexts[i+1].Key && listOfTexts[i].Key == currentDecisionId)
                {
                    var firstText = new Text();
                    var secondText = new Text();

                    firstText.Content = listOfTexts[i].Value;
                    secondText.Content = listOfTexts[i + 1].Value;

                    conflict = (firstText, secondText);
                    unsolvedConflict = true;

                    return false;
                }
                if (listOfTexts[i].Key == currentDecisionId)
                {
                    resultText.Content += listOfTexts[i].Value;
                    currentDecisionId++;
                    return true;
                }
            }
            return true;
        }

        public Text CreateNewText()
        {
            if (!mergeReady) throw new NotMergedException();
            var resultText = new Text();
            foreach (var blockOfText in listOfTexts) resultText.Content += blockOfText;
            return resultText;
            
        }
    }
}
