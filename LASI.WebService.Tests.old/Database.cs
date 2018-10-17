using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.WebService.Data;
using LASI.WebService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LASI.WebService.Tests
{
    static class Database
    {
        internal static DocumentsContext CreateInMemoryDatabase()
        {
            var context = new DocumentsContext(new DbContextOptionsBuilder<DocumentsContext>().UseInMemoryDatabase("DocumentsContext",
                databaseRoot: new Microsoft.EntityFrameworkCore.Storage.InMemoryDatabaseRoot()
            ).Options);

            context.Uploads.AddRange(new UploadDocument
            {

                Deleted = false,
                FileName = "test15.txt",
                Content = "This was only a test.",
                Name = "test15.txt",
            });

            return context;
        }
    }
}
