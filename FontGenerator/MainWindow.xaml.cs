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
using GameEngine.Font;
using System.Xml.Serialization;
using System.Xml;

namespace FontGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap bitmapFont;
        List<CharElement> charElements;

        public MainWindow()
        {
            InitializeComponent();

            tbCharacters.Text = @" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_.,:;-|<>()[]{}/\^+#*~";
            tbCharacters2.Text = @" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_.,:;-|<>()[]{}/\^+#*~";

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

            charElements = new List<CharElement>();
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
                    this.Height = 419.6;
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
            tbCharCount.IsEnabled = false;
            btnAE.IsEnabled = false;
            tbCharacters2.IsEnabled = true;
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            grd_UCD.IsEnabled = false;
            tbCharCount.IsEnabled = true;
            btnAE.IsEnabled = true;
            tbCharacters2.IsEnabled = false;
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
                Console.WriteLine("Bitmap {0} loaded with size {1}.", fd.FileName, bitmapFont.Size.ToString());
            }

            btnPreview.IsEnabled = (bitmapFont != null);
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(tbResource.Text))
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

        private void btnAE_Click(object sender, RoutedEventArgs e)
        {
            if (bitmapFont == null)
            {
                MessageBox.Show(@"You need to load an image to use the advanced editor.");
                return;
            }

            AdvancedEditor ae = new AdvancedEditor();
            ae.charElements = charElements;
            ae.bitmapFont = bitmapFont;
            ae.ShowDialog();

            this.charElements = ae.charElements;
            this.tbCharCount.Text = this.charElements.Count.ToString();
        }

        private void btnSave2_Click(object sender, RoutedEventArgs e)
        {
            if (bitmapFont == null)
            {
                MessageBox.Show(@"You need to load an image to save the bitmap font.");
                return;
            }
            if (!this.grd_UCD.IsEnabled && charElements.Count == 0)
            {
                MessageBox.Show(@"You need to add elements in the advanced editor if you want to use the individual character dimension.");
                return;
            }


            GameEngine.Font.BitmapEncoder encoder = new GameEngine.Font.BitmapEncoder();

            if (this.grd_UCD.IsEnabled)
            {
                List<CharElement> characterlist = new List<CharElement>();

                int width = int.Parse(this.tbX_ud.Text);
                int height = int.Parse(this.tbY_ud.Text);
                int x = 0;
                int y = 0;
                for (int i = 0; i < this.tbCharacters2.Text.Length; i++)
                {
                    if (bitmapFont.Width < x + width)
                    {
                        y += height;
                        x = 0;
                    }

                    CharElement ce = new CharElement(this.tbCharacters2.Text[i], x, y, width, height);

                    characterlist.Add(ce);
                    x += width;

                    if (y > bitmapFont.Height)
                        break;
                }

                encoder.Chars = characterlist;
            }
            else
            {
                encoder.Chars = this.charElements;
            }
            encoder.Source = this.bitmapFont;
            encoder.Encode(tbFontname.Text);
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml Files (*.xml)|*.xml";
            ofd.AddExtension = true;
            bool? dialogresult = ofd.ShowDialog();

            if (dialogresult.HasValue ? (bool)dialogresult : false)
            {
                using (XmlReader reader = XmlReader.Create(File.OpenRead(ofd.FileName)))
                {
                    charElements.Clear();
                    CharElement element = new CharElement();
                    while (reader.Read())
                    {
                        if (reader.Name == "CharElement" && reader.NodeType == XmlNodeType.EndElement)
                            charElements.Add(element);

                        if (reader.NodeType == XmlNodeType.Element)
                            switch (reader.Name)
                            {
                                case "Name":
                                    reader.Read();
                                    element.Name = (char)int.Parse(reader.Value);
                                    break;
                                case "Position":
                                    element.position = ReadPoint(reader);
                                    break;
                                case "Dimension":
                                    element.dimension = ReadPoint(reader);
                                    break;
                            }
                    }
                }
                tbCharCount.Text = this.charElements.Count.ToString();
#if DEBUG
                Console.WriteLine("Loading complete.");
#endif
            }
        }

        private System.Drawing.Point ReadPoint(XmlReader reader)
        {
            reader.Read();
            System.Drawing.Point p = new System.Drawing.Point();
            reader.Read();
            p.X = int.Parse(reader.Value);
            reader.Read();
            reader.Read();
            reader.Read();
            p.Y = int.Parse(reader.Value);

            return p;
        }

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.Filter = "xml Files (*.xml)|*.xml";
            bool? dialogresult = sfd.ShowDialog();

            if (dialogresult.HasValue ? (bool)dialogresult : false)
            {
                using (XmlWriter writer = XmlWriter.Create(File.Create(sfd.FileName)))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("CharElementList");

                    for (int i = 0; i < charElements.Count; i++)
                    {
                        //Write new CharElement
                        writer.WriteStartElement("CharElement");

                        //Write the name of the Element
                        writer.WriteStartElement("Name");
                        writer.WriteValue(charElements[i].Name);
                        writer.WriteEndElement();

                        //Write the position of the Element
                        writer.WriteStartElement("Position");

                        //Point.X
                        writer.WriteStartElement("X");
                        writer.WriteValue(charElements[i].position.X);
                        writer.WriteEndElement();

                        //Point.Y
                        writer.WriteStartElement("Y");
                        writer.WriteValue(charElements[i].position.Y);
                        writer.WriteEndElement();

                        //Position EndElement
                        writer.WriteEndElement();

                        //Write the dimension of the Element
                        writer.WriteStartElement("Dimension");

                        //Point.X
                        writer.WriteStartElement("X");
                        writer.WriteValue(charElements[i].dimension.X);
                        writer.WriteEndElement();

                        //Point.Y
                        writer.WriteStartElement("Y");
                        writer.WriteValue(charElements[i].dimension.Y);
                        writer.WriteEndElement();

                        //Dimension EndElement
                        writer.WriteEndElement();

                        //CharElement EndElement
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
#if DEBUG
                Console.WriteLine("Saving complete.");
#endif
            }
        }
    }
}