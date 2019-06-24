using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IFileReader
    {

        string FileRead(string path);
    }
    public class FileReader : IFileReader
    {
        private readonly IFile _file;

        public FileReader(IFile file)
        {
            _file = file;
        }
        public string FileRead(string path)
        {
            var str = _file.ReadAllText(path);
            return str;
        }
    }

    public interface IFile
    {
        string ReadAllText(string path);
    }


}
