using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LASI.WebApp.Models;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Immutable;
using System.IO;
using LASI.Utilities;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace LASI.WebApp
{
    internal interface IAccountProvider : IEnumerable<IAccountModel>
    {
        void Insert(AccountModel account);
    }
}