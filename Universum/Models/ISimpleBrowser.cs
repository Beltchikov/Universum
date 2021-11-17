namespace Universum.Models
{
    public interface ISimpleBrowser
    {
        string OneValueResult(string url, string pattern1, string pattern2);
    }
}