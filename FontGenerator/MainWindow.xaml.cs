using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;

namespace FontGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap bitmapFont;

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
            tbPreview.FontFamily = new System.Windows.Media.FontFamily((string)combFonts.SelectedValue);
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

        private void MainTabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (MainTabcontrol.SelectedIndex)
            {
                case 0:
                    this.Height = 231.2;
                    this.Width = 547.6;
                    break;
                case 1:
                    this.Height = 1000;
                    this.Width = 547.6;
                    break;
            }
        }

        private void sldY_ud_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbY_ud.Text = sldY_ud.Value.ToString("F0");
        }

        private void sldX_ud_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbX_ud.Text = sldX_ud.Value.ToString("F0");
        }

        private void tb_ud_TextChanged(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^(\d)+$");
            e.Handled = !(regex.IsMatch(e.Text) && ((TextBox)sender).Text.Length < 4);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            grd_UCD.IsEnabled = true;
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            grd_UCD.IsEnabled = false;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "BMP Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|TIFF Files (*.tiff)|*.tiff";
            bool? success = fd.ShowDialog(); 
            if (success.HasValue && success.Value)
            {
                bitmapFont = (Bitmap)Bitmap.FromStream(fd.OpenFile());
                tbResource.Text = fd.FileName;
            }

            btnPreview.IsEnabled = (bitmapFont != null);
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            if(File.Exists(tbResource.Text))
            {
                bitmapFont = (Bitmap)Bitmap.FromStream(File.OpenRead(tbResource.Text));
            }

            btnPreview.IsEnabled = (bitmapFont != null);
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            PreviewWindow preview = new PreviewWindow();
            preview.bitmapFont = this.bitmapFont;
            preview.ShowDialog();
        }
    }
}
