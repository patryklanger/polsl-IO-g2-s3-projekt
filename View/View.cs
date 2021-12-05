using System;
using System.Collections.Generic;

namespace FileComparator
{
    public class View
    {
        public View()
        {
        }

        public void displayText(string textToDisplay)
        {
            Console.WriteLine(textToDisplay);
        }
        public void displayElement(KeyValuePair<int, string> element)
        {
            var newElement = "[" + element.Key + ". " + element.Value.Replace("\n", "") + "]";
            Console.WriteLine(newElement);
        }
        public void UpdateView()
        {
            throw new NotImplementedException();
        }
    }
}
