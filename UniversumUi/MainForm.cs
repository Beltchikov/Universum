using System;
using System.Threading;
using System.Windows.Forms;

namespace UniversumUi
{
    public partial class MainForm : Form
    {
        private Processor _processor;

        public MainForm(IProcessor processor)
        {
            InitializeComponent();

            if(processor is Processor processorNullSafe)
            {
                _processor = processorNullSafe;
                _processor.MessageEvent += _processor_MessageEvent;
            }
            else
            {
                throw new ApplicationException("Unexpected: _processor is null ");
            }
            
            // Test data
            var testData = "OKE\r\nSNCY\r\nCSX\r\nNS";
            txtSymbols.Text = testData;
        }

        private void _processor_MessageEvent(object sender, MessageEventArgs e)
        {
            Invoke(new Action(() =>
            {
                txtResults.Text += e.Message + Environment.NewLine;
            }));
        }

        private void btGo_Click(object sender, EventArgs e)
        {
            var symbolsAsText = txtSymbols.Text;
            var apiUrl = txtHost.Text;
            var separator = txtSeparator.Text;
            var decimalSeparator = txtDecimalSeparator.Text;

            new Thread(async () => await _processor.ProcessAsync(apiUrl, symbolsAsText, separator, decimalSeparator))
            { IsBackground = true }
            .Start();
        }
    }
}
