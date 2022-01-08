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

            // Test data
            var testData = "OKE\r\nSNCY\r\nCSX\r\nNS";
            txtSymbols.Text=testData;
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
            new Thread(() => _processor.ProcessAsync(symbolsAsText))
            { IsBackground = true }
            .Start();


        }
    }
}
