using System;
using System.Collections.Generic;

namespace FileComparator
{
    public class View
    {
        public View()
        {
        }

        public void DisplayText(string textToDisplay)
        {
            Console.WriteLine(textToDisplay);
        }
        public void DisplayKeyValueElement(KeyValuePair<int, string> element)
        {
            var newElement = "[" + element.Key + ". " + element.Value.Replace("\n", "") + "]";
            Console.WriteLine(newElement);
        }
        public void DisplayTextWithNumber(int number, Text textToDisplay)
        {
            Console.WriteLine(number + ". " + textToDisplay.Content);
        }
        public void UpdateView()
        {
            throw new NotImplementedException();
        }
        public string WaitForUserInput()
        {
            return Console.ReadLine();
        }
    }
}
