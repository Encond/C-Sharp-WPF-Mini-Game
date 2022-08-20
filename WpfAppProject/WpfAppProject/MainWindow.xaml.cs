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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool pressed_item = false, pressed_map2 = false;
        private int row, col, row_span, col_span;
        private string uri_source;
        private string fullPath;
        private object save_element;
        private List<Grid> maps;

        public MainWindow()
        {
            InitializeComponent();
            maps = new List<Grid>();
            Start();
        }

        void SetPath()
        {
            string[] pathSplit = Directory.GetCurrentDirectory().Split('\\');

            for (int i = 0; i < pathSplit.Length - 2; i++)
                fullPath += pathSplit[i] + "\\";

            fullPath = fullPath.Insert(fullPath.Length, "Images\\");
        }

        private void SetDefaultValues()
        {
            row_span = 1;
            col_span = 1;

            maps.Add(new Grid());
            maps.Add(new Grid());

            for (int i = 0; i < 25; i++)
            {
                gridBoard.RowDefinitions.Add(new RowDefinition());
                gridBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }

            Start_borders(gridBoard);
            Start_borders(maps[0]);
            Start_borders(maps[1]);
        }

        private void Start()
        {
            SetPath();
            SetDefaultValues();
        }

        private void TextBlock_IsEnabled_True()
        {
            positionRow.IsEnabled = true;
            positionCol.IsEnabled = true;
        }

        private void TextBlock_IsEnabled_False()
        {
            positionRow.IsEnabled = false;
            positionCol.IsEnabled = false;
        }

        private void TextBlock_Text_NULL()
        {
            positionRow.Text = null;
            positionCol.Text = null;
        }

        private void Show_GridBorderLines()
        {
            for (int i = 0; i < gridBoard.Children.Count; i++)
            {
                if (!(gridBoard.Children[i] is Image))
                    gridBoard.Children[i].Opacity = 1;
            }
        }

        private void Hide_GridBorderLines()
        {
            for (int i = 0; i < gridBoard.Children.Count; i++)
            {
                if (!(gridBoard.Children[i] is Image))
                    gridBoard.Children[i].Opacity = 0;
            }
        }

        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pressed_item = true;
            //uri_source = $"/{((Image)sender).Source.ToString().Split('/')[10]}/{((Image)sender).Source.ToString().Split('/')[11]}";
            string temp = "/Images/" + ((Image)sender).Source.ToString().Split('/').Last();
            uri_source = temp;
            if (!pressed_map2)
            {
                GetItemSpanMap1(uri_source);
            }
            else
            {
                GetItemSpanMap2(uri_source);
            }

            Show_GridBorderLines();

            #region Foreach analog
            //foreach (var item in gridBoard.Children)
            //{
            //    if (((Border)item).BorderBrush == Brushes.Black)
            //    {
            //        ((Border)item).BorderBrush = Brushes.Green;
            //        ((Border)item).BorderThickness = new Thickness(2);
            //        Grid.SetColumnSpan((UIElement)item, 2);
            //        Grid.SetRowSpan((UIElement)item, 2);
            //    }
            //}
            #endregion

            #region Dynamic green position
            //for (int i = 0; i < gridBoard.Children.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        if (((Border)gridBoard.Children[i]).BorderBrush == Brushes.Black)
            //        {
            //            ((Border)gridBoard.Children[i]).BorderBrush = Brushes.Green;
            //            ((Border)gridBoard.Children[i]).BorderThickness = new Thickness(30);
            //            Grid.SetColumnSpan(gridBoard.Children[i], 2);
            //            Grid.SetRowSpan(gridBoard.Children[i], 2);
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        if (Grid.GetColumnSpan(gridBoard.Children[i - 1]) == 1 && Grid.GetRowSpan(gridBoard.Children[i - 1]) == 1)
            //        {
            //            if (((Border)gridBoard.Children[i]).BorderBrush == Brushes.Black)
            //            {
            //                ((Border)gridBoard.Children[i]).BorderBrush = Brushes.Green;
            //                ((Border)gridBoard.Children[i]).BorderThickness = new Thickness(2);
            //                Grid.SetColumnSpan(gridBoard.Children[i], 2);
            //                Grid.SetRowSpan(gridBoard.Children[i], 2);
            //                break;
            //            }
            //        }
            //    }
            //}
            #endregion
        }

        private void GetItemSpanMap1(string item)
        {
            if (item == ((Image)menuItems.Items[0]).Tag.ToString()) { row_span = 2; col_span = 2; }
            else if (item == ((Image)menuItems.Items[1]).Tag.ToString()) { row_span = 3; col_span = 2; }
            else if (item == ((Image)menuItems.Items[2]).Tag.ToString()) { row_span = 3; col_span = 2; }
            else if (item == ((Image)menuItems.Items[3]).Tag.ToString()) { row_span = 3; col_span = 2; }
            else if (item == ((Image)menuItems.Items[4]).Tag.ToString()) { row_span = 4; col_span = 4; }
            else if (item == ((Image)menuItems.Items[5]).Tag.ToString()) { row_span = 3; col_span = 2; }
            else if (item == ((Image)menuItems.Items[6]).Tag.ToString()) { row_span = 3; col_span = 2; }
            else if (item == ((Image)menuItems.Items[7]).Tag.ToString()) { row_span = 3; col_span = 3; }
            else if (item == ((Image)menuItems.Items[8]).Tag.ToString()) { row_span = 4; col_span = 2; }
            else if (item == ((Image)menuItems.Items[9]).Tag.ToString()) { row_span = 4; col_span = 3; }
        }

        private void GetItemSpanMap2(string item)
        {
            if (item == ((Image)menuItems.Items[0]).Tag.ToString()) { row_span = 2; col_span = 2; }
            else if (item == ((Image)menuItems.Items[1]).Tag.ToString()) { row_span = 2; col_span = 2; }
            else if (item == ((Image)menuItems.Items[2]).Tag.ToString()) { row_span = 2; col_span = 2; }
            else if (item == ((Image)menuItems.Items[3]).Tag.ToString()) { row_span = 6; col_span = 4; }
            else if (item == ((Image)menuItems.Items[4]).Tag.ToString()) { row_span = 1; col_span = 2; }
        }

        private void Start_borders(Grid item)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    Border border = new Border() { BorderBrush = Brushes.Black, BorderThickness = new Thickness(0.5) };
                    border.Tag = $"{i}|{j}";
                    border.Opacity = 0;
                    border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
                    border.MouseRightButtonDown += Border_MouseRightButtonDown;
                    border.MouseEnter += Border_MouseEnter;
                    border.MouseLeave += Border_MouseLeave;

                    item.Children.Add(border);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                }
            }
        }

        private void ChangeMap(Grid save, Grid changeTo)
        {
            int saveCount_childrens = gridBoard.Children.Count;
            int countMinus_afterDelete = 0;
            for (int i = 0; i < saveCount_childrens; i++)
            {
                UIElement temp = gridBoard.Children[i - countMinus_afterDelete]; // Save element to bypass the Exception is arleady child
                gridBoard.Children.RemoveAt(i - countMinus_afterDelete); // Remove used child
                save.Children.Add(temp); // Save in slot
                countMinus_afterDelete++; // Number of deleted childs
            }

            saveCount_childrens = changeTo.Children.Count;
            countMinus_afterDelete = 0;
            gridBoard.Children.Clear();
            for (int i = 0; i < saveCount_childrens; i++)
            {
                UIElement temp = changeTo.Children[i - countMinus_afterDelete];
                changeTo.Children.RemoveAt(i - countMinus_afterDelete);
                gridBoard.Children.Add(temp);
                countMinus_afterDelete++;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            pressed_map2 = false;

            border1.IsEnabled = false;
            border2.IsEnabled = true;
            ChangeMap(maps[0], maps[1]);
            TextBlock_Text_NULL();
            TextBlock_IsEnabled_False();

            UpdateMap_Items(10, 10);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            pressed_map2 = true;

            border2.IsEnabled = false;
            border1.IsEnabled = true;
            ChangeMap(maps[1], maps[0]);
            TextBlock_Text_NULL();
            TextBlock_IsEnabled_False();

            UpdateMap_Items(0, 5);
        }

        private void UpdateMap_Items(int at, int end)
        {
            ImageSourceConverter imsc = new ImageSourceConverter();
            ImageBrush imageBrush = new ImageBrush();

            string source = null;
            if (pressed_map2) { source = "https://img.freepik.com/free-vector/sky-day-game-background_7814-306.jpg?size=626&ext=jpg"; }
            else { source = "https://cutewallpaper.org/24/room-png/room-wood-png-v61-photo-big-definition.png"; }

            imageBrush.ImageSource = (ImageSource)imsc.ConvertFromString(source);
            gridBoard.Background = imageBrush;

            menuItems.Items.Clear();
            int temp = at;
            for (int i = 0; i < end; i++)
            {
                temp++;
                Image image = new Image() { Source = (ImageSource)imsc.ConvertFromString($"{fullPath}{temp}.png") };
                image.Stretch = Stretch.Fill;
                image.MouseLeftButtonDown += Temp_image_MouseLeftButtonDown;
                image.Tag = $"/Images/{temp}.png";

                Style style = (Style)Resources["menuImage"];
                image.Style = style;

                menuItems.Items.Add(image);
                ((Image)menuItems.Items[i]).MouseLeftButtonDown += Item_MouseLeftButtonDown;
            }
        }

        private string[] GetRow_and_Columns(object sender)
        {
            string tag = null;
            if (sender.GetType() == typeof(Border))
            {
                tag = ((Border)sender).Tag.ToString();
            }
            else if (sender.GetType() == typeof(Image))
            {
                tag = ((Image)sender).Tag.ToString();
            }
            string[] pars = new string[tag.Split('|').Length];
            for (int i = 0; i < pars.Length; i++)
                pars[i] = tag.Split('|')[i];

            return pars;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            row = Convert.ToInt32(GetRow_and_Columns(sender)[0]);
            col = Convert.ToInt32(GetRow_and_Columns(sender)[1]);

            if (((Border)sender).Background != Brushes.Green && pressed_item)
            {
                ((Border)sender).Background = Brushes.Green;
                Grid.SetRowSpan((UIElement)sender, row_span);
                Grid.SetColumnSpan((UIElement)sender, col_span);
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = Brushes.Transparent;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pressed_item)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(uri_source, UriKind.Relative);
                bitmapImage.EndInit();

                ((Border)sender).Background = Brushes.Transparent;

                Image temp_image = new Image() { Source = bitmapImage };
                temp_image.Stretch = Stretch.Fill;
                temp_image.Tag = ((Border)sender).Tag;
                temp_image.MouseLeftButtonDown += Temp_image_MouseLeftButtonDown;

                gridBoard.Children.Add(temp_image);
                Grid.SetRow(temp_image, row);
                Grid.SetColumn(temp_image, col);
                Grid.SetRowSpan(temp_image, row_span);
                Grid.SetColumnSpan(temp_image, col_span);

                save_element = temp_image;
                positionRow.Text = row.ToString();
                positionCol.Text = col.ToString();

                TextBlock_IsEnabled_True();
                Hide_GridBorderLines();
            }
            else
            {
                TextBlock_Text_NULL();
                TextBlock_IsEnabled_False();
            }
            pressed_item = false;
        }

        private void Temp_image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock_Text_NULL();
            if (togglebuttonRemove.IsChecked.Value == true)
            {
                for (int i = 0; i < gridBoard.Children.Count; i++)
                {
                    if (e.Source == gridBoard.Children[i])
                    {
                        gridBoard.Children.Remove(gridBoard.Children[i]);
                        break;
                    }
                }
                TextBlock_Text_NULL();
                TextBlock_IsEnabled_False();
            }
            else if (togglebuttonRemove.IsChecked.Value == false)
            {
                for (int i = 0; i < gridBoard.Children.Count; i++)
                {
                    if (e.Source == gridBoard.Children[i])
                    {
                        save_element = sender;
                        positionRow.Text = GetRow_and_Columns(sender)[0];
                        positionCol.Text = GetRow_and_Columns(sender)[1];
                        break;
                    }
                }
                TextBlock_IsEnabled_True();
            }
        }

        private void Border_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            pressed_item = false;
            Hide_GridBorderLines();
        }

        private void buttonClearAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < gridBoard.Children.Count; i++)
            {
                if (gridBoard.Children[i] is Image)
                {
                    gridBoard.Children.Remove(gridBoard.Children[i]);
                    i--;
                }
            }
            TextBlock_Text_NULL();
            TextBlock_IsEnabled_False();
        }

        private void Change_Element_Position()
        {
            if (!String.IsNullOrEmpty(positionRow.Text) && !String.IsNullOrEmpty(positionCol.Text) && save_element != null)
            {
                if (Convert.ToInt32(positionRow.Text) >= 0 && Convert.ToInt32(positionRow.Text) <= 25
                   && Convert.ToInt32(positionCol.Text) >= 0 && Convert.ToInt32(positionCol.Text) <= 25)
                {
                    int r = Convert.ToInt32(positionRow.Text);
                    int c = Convert.ToInt32(positionCol.Text);

                    Grid.SetRow((UIElement)save_element, r);
                    Grid.SetColumn((UIElement)save_element, c);

                    foreach (var item in gridBoard.Children)
                    {
                        if (item == save_element)
                            ((Image)item).Tag = $"{r}|{c}";
                    }
                }
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Change_Element_Position();
        }
    }
}