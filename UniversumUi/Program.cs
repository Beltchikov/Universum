using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversumUi
{
    internal static class Program
    {
        static readonly HttpClient httpClient = new HttpClient();

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var processor = new Processor(httpClient);
            
            Application.Run(new MainForm(processor));
        }
    }
}
