using System.Linq;
using System.Threading.Tasks;
using LASI.Content;
using LASI.Core;
using LASI.Interop;
using Microsoft.AspNetCore.Mvc;

namespace LASI.WebService.Api
{
    [Produces("application/json")]
    [Route("api/[Controller]/{id?}")]
    public class DocumentsController : Controller
    {
        public async Task<Document> Get()
        {
            var orchestrator = new AnalysisOrchestrator(new RawTextFragment("House Fires", content));
            var documents = await orchestrator.ProcessAsync();
            return documents.First();
        }


        readonly string content =
@"
Each year more than 2,500 people die and 12,600 are injured in home fires in the United States, with direct property loss due to home fires estimated at $7.3 billion annually.Home fires can be prevented!

To protect yourself, it is important to understand the basic characteristics of fire. Fire spreads quickly.There is no time to gather valuables or make a phone call. In just two minutes, a fire can become life-threatening.In five minutes, a residence can be engulfed in flames.


Heat and smoke from fire can be more dangerous than the flames. Inhaling the super-hot air can sear your lungs. Fire produces poisonous gases that make you disoriented and drowsy. Instead of being awakened by a fire, you may fall into a deeper sleep.Asphyxiation is the leading cause of fire deaths, exceeding burns by a three-to-one ratio. You will be hurt by the flames and the smoke. Be aware of the damage.
";
    }
}
