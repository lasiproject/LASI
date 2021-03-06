﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Authorization;

namespace LASI.WebApp.Controllers.Documents
{
    [Authorize("Bearer")]
    [Route("api/UserDocuments/[controller]")]
    public class ListController : Controller
    {
        public ListController(IDocumentAccessor<UserDocument> documentStore)
        {
            this.documentStore = documentStore;
        }
        [HttpGet("{limit?}")]
        public IEnumerable<dynamic> Get(int limit = int.MaxValue)
        {
            if (HttpContext.User == null) {
                return Enumerable.Empty<dynamic>();
            }
            var results =
                from document in documentStore.GetAllForUser(HttpContext.User.GetUserId())
                let activeDocument = ActiveUserDocument.FromUserDocument(document)
                let dateUploaded = (DateTimeOffset)(JToken)(document.DateUploaded)
                orderby dateUploaded descending
                select new
                {
                    Id = activeDocument._id.ToString(),
                    Name = activeDocument.Name,
                    Progress = activeDocument.Progress
                };
            return results.Take(limit);
        }

        private readonly IDocumentAccessor<UserDocument> documentStore;
    }
}