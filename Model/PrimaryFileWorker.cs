using System;
namespace FileComparator
{
    public class PrimaryFileWorker : IFileReader, IFileSaver
    {
        public PrimaryFileWorker()
        {
        }

        public Text ReadFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(Text textToSave, string directory, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
