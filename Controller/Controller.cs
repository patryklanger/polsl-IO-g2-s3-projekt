using System;
using System.Collections.Generic;

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
        public void Main()
        {
            string firstStringToCompare = "Jestem taki sam jak ty\nTroche inny";
            string secondStringToCompare = "Goodbye World!\nJestem taki sam jak ty\ntro inny";

            var text1 = new Text();
            var text2 = new Text();
            text1.Content = firstStringToCompare;
            text2.Content = secondStringToCompare;

            comparator.MakeComparison(text1, text2);

            List<KeyValuePair<int, string>> a = comparator.ListOfTexts;

            foreach (var element in a)
            {
                view.displayElement(element);
            }

            
        }
        
    }
}
