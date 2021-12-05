using System;
using System.Collections.Generic;
using DiffMatchPatch;

namespace FileComparator
{
    class Program
    {

        static void Main(string[] args)
        {
            var textComparator = new PrimaryTextComparator();
            var fileWorker = new PrimaryFileWorker();
            var view = new View();
            var controller = new Controller(textComparator, view, fileWorker);
            controller.Main();
        }
    }
}