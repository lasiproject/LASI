using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.ContentSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;

namespace LASI.App
{
    class ProjectFile
    {
        private readonly ProjectInfo projectJson;

        public ProjectFile(string path) {
            projectJson = JsonConvert.DeserializeObject<ProjectInfo>(System.IO.File.ReadAllText(path));
            //projectJson.Validate(fileSchema, (s, e) => { throw e.Exception; });
        }
        //private JsonSchema LoadFileSchema() {
        //    using (var reader = System.Xml.XmlReader.Create(System.Configuration.ConfigurationManager.AppSettings["ProjectFileSchemaLocation"])) {
        //        var result = new XmlSchemaSet();
        //        var schema = XmlSchema.Read(reader, (s, e) => { throw e.Exception; });
        //        result.Add(schema);
        //        return result;
        //    }
        //}

        static JsonSchema fileSchema = new JsonSchemaGenerator
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }.Generate(typeof(ProjectInfo));

        private class ProjectInfo
        {
            public string ProjectName { get; set; }
            public string ProjectLocation { get; set; }
            public IEnumerable<InputFile> ProjectTextSources { get; set; }
        }
    }
}
