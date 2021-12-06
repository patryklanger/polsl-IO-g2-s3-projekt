using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileComparator
{
    public class Controller
    {
        public Text firstText;
        public Text secondText;
        public Text resultText;
        public ITextComparator comparator;
        public PrimaryFileWorker fileWorker;
        public View view;
        public Controller(ITextComparator comparator, View view, PrimaryFileWorker fileWorker)
        {
            this.comparator = comparator;
            this.view = view;
            this.fileWorker = fileWorker;
        }
        public async Task Main()
        {

            var text11 = fileWorker.ReadFile(@"/Users/patryklanger/inputFile.txt");
            var text12 = fileWorker.ReadFile(@"/Users/patryklanger/inputFile1.txt");

            List<Text> tmpList;
            comparator.MakeComparison(text11, text12);
            while (!comparator.MergeReady)
            {
                tmpList = comparator.MakeDecision();
                if(tmpList != null)
                {
                    SolveConflict(tmpList);
                    tmpList = null;
                }
            }

            Text resultText = comparator.ResultText;
            Console.WriteLine(resultText.Content);
            await fileWorker.SaveFile(resultText, @"/Users/patryklanger/", "result.txt").ContinueWith((antecedent) =>Console.WriteLine("FileSaved!"));

            
        }
        private void SolveConflict(List<Text> conflictList)
        {
            view.DisplayText("Choose between two options:\nFirst option is");
            view.DisplayTextWithNumber(1, conflictList[0]);
            view.DisplayTextWithNumber(2, conflictList[1]);
            var userChoise = view.WaitForUserInput();
            if (userChoise == "1") comparator.ConflictSolved(conflictList[0]);
            else comparator.ConflictSolved(conflictList[1]);
        } 
        
    }
}
