namespace Universum.Models
{
    public interface IBrowserWrapper
    {
        public bool Navigate(string url);
        public string Text { get; }
    }
}