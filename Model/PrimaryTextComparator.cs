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
        private List<KeyValuePair<int, string>> listOfTexts;
        private Text resultText;
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
        public PrimaryTextComparator()
        {
            currentDecisionId = 0;
            resultText = new Text();

        }
        public void ConflictSolved(Text textToAdd)
        {
            this.resultText.Content += textToAdd.Content;
            currentDecisionId++;
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

        public List<Text> MakeDecision()
        {
            if (mergeReady) return null;
            for (int i = 0; i < listOfTexts.Count; i++)
            {
                if(i == listOfTexts.Count - 1 && listOfTexts[listOfTexts.Count-1].Key == currentDecisionId)
                {
                    resultText.Content += listOfTexts[i].Value;
                    mergeReady = true;
                    return null;
                }
                if (listOfTexts[i].Key == listOfTexts[i+1].Key && listOfTexts[i].Key == currentDecisionId)
                {
                    //Console.WriteLine("Choose version: 1 or 2");
                    //string userChoise = Console.ReadLine();
                    //if (userChoise == "1") resultText.Content += listOfTexts[i].Value;
                    //else resultText.Content += listOfTexts[i + 1].Value;
                    //currentDecisionId++;
                    //if(listOfTexts[listOfTexts.Count - 1].Key == listOfTexts.Count)
                    //{
                    //    mergeReady = true;
                    //    return null;
                    //}
                    var listToReturn = new List<Text>();
                    var firstText = new Text();
                    var secondText = new Text();

                    firstText.Content = listOfTexts[i].Value;
                    secondText.Content = listOfTexts[i + 1].Value;

                    listToReturn.Add(firstText);
                    listToReturn.Add(secondText);
                    return listToReturn;
                }
                if (listOfTexts[i].Key == currentDecisionId)
                {
                    resultText.Content += listOfTexts[i].Value;
                    currentDecisionId++;
                    return null;
                }
            }
            return null;
        }

        public Text CreateNewText()
        {
            try
            {
                if (!this.mergeReady) throw new NotMergedException();
                var resultText = new Text();
                foreach (var blockOfText in this.listOfTexts) resultText.Content += blockOfText;
                return resultText;
            }
            catch
            {
                return new Text();
            }
            
        }
    }
}
