using System;

namespace AspSixApp.WebHelpers
{
    public class ModalSource
    {
        public string Title { get; set; }
        public string FooterText { get; set; }
        public string DialogId { get; set; }
        public Func<object> RenderBody { get; set; }
    }
}