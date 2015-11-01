using System;
using System.IO;

namespace LASI.Content.Tests
{
    public abstract class FileConverterTestBase<TConvertFrom> : IDisposable where TConvertFrom : InputFile
    {
        private static int TestsRun;
        private readonly string filePath;
        private readonly string directoryPath;
        protected abstract string FileName { get; }
        private string FilePath => filePath;

        public FileConverterTestBase()
        {
            var file = new FileInfo($@"..\..\MockUserFiles\{FileName}");
            directoryPath = Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\{this.GetType()}\{TestsRun}").FullName;
            filePath = file.CopyTo($@"{directoryPath}\{FileName}", overwrite: true).FullName;
            ++TestsRun;
        }
        protected abstract Func<string, TConvertFrom> SourceFactory { get; }

        protected TConvertFrom SourceFile => SourceFactory(FilePath);

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
        #endregion
    }
}