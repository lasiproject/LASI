using System;
using System.IO;
using LASI.Content.FileTypes;

namespace LASI.Content.Tests
{
    public abstract class FileConverterBaseTest<TInput> : IDisposable where TInput : InputFile
    {
#pragma warning disable RECS0108 // Warns about static fields in generic types
        static int testsRun;
#pragma warning restore RECS0108 // Warns about static fields in generic types
        private readonly string filePath;
        private readonly string directoryPath;
        private string FilePath => filePath;

        public FileConverterBaseTest(string fileName)
        {
            var file = FileInfo($@"..\..\MockUserFiles\{fileName}");
            directoryPath = Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\{GetType()}\{testsRun}").FullName;
            filePath = file.CopyTo($@"{directoryPath}\{fileName}", overwrite: true).FullName;
            testsRun += 1;
        }

        protected abstract Func<string, TInput> SourceFactory { get; }

        protected static FileInfo FileInfo(string fileName) => new FileInfo(fileName);

        protected TInput Input => SourceFactory(FilePath);

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //Directory.Delete(directoryPath, recursive: true);
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}