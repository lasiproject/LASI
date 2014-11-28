namespace LASI.Core
{
    public interface IPrepositonLinkable
    {
        double MetaWeight { get; set; }
        string Text { get; }
        double Weight { get; set; }
    }
}