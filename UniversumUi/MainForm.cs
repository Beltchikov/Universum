using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversumUi
{
    public partial class MainForm : Form
    {
        private IProcessor _processor;

        public MainForm(IProcessor processor)
        {
            InitializeComponent();
            _processor = processor;
        }

        private void btGo_Click(object sender, EventArgs e)
        {
            _processor.Process(txtSymbols.Text);
        }
    }
}
