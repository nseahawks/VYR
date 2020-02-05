using System;

namespace VYRMobile.Services
{
    public interface IFiles
    {
        string ReadTextFile(string path, string fileName);
        void WriteTextFile(string path, string filename, string stringToWrite);
        string RootDirectory();
    }
}