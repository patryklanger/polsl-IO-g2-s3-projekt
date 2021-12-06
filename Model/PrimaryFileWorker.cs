﻿using System;
using System.IO;
using System.Threading.Tasks;
namespace FileComparator
{
    public class PrimaryFileWorker : IFileReader, IFileSaver
    {
        public PrimaryFileWorker()
        {
        }

        public Text ReadFile(string filePath)
        {
            try
            {
                string text = System.IO.File.ReadAllText(filePath);
                var resultObject = new Text();
                resultObject.Directory = filePath;
                resultObject.Content = text;
                return resultObject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Text();
            }
        }

        public async Task SaveFile(Text textToSave, string directory, string fileName)
        {
            await File.WriteAllTextAsync((directory+fileName), textToSave.Content);
        }
    }
}
