using System;
using System.IO;

namespace LASI.Content.Tests
{
    public abstract class FileConverterTestBase<TConvertFrom> : IDisposable where TConvertFrom : InputFile
    {
#pragma warning disable RECS0108 // Warns about static fields in generic types
        static int TestsRun;
#pragma warning restore RECS0108 // Warns about static fields in generic types
        private readonly string filePath;
        private readonly string directoryPath;
        private string FilePath => filePath;

        public FileConverterTestBase(string fileName)
        {
            var file = new FileInfo($@"..\..\MockUserFiles\{fileName}");
            directoryPath = Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\{this.GetType()}\{TestsRun}").FullName;
            filePath = file.CopyTo($@"{directoryPath}\{fileName}", overwrite: true).FullName;
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