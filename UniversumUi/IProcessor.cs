namespace UniversumUi
{
    public interface IProcessor
    {
        void Process(string text);

        //delegate void MessageEventHandler(object sender, MessageEventArgs e);

        //event MessageEventHandler MessageEvent;
    }
}