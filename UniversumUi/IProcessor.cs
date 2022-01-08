using System.Threading.Tasks;

namespace UniversumUi
{
    public interface IProcessor
    {
        Task ProcessAsync(string apiUrl, string symbolsAsString);
        
        delegate void MessageEventHandler(object sender, MessageEventArgs e);
    }
}