using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiffMatchPatch;

namespace FileComparator
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var textComparator = new PrimaryTextComparator();
            var fileWorker = new PrimaryFileWorker();
            var view = new View();
            var controller = new Controller(textComparator, view, fileWorker);
            await controller.Main();
        }
    }
}