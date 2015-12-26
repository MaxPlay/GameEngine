using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GameEngine.Font;

namespace FontGenerator
{
    /// <summary>
    /// Interaction logic for AdvancedEditor.xaml
    /// </summary>
    public partial class AdvancedEditor : Window
    {
        public List<CharElement> charElements;
        public Bitmap bitmapFont;
        private int SelectedChar;

        public AdvancedEditor()
        {
            InitializeComponent();
            this.lbItems.ItemsSource = this.charElements;
            this.lbItems.Items.Refresh();

            for (int i = 32; i < 127; i++)
            {
                cbCharselector.Items.Add((char)i);
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedChar == -1 || this.lbItems.Items.Count == 0)
                return;

            this.charElements.RemoveAt(lbItems.SelectedIndex);
            this.lbItems.ItemsSource = this.charElements;
            this.lbItems.Items.Refresh();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            this.charElements.Add(new CharElement(0, 0, 10, 10));
            this.lbItems.ItemsSource = this.charElements;
            this.lbItems.Items.Refresh();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.lbItems.ItemsSource = this.charElements;
            this.lbItems.Items.Refresh();
        }

        private void lbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshPreview();

            if (this.lbItems.SelectedIndex == -1)
            {
                if (this.lbItems.Items.Count == 0)
                    this.SelectedChar = -1;
            }
            else
                this.SelectedChar = this.lbItems.SelectedIndex;

            if (SelectedChar == -1 || this.lbItems.Items.Count == 0)
                return;

            this.tbLeft.Text = this.charElements[SelectedChar].position.X.ToString();
            this.tbTop.Text = this.charElements[SelectedChar].position.Y.ToString();
            this.tbWidth.Text = this.charElements[SelectedChar].dimension.X.ToString();
            this.tbHeight.Text = this.charElements[SelectedChar].dimension.Y.ToString();
            this.cbCharselector.SelectedItem = this.charElements[SelectedChar].Name;
        }

        private void RefreshPreview()
        {
            if (SelectedChar == -1 || this.lbItems.Items.Count == 0)
                return;

            MemoryStream ms = new MemoryStream();
            bitmapFont.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();

            RenderOptions.SetBitmapScalingMode(bi, BitmapScalingMode.NearestNeighbor);


            double dpiCorrectX = bi.DpiX / 96f;
            double dpiCorrectY = bi.DpiY / 96f;

            Console.WriteLine("DpiCorrection: x = {0}, y = {1}", dpiCorrectX, dpiCorrectY);

            imgBrush.ImageSource = bi;
            imgBrush.Viewbox = this.charElements[SelectedChar].ToRect(bi.Width * dpiCorrectX, bi.Height * dpiCorrectY);
            imgPreview.Width = this.charElements[SelectedChar].dimension.X;
            imgPreview.Height = this.charElements[SelectedChar].dimension.Y;
            imgPreviewBackground.Width = this.charElements[SelectedChar].dimension.X;
            imgPreviewBackground.Height = this.charElements[SelectedChar].dimension.Y;

#if DEBUG
            for (int i = 0; i < this.charElements.Count; i++)
                Console.WriteLine(i + this.charElements[i].position.ToString() + this.charElements[i].dimension.ToString());
#endif
        }

        private void Dimension_PreviewKeyInput(object sender, KeyEventArgs e)
        {
            bool KeysAllowed = ProcessKeys(e.Key);

            e.Handled = !(SelectedChar != -1 && KeysAllowed);
        }

        private bool ProcessKeys(Key key)
        {
            switch (key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Back:
                case Key.Delete:
                    return true;
                default:
                    return false;
            }
        }

        private void DimensionWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedChar == -1 || this.lbItems.Items.Count == 0)
                return;

            CharElement ce = this.charElements[SelectedChar];
            if (tbWidth.Text != string.Empty)
                ce.dimension.X = int.Parse(tbWidth.Text);
            else
                ce.dimension.X = 0;
            this.charElements[SelectedChar] = ce;

            this.RefreshPreview();
        }

        private void DimensionHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedChar == -1 || this.lbItems.Items.Count == 0)
                return;

            CharElement ce = this.charElements[SelectedChar];
            if (tbHeight.Text != string.Empty)
                ce.dimension.Y = int.Parse(tbHeight.Text);
            else
                ce.dimension.Y = 0;
            this.charElements[SelectedChar] = ce;

            this.RefreshPreview();
        }

        private void DimensionLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedChar == -1 || this.lbItems.Items.Count == 0)
                return;

            CharElement ce = this.charElements[SelectedChar];
            if (tbLeft.Text != string.Empty)
                ce.position.X = int.Parse(tbLeft.Text);
            else
                ce.position.X = 0;
            this.charElements[SelectedChar] = ce;

            this.RefreshPreview();
        }

        private void DimensionTop_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedChar == -1 || this.lbItems.Items.Count == 0)
                return;
            
            CharElement ce = this.charElements[SelectedChar];
            if (tbTop.Text != string.Empty)
                ce.position.Y = int.Parse(tbTop.Text);
            else
                ce.position.Y = 0;
            this.charElements[SelectedChar] = ce;

            this.RefreshPreview();
        }

        private void Background_Click(object sender, RoutedEventArgs e)
        {
            imgBackground.Color = ((SolidColorBrush)((Button)sender).Background).Color;
        }

        private void cbCharselector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedChar == -1 || this.lbItems.Items.Count == 0)
                return;

            CharElement ce = this.charElements[SelectedChar];
            ce.Name = (char)cbCharselector.SelectedItem;
            this.charElements[SelectedChar] = ce;

            this.lbItems.ItemsSource = this.charElements;
            this.lbItems.Items.Refresh();
        }
    }
}
