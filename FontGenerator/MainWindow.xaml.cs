using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FontGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            tbCharacters.Text = @" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_.,:;-|<>()[]{}/\^+#*~";

            InstalledFontCollection installedFonts = new InstalledFontCollection();

            for (int i = 0; i < installedFonts.Families.Length; i++)
            {
                combFonts.Items.Add(installedFonts.Families[i].Name);
            }
            combFonts.SelectedItem = (object)"Arial";

            for (int i = 1; i <= 500; i++)
            {
                combSize.Items.Add(i);
            }
            combSize.SelectedIndex = 11;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<string> args = new List<string>();

            args.Add((string)combFonts.SelectedValue);
            args.Add("-s");
            args.Add((combSize.SelectedIndex + 1).ToString());
            args.Add("-c");
            args.Add(tbCharacters.Text);
            if (tbPreview.FontWeight == FontWeights.Bold)
                args.Add("-b");
            if (tbPreview.FontStyle == FontStyles.Italic)
                args.Add("-i");
            if (tbPreview.TextDecorations == TextDecorations.Underline)
                args.Add("-u");

            GameEngine.Font.Encoder enc = new GameEngine.Font.Encoder(args.ToArray());
        }


        private void UpdatePreview()
        {
            tbPreview.FontFamily = new FontFamily((string)combFonts.SelectedValue);
            tbPreview.FontSize = combSize.SelectedIndex + 1;
            tbPreview.FontWeight = (cbBold.IsChecked ?? false) ? FontWeights.Bold : FontWeights.Normal;
            tbPreview.FontStyle = (cbItalic.IsChecked ?? false) ? FontStyles.Italic : FontStyles.Normal;
            tbPreview.TextDecorations = (cbUnderline.IsChecked ?? false) ? TextDecorations.Underline : null;
        }

        private void checkbox_ValuesChanged(object sender, RoutedEventArgs e)
        {
            UpdatePreview();
        }

        private void combobox_ValuesChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }
    }
}
