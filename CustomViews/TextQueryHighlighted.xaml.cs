using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CustomViews
{
    /// <summary>
    /// Interaction logic for TextQueryHighlighted.xaml
    /// </summary>
    public partial class TextQueryHighlighted : Window
    {
        public TextQueryHighlighted()
        {
            InitializeComponent();
        }

        public TextQueryHighlighted(string ocrPath, string query) : this()
        {
            var sr = new StreamReader(ocrPath);
            txtBlock.Text = sr.ReadToEnd();
        }
    }
}
