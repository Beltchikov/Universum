using System.Threading.Tasks;

namespace UniversumUi
{
    public interface IProcessor
    {
        Task ProcessAsync(string text);
        
        delegate void MessageEventHandler(object sender, MessageEventArgs e);
    }
}