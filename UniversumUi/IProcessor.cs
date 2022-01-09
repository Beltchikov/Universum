using System.Threading.Tasks;

namespace UniversumUi
{
    public interface IProcessor
    {
        Task ProcessAsync(string apiUrl, string symbolsAsString, string separator, string decimalSeparator);
        
        delegate void MessageEventHandler(object sender, MessageEventArgs e);
    }
}