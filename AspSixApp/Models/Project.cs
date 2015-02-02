namespace AspSixApp.Models
{
    public class Project
    {
        public System.Guid Id { get; set; }
        public System.Collections.Generic.IEnumerable<UserDocument> SourceTexts { get; set; }
    }
}