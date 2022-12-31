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

        private void PromiseCharmToggle(object sender, RoutedEventArgs e)
        {
            PromiseCharmToggle(PromiseCharmOption.IsChecked);
        }

        private void PromiseCharmToggle(bool toggle)
        {
            Properties.Settings.Default.PromiseCharm = toggle;
            PromiseCharmOption.IsChecked = toggle;
            if (toggle)
                broadcast.PromiseCharm.Visibility = Visibility.Visible;
            else
                broadcast.PromiseCharm.Visibility = Visibility.Hidden;
            //HandleItemToggle(toggle, PromiseCharm, false);
        }

        private void ReportsToggle(object sender, RoutedEventArgs e)
        {
            ReportsToggle(ReportsOption.IsChecked);
        }

        private void ReportsToggle(bool toggle)
        {
            Properties.Settings.Default.AnsemReports = toggle;
            ReportsOption.IsChecked = toggle;
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(toggle, data.Reports[i], false);
            }
        }

        private void AbilitiesToggle(object sender, RoutedEventArgs e)
        {
            AbilitiesToggle(AbilitiesOption.IsChecked);
        }

        private void AbilitiesToggle(bool toggle)
        {
            Properties.Settings.Default.Abilities = toggle;
            AbilitiesOption.IsChecked = toggle;
            //HandleItemToggle(toggle, OnceMore, false);
            //HandleItemToggle(toggle, SecondChance, false);
        }

        private void TornPagesToggle(object sender, RoutedEventArgs e)
        {
            TornPagesToggle(TornPagesOption.IsChecked);
        }

        private void TornPagesToggle(bool toggle)
        {
            Properties.Settings.Default.TornPages = toggle;
            TornPagesOption.IsChecked = toggle;
            for (int i = 0; i < data.TornPages.Count; ++i)
            {
                HandleItemToggle(toggle, data.TornPages[i], false);
            }
        }

        private void CureToggle(object sender, RoutedEventArgs e)
        {
            CureToggle(CureOption.IsChecked);
        }

        private void CureToggle(bool toggle)
        {
            Properties.Settings.Default.Cure = toggle;
            CureOption.IsChecked = toggle;
            //HandleItemToggle(toggle, Cure1, false);
            //HandleItemToggle(toggle, Cure2, false);
            //HandleItemToggle(toggle, Cure3, false);
        }

        private void FinalFormToggle(object sender, RoutedEventArgs e)
        {
            FinalFormToggle(FinalFormOption.IsChecked);
        }

        private void FinalFormToggle(bool toggle)
        {
            Properties.Settings.Default.FinalForm = toggle;
            FinalFormOption.IsChecked = toggle;
            //HandleItemToggle(toggle, Final, false);
        }

        private void SoraHeartToggle(object sender, RoutedEventArgs e)
        {
            SoraHeartToggle(SoraHeartOption.IsChecked);
        }

        private void SoraHeartToggle(bool toggle)
        {
            Properties.Settings.Default.SoraHeart = toggle;
            SoraHeartOption.IsChecked = toggle;
            HandleWorldToggle(toggle, GoombaRegion, GoombaRegionGrid);
        }

        private void SimulatedToggle(object sender, RoutedEventArgs e)
        {
            SimulatedToggle(SimulatedOption.IsChecked);
        }

        private void SimulatedToggle(bool toggle)
        {
            Properties.Settings.Default.Simulated = toggle;
            SimulatedOption.IsChecked = toggle;
            HandleWorldToggle(toggle, ToadTownTunnels, ToadTownTunnelsGrid);
        }

        private void ForeverForestToggle(object sender, RoutedEventArgs e)
        {
            ForeverForestToggle(ForeverForestOption.IsChecked);
        }

        private void ForeverForestToggle(bool toggle)
        {
            Properties.Settings.Default.HundredAcre = toggle;
            ForeverForestOption.IsChecked = toggle;
            HandleWorldToggle(toggle, ForeverForest, ForeverForestGrid);
        }

        private void ShiverMountainToggle(object sender, RoutedEventArgs e)
        {
            ShiverMountainToggle(ShiverMountainOption.IsChecked);
        }

        private void ShiverMountainToggle(bool toggle)
        {
            Properties.Settings.Default.ShiverMountain = toggle;
            ShiverMountainOption.IsChecked = toggle;
            HandleWorldToggle(toggle, ShiverMountain, ShiverMountainGrid);
        }

        private void SimpleToggle(object sender, RoutedEventArgs e)
        {
            // Mimicing radio buttons so you cant toggle a button off
            if (SimpleOption.IsChecked == false)
            {
                SimpleOption.IsChecked = true;
                return;
            }

            //OrbOption.IsChecked = false;
            //AltOption.IsChecked = false;
            //Properties.Settings.Default.Simple = SimpleOption.IsChecked;
            //Properties.Settings.Default.Orb = OrbOption.IsChecked;
            //Properties.Settings.Default.Alt = AltOption.IsChecked;

            if (SimpleOption.IsChecked)
            {
                //Report1.SetResourceReference(ContentProperty, "AnsemReport1");
                //Report2.SetResourceReference(ContentProperty, "AnsemReport2");
                //Report3.SetResourceReference(ContentProperty, "AnsemReport3");
                //Report4.SetResourceReference(ContentProperty, "AnsemReport4");
                //Report5.SetResourceReference(ContentProperty, "AnsemReport5");
                //Report6.SetResourceReference(ContentProperty, "AnsemReport6");
                //Report7.SetResourceReference(ContentProperty, "AnsemReport7");
                //Report8.SetResourceReference(ContentProperty, "AnsemReport8");
                //Report9.SetResourceReference(ContentProperty, "AnsemReport9");
                //Report10.SetResourceReference(ContentProperty, "AnsemReport10");
                //Report11.SetResourceReference(ContentProperty, "AnsemReport11");
                //Report12.SetResourceReference(ContentProperty, "AnsemReport12");
                //Report13.SetResourceReference(ContentProperty, "AnsemReport13");
                //Fire1.SetResourceReference(ContentProperty, "Fire");
                //Fire2.SetResourceReference(ContentProperty, "Fire");
                //Fire3.SetResourceReference(ContentProperty, "Fire");
                //Blizzard1.SetResourceReference(ContentProperty, "Blizzard");
                //Blizzard2.SetResourceReference(ContentProperty, "Blizzard");
                //Blizzard3.SetResourceReference(ContentProperty, "Blizzard");
                //Thunder1.SetResourceReference(ContentProperty, "Thunder");
                //Thunder2.SetResourceReference(ContentProperty, "Thunder");
                //Thunder3.SetResourceReference(ContentProperty, "Thunder");
                //Cure1.SetResourceReference(ContentProperty, "Cure");
                //Cure2.SetResourceReference(ContentProperty, "Cure");
                //Cure3.SetResourceReference(ContentProperty, "Cure");
                //Reflect1.SetResourceReference(ContentProperty, "Reflect");
                //Reflect2.SetResourceReference(ContentProperty, "Reflect");
                //Reflect3.SetResourceReference(ContentProperty, "Reflect");
                //Magnet1.SetResourceReference(ContentProperty, "Magnet");
                //Magnet2.SetResourceReference(ContentProperty, "Magnet");
                //Magnet3.SetResourceReference(ContentProperty, "Magnet");

                //Valor.SetResourceReference(ContentProperty, "Valor");
                //Wisdom.SetResourceReference(ContentProperty, "Wisdom");
                //Limit.SetResourceReference(ContentProperty, "Limit");
                //Master.SetResourceReference(ContentProperty, "Master");
                //Final.SetResourceReference(ContentProperty, "Final");

                //ValorM.SetResourceReference(ContentProperty, "Valor");
                //WisdomM.SetResourceReference(ContentProperty, "Wisdom");
                //LimitM.SetResourceReference(ContentProperty, "Limit");
                //MasterM.SetResourceReference(ContentProperty, "Master");
                //FinalM.SetResourceReference(ContentProperty, "Final");

                //TornPage1.SetResourceReference(ContentProperty, "TornPage");
                //TornPage2.SetResourceReference(ContentProperty, "TornPage");
                //TornPage3.SetResourceReference(ContentProperty, "TornPage");
                //TornPage4.SetResourceReference(ContentProperty, "TornPage");
                //TornPage5.SetResourceReference(ContentProperty, "TornPage");
                //Lamp.SetResourceReference(ContentProperty, "Genie");
                //Ukulele.SetResourceReference(ContentProperty, "Stitch");
                //Baseball.SetResourceReference(ContentProperty, "ChickenLittle");
                //Feather.SetResourceReference(ContentProperty, "PeterPan");
                //Nonexistence.SetResourceReference(ContentProperty, "ProofOfNonexistence");
                //Connection.SetResourceReference(ContentProperty, "ProofOfConnection");
                //Peace.SetResourceReference(ContentProperty, "ProofOfPeace");
                //PromiseCharm.SetResourceReference(ContentProperty, "PromiseCharm");

                broadcast.Report.SetResourceReference(ContentProperty, "AnsemReport");
                broadcast.Peace.SetResourceReference(ContentProperty, "ProofOfPeace");
                broadcast.Nonexistence.SetResourceReference(ContentProperty, "ProofOfNonexistence");
                broadcast.Connection.SetResourceReference(ContentProperty, "ProofOfConnection");
                broadcast.PromiseCharm.SetResourceReference(ContentProperty, "PromiseCharm");
                broadcast.Fire.SetResourceReference(ContentProperty, "Fire");
                broadcast.Blizzard.SetResourceReference(ContentProperty, "Blizzard");
                broadcast.Thunder.SetResourceReference(ContentProperty, "Thunder");
                broadcast.Cure.SetResourceReference(ContentProperty, "Cure");
                broadcast.Reflect.SetResourceReference(ContentProperty, "Reflect");
                broadcast.Magnet.SetResourceReference(ContentProperty, "Magnet");
                broadcast.Valor.SetResourceReference(ContentProperty, "Valor");
                broadcast.Wisdom.SetResourceReference(ContentProperty, "Wisdom");
                broadcast.Limit.SetResourceReference(ContentProperty, "Limit");
                broadcast.Master.SetResourceReference(ContentProperty, "Master");
                broadcast.Final.SetResourceReference(ContentProperty, "Final");
                broadcast.Baseball.SetResourceReference(ContentProperty, "ChickenLittle");
                broadcast.Lamp.SetResourceReference(ContentProperty, "Genie");
                broadcast.Ukulele.SetResourceReference(ContentProperty, "Stitch");
                broadcast.Feather.SetResourceReference(ContentProperty, "PeterPan");

                ((Grid)((Grid)broadcast.Fire.Parent).Parent).RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                ((Grid)((Grid)broadcast.Fire.Parent).Parent).RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                ((Grid)broadcast.Valor.Parent).RowDefinitions[1].Height = new GridLength(2.2, GridUnitType.Star);
                ((Grid)broadcast.Valor.Parent).RowDefinitions[2].Height = new GridLength(2.2, GridUnitType.Star);
                ((Grid)broadcast.Lamp.Parent).RowDefinitions[1].Height = new GridLength(4.4, GridUnitType.Star);
            }
        }

        private void WorldProgressToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WorldProgress = WorldProgressOption.IsChecked;
            if (WorldProgressOption.IsChecked)
            {
                broadcast.ToggleProgression(true);

                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    if (data.WorldsData[key].progression != null)
                        data.WorldsData[key].progression.Visibility = Visibility.Visible;

                    data.WorldsData[key].top.ColumnDefinitions[0].Width = new GridLength(1.5, GridUnitType.Star);
                    data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(3.3, GridUnitType.Star);

                    Grid grid = data.WorldsData[key].world.Parent as Grid;
                    grid.ColumnDefinitions[0].Width = new GridLength(3.5, GridUnitType.Star);
                    grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                    grid.ColumnDefinitions[2].Width = new GridLength(2, GridUnitType.Star);
                    Grid.SetColumnSpan(data.WorldsData[key].world, 2);
                }
            }
            else
            {
                broadcast.ToggleProgression(false);

                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    if (data.WorldsData[key].progression != null)
                        data.WorldsData[key].progression.Visibility = Visibility.Hidden;

                    data.WorldsData[key].top.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    data.WorldsData[key].top.ColumnDefinitions[1].Width = new GridLength(4, GridUnitType.Star);

                    Grid grid = data.WorldsData[key].world.Parent as Grid;
                    grid.ColumnDefinitions[0].Width = new GridLength(2, GridUnitType.Star);
                    grid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
                    grid.ColumnDefinitions[2].Width = new GridLength(4, GridUnitType.Star);
                    Grid.SetColumnSpan(data.WorldsData[key].world, 3);
                }
            }
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

        private void BroadcastStartupToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BroadcastStartup = BroadcastStartupOption.IsChecked;
            if (BroadcastStartupOption.IsChecked)
                broadcast.Show();
        }

        private void FormsGrowthToggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.FormsGrowth = FormsGrowthOption.IsChecked;
            if (FormsGrowthOption.IsChecked)
                FormRow.Height = new GridLength(0.65, GridUnitType.Star);
            else
                FormRow.Height = new GridLength(0, GridUnitType.Star);
        }
    }
}
