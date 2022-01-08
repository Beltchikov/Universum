using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversumUi
{
    public partial class MainForm : Form
    {
        private Processor _processor;

        public MainForm(IProcessor processor)
        {
            InitializeComponent();
            _processor = processor as Processor;

            _processor.MessageEvent += _processor_MessageEvent;
        }

        private void _processor_MessageEvent(object sender, MessageEventArgs e)
        {
            Invoke(new Action(() =>
            {
                var oldText = txtResults.Text;
                var newText = e.Message + Environment.NewLine + oldText;
                txtResults.Text = newText;
            }));
        }

        private void btGo_Click(object sender, EventArgs e)
        {
            var symbolsAsText = txtSymbols.Text;

            // Start input thread
            new Thread(() => _processor.Process(symbolsAsText))
            { IsBackground = true }
            .Start();


        }
    }
}
