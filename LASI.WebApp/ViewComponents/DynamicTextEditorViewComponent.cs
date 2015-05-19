using System.Threading.Tasks;
using LASI.Content;
using LASI.Content.Tagging;
using LASI.WebApp.CustomIdentity;
using LASI.WebApp.Models;
using Microsoft.AspNet.Mvc;

namespace LASI.WebApp.ViewComponents
{
    public class DynamicTextEditorViewComponent : ViewComponent
    {
        [Activate]
        private IDocumentProvider<UserDocument> DocumentStore { get; set; }
        [Activate]
        private Tagger Tagger { get; set; }

        public async Task<IViewComponentResult> InvokeAsync() => await Task.FromResult(View(new DynamicallyEnteredTextFragment()));

        public async Task<ITaggedTextSource> TagTextAsync(DynamicallyEnteredTextFragment text) => await Tagger.TaggedFromRawAsync(text);

        public class DynamicallyEnteredTextFragment : IRawTextSource
        {
            public string Name => "Dynamic Free Text";
            public string Text { get; set; }
            public string GetText() => Text;
            public async Task<string> GetTextAsync() => await Task.FromResult(Text);
        }
    }
}