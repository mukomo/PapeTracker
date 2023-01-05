using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Windows.Data;

namespace PapeTracker
{
    public partial class MainWindow : Window
    {
        private void HandleItemToggle(bool toggle, Item button, bool init)
        {
            if (toggle && button.IsEnabled == false)
            {
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
                if (!init)
                {
                    IncrementTotal();
                }
            }
            else if (toggle == false && button.IsEnabled == true)
            {
                button.IsEnabled = false;
                button.Visibility = Visibility.Hidden;
                DecrementTotal();

                button.HandleItemReturn();
            }
        }

        private void HandleWorldToggle(bool toggle, Button button, UniformGrid grid)
        {
            if (toggle && button.IsEnabled == false)
            {
                var outerGrid = (((button.Parent as Grid).Parent as Grid).Parent as Grid);
                int row = (int)((button.Parent as Grid).Parent as Grid).GetValue(Grid.RowProperty);
                outerGrid.RowDefinitions[row].Height = new GridLength(1, GridUnitType.Star);
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
            }
            else if (toggle == false && button.IsEnabled == true)
            {
                if (data.selected == button)
                {
                    data.WorldsData[button.Name].selectedBar.Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                    data.selected = null;
                }

                for (int i = grid.Children.Count - 1; i >= 0; --i)
                {
                    Item item = grid.Children[i] as Item;
                    item.HandleItemReturn();
                }

                var outerGrid = (((button.Parent as Grid).Parent as Grid).Parent as Grid);
                int row = (int)((button.Parent as Grid).Parent as Grid).GetValue(Grid.RowProperty);
                outerGrid.RowDefinitions[row].Height = new GridLength(0, GridUnitType.Star);
                button.IsEnabled = false;
                button.Visibility = Visibility.Collapsed;
            }
        }

        private void FryingPanToggle(object sender, RoutedEventArgs e)
        {
            FryingPanToggle(FryingPanOption.IsChecked);
        }

        private void FryingPanToggle(bool isChecked)
        {
            throw new NotImplementedException();
        }

        private void BlueHouseToggle(object sender, RoutedEventArgs e)
        {
            BlueHouseToggle(BlueHouseOption.IsChecked);
        }

        private void BlueHouseToggle(bool isChecked)
        {
            throw new NotImplementedException();
        }

        private void BowserCastleToggle(object sender, RoutedEventArgs e)
        {
            BowserCastleToggle(BowserCastleOption.IsChecked);
        }

        private void BowserCastleToggle(bool isChecked)
        {
            throw new NotImplementedException();
        }


        private void DragDropToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DragDrop = DragAndDropOption.IsChecked;
            data.dragDrop = DragAndDropOption.IsChecked;
            foreach (Item item in data.Items)
            {
                if (itemPools.Contains(item.Parent))
                {
                    if (data.dragDrop == false)
                    {
                        item.MouseDoubleClick -= item.Item_Click;
                        item.MouseMove -= item.Item_MouseMove;

                        item.MouseDown -= item.Item_MouseDown;
                        item.MouseDown += item.Item_MouseDown;
                        item.MouseUp -= item.Item_MouseUp;
                        item.MouseUp += item.Item_MouseUp;
                    }
                    else
                    {
                        item.MouseDoubleClick -= item.Item_Click;
                        item.MouseDoubleClick += item.Item_Click;
                        item.MouseMove -= item.Item_MouseMove;
                        item.MouseMove += item.Item_MouseMove;

                        item.MouseDown -= item.Item_MouseDown;
                        item.MouseUp -= item.Item_MouseUp;
                    }
                }
            }
        }

        private void TopMostToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TopMost = TopMostOption.IsChecked;
            Topmost = TopMostOption.IsChecked;
            broadcast.Topmost = TopMostOption.IsChecked;
        }

        //private void BroadcastStartupToggle(object sender, RoutedEventArgs e)
        //{
        //    Properties.Settings.Default.BroadcastStartup = BroadcastStartupOption.IsChecked;
        //    if (BroadcastStartupOption.IsChecked)
        //        broadcast.Show();
        //}
    }
}
