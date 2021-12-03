using System;
namespace FileComparator
{
    public interface IFileReader
    {
        public abstract Text ReadFile(string filePath);
    }
}
