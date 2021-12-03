using System;
namespace FileComparator
{
    public class Controller
    {
        public Text firstText;
        public Text secondText;
        public Text resultText;
        public ITextComparator comparator;
        public PrimaryFileWorker fileWorker;
        public Controller(ITextComparator comparator, View view)
        {
            throw new NotImplementedException();
        }
        
    }
}
