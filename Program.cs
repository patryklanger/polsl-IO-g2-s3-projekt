using System;
using System.Collections.Generic;
using DiffMatchPatch;

namespace FileComparator
{
    class Program
    {

        static void Main(string[] args)
        {
            string firstStringToCompare = "Jestem taki sam jak ty\nTroche inny";
            string secondStringToCompare = "Goodbye World!\nJestem taki sam jak ty\ntro inny";

            var text1 = new Text();
            var text2 = new Text();
            text1.Content = firstStringToCompare;
            text2.Content = secondStringToCompare;

            var comparator = new PrimaryTextComparator(text1, text2);
            var a = comparator.ListOfTexts;

            foreach(var element in a)
            {
                var newElement = "[" + element.Key+ ". " + element.Value.Replace("\n", "") + "]";
                Console.WriteLine(newElement);
            }


            //foreach (var patchElement in patches) Console.WriteLine(patchElement);

        }
    }
}