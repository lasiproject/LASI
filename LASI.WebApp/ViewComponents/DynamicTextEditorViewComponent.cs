using System.Threading.Tasks;
using LASI.Content;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using Microsoft.AspNet.Mvc;

namespace LASI.WebApp.ViewComponents
{
    public class DynamicTextEditorViewComponent : ViewComponent
    {
        private readonly IDocumentAccessor<UserDocument> documentProvider;
        private readonly Content.Tagging.Tagger tagger = new Content.Tagging.Tagger(TaggerInterop.TaggerMode.TagAndAggregate);
        public DynamicTextEditorViewComponent(IDocumentAccessor<UserDocument> documentProvider)
        {
            this.documentProvider = documentProvider;
        }
        public async Task<IViewComponentResult> InvokeAsync() => await Task.FromResult(View(new DynamicallyEnteredTextFragment()));

        public async Task<ITaggedTextSource> TagTextAsync(DynamicallyEnteredTextFragment text) => await tagger.TaggedFromRawAsync(text);

        public class DynamicallyEnteredTextFragment : IRawTextSource
        {
            public string Name => "Dynamic Free Text";
            public string Text { get; set; }
            public string LoadText() => Text;
            public Task<string> LoadTextAsync() => Task.FromResult(Text);
        }
    }
}