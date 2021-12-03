using System;
namespace FileComparator
{
    public interface IFileSaver
    {
        public abstract void SaveFile(Text textToSave, string directory, string fileName);
    }
}
