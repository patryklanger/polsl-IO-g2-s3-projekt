using System;
using System.Threading.Tasks;

namespace FileComparator
{
    public interface IFileSaver
    {
        public abstract Task SaveFile(Text textToSave, string directory, string fileName);
    }
}
