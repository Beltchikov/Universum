using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniversumUi
{
    public class Processor : IProcessor
    {
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        public event MessageEventHandler MessageEvent;

        public void Process(string text)
        {
            MessageEvent?.Invoke(this, new MessageEventArgs("Processing started"));

            Thread.Sleep(10000);

            MessageEvent?.Invoke(this, new MessageEventArgs("Message 1"));

            Thread.Sleep(10000);

            MessageEvent?.Invoke(this, new MessageEventArgs("Message 2"));
        }
    }
}
