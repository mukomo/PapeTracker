using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PapeTracker
{
    /// <summary>
    /// Interaction logic for BroadcastWindow.xaml
    /// </summary>
    public partial class BroadcastWindow : Window
    {
        public bool canClose = false;
        Dictionary<string, int> worlds = new Dictionary<string, int>();
        Dictionary<string, int> totals = new Dictionary<string, int>();
        Dictionary<string, int> important = new Dictionary<string, int>();
        Dictionary<string, ContentControl> Progression = new Dictionary<string, ContentControl>();
        List<BitmapImage> Numbers = null;
        Data data;

        public BroadcastWindow(Data dataIn)
        {
            InitializeComponent();
            //Item.UpdateTotal += new Item.TotalHandler(UpdateTotal);
            Numbers = dataIn.Numbers;
            worlds.Add("GoombaRegion",0);
            worlds.Add("ToadTown", 0);
            worlds.Add("ToadTownTunnels",0);
            worlds.Add("KoopaRegion",0);
            worlds.Add("KoopaFortress",0);
            worlds.Add("MtRugged",0);
            worlds.Add("DryDryOutpost",0);
            worlds.Add("DryDryDesert",0);
            worlds.Add("DryDryRuins",0);
            worlds.Add("ForeverForest",0);
            worlds.Add("BooMansion",0);
            worlds.Add("GustyGulch",0);
            worlds.Add("TubbaCastle",0);
            worlds.Add("ShyGuyToybox",0);
            worlds.Add("JadeJungle",0);
            worlds.Add("MtLavalava",0);
            worlds.Add("FlowerFields", 0);
            worlds.Add("Report", 0);
            worlds.Add("TornPage", 0);
            worlds.Add("Fire", 0);
            worlds.Add("Blizzard", 0);
            worlds.Add("Thunder", 0);
            worlds.Add("Cure", 0);
            worlds.Add("Reflect", 0);
            worlds.Add("Magnet", 0);
            worlds.Add("ShiverMountain", 0);
            worlds.Add("CrystalPalace", 0);
            worlds.Add("StartingGear", 0);

            totals.Add("GoombaRegion", -1);
            totals.Add("ToadTown", -1);
            totals.Add("ToadTownTunnels", -1);
            totals.Add("KoopaRegion", -1);
            totals.Add("KoopaFortress", -1);
            totals.Add("MtRugged", -1);
            totals.Add("DryDryOutpost", -1);
            totals.Add("DryDryDesert", -1);
            totals.Add("DryDryRuins", -1);
            totals.Add("ForeverForest", -1);
            totals.Add("BooMansion", -1);
            totals.Add("GustyGulch", -1);
            totals.Add("TubbaCastle", -1);
            totals.Add("ShyGuyToybox", -1);
            totals.Add("JadeJungle", -1);
            totals.Add("MtLavalava", -1);
            totals.Add("ShiverMountain", -1);
            totals.Add("CrystalPalace", -1);
            totals.Add("StartingGear", -1);

            important.Add("Fire", 0);
            important.Add("Blizzard", 0);
            important.Add("Thunder", 0);
            important.Add("Cure", 0);
            important.Add("Reflect", 0);
            important.Add("Magnet", 0);
            important.Add("Valor", 0);
            important.Add("Wisdom", 0);
            important.Add("Limit", 0);
            important.Add("Master", 0);
            important.Add("Final", 0);
            important.Add("Nonexistence", 0);
            important.Add("Connection", 0);
            important.Add("Peace", 0);
            important.Add("PromiseCharm", 0);
            important.Add("Feather", 0);
            important.Add("Ukulele", 0);
            important.Add("Baseball", 0);
            important.Add("Lamp", 0);
            important.Add("Report", 0);
            important.Add("TornPage", 0);
            important.Add("SecondChance", 0);
            important.Add("OnceMore", 0);

            //Progression.Add("ToadTownTunnels", ToadTownTunnelsProgression);
            //Progression.Add("KoopaRegion", KoopaRegionProgression);
            //Progression.Add("KoopaFortress", KoopaFortressProgression);
            //Progression.Add("MtRugged", MtRuggedProgression);
            //Progression.Add("DryDryOutpost", DryDryOutpostProgression);
            //Progression.Add("DryDryDesert", DryDryDesertProgression);
            //Progression.Add("DryDryRuins", DryDryRuinsProgression);
            //Progression.Add("ForeverForest", ForeverForestProgression);
            //Progression.Add("BooMansion", BooMansionProgression);
            //Progression.Add("GustyGulch", GustyGulchProgression);
            //Progression.Add("TubbaCastle", TubbaCastleProgression);
            //Progression.Add("ShyGuyToybox", ShyGuyToyboxProgression);
            //Progression.Add("JadeJungle", JadeJungleProgression);
            //Progression.Add("MtLavalava", MtLavalavaProgression);

            data = dataIn;

            foreach (Item item in data.Items)
            {
                item.UpdateTotal += new Item.TotalHandler(UpdateTotal);
                item.UpdateFound += new Item.FoundHandler(UpdateFound);
            }

            Top = Properties.Settings.Default.BroadcastWindowY;
            Left = Properties.Settings.Default.BroadcastWindowX;

            Width = Properties.Settings.Default.BroadcastWindowWidth;
            Height = Properties.Settings.Default.BroadcastWindowHeight;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BroadcastWindowY = RestoreBounds.Top;
            Properties.Settings.Default.BroadcastWindowX = RestoreBounds.Left;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Properties.Settings.Default.BroadcastWindowWidth = RestoreBounds.Width;
            Properties.Settings.Default.BroadcastWindowHeight = RestoreBounds.Height;
        }

        public void UpdateFound(string item, string world, bool add)
        {
            return;
            string worldName = world;
            if (add) worlds[worldName]++; else worlds[worldName]--;
            //Console.WriteLine(worlds[worldName]);

            while (item.Any(char.IsDigit))
            {
                item = item.Remove(item.Length - 1, 1);
            }

            if (add) important[item]++; else important[item]--;
            worlds["Report"] = important["Report"];
            worlds["TornPage"] = important["TornPage"];
            worlds["Fire"] = important["Fire"];
            worlds["Blizzard"] = important["Blizzard"];
            worlds["Thunder"] = important["Thunder"];
            worlds["Cure"] = important["Cure"];
            worlds["Reflect"] = important["Reflect"];
            worlds["Magnet"] = important["Magnet"];
            //Console.WriteLine(item);

            UpdateNumbers();
            
        }

        public void UpdateNumbers()
        {
            return;
            foreach(KeyValuePair<string,int> world in worlds)
            {
                if (world.Value < 52)
                {
                    BitmapImage number = Numbers[world.Value + 1];
                    if ((data.WorldsData.ContainsKey(world.Key) && world.Key != "FlowerFields" && data.WorldsData[world.Key].hintedHint) 
                        || (data.WorldsData.ContainsKey(world.Key) &&  world.Key != "FlowerFields" && data.WorldsData[world.Key].complete))
                    {
                        number = data.BlueNumbers[world.Value + 1];
                        Image bar = FindName(world.Key + "Bar") as Image;
                        bar.Source = new BitmapImage(new Uri("Images/BarBlue.png", UriKind.Relative));
                    }

                    if (world.Key == "TornPage" || world.Key == "Fire" || world.Key == "Blizzard"
                        || world.Key == "Thunder" || world.Key == "Cure" || world.Key == "Reflect" || world.Key == "Magnet")
                    {
                        number = data.SingleNumbers[world.Value + 1];
                    }

                    Image worldFound = this.FindName(world.Key + "Found") as Image;
                    worldFound.Source = number;

                    if (world.Key == "Fire" || world.Key == "Blizzard" || world.Key == "Thunder" 
                        || world.Key == "Cure" || world.Key == "Reflect" || world.Key == "Magnet")
                    {
                        if (world.Value == 0)
                        {
                            worldFound.Source = null;
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, int> total in totals)
            {
                Image worldTotal = this.FindName(total.Key + "Total") as Image;
                if (total.Value == -1)
                {
                    worldTotal.Source = data.SingleNumbers[0];
                }
                else if ((data.WorldsData.ContainsKey(total.Key) &&  total.Key != "FlowerFields" && data.WorldsData[total.Key].hintedHint) 
                    || (data.WorldsData.ContainsKey(total.Key) &&  total.Key != "FlowerFields" && data.WorldsData[total.Key].complete))
                {
                    if (total.Value <= 10)
                        worldTotal.Source = data.BlueSingleNumbers[total.Value];
                    else
                        worldTotal.Source = data.BlueNumbers[total.Value];
                }
                else
                {
                    if (total.Value <= 10)
                        worldTotal.Source = data.SingleNumbers[total.Value];
                    else
                        worldTotal.Source = data.Numbers[total.Value];
                }

                // Format fixing for double digit numbers
                if (total.Key != "FlowerFields" && total.Key != "ShiverMountain")
                {
                    if (total.Value >= 11)
                    {
                        (worldTotal.Parent as Grid).ColumnDefinitions[3].Width = new GridLength(2, GridUnitType.Star);
                        (worldTotal.Parent as Grid).ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
                    }
                    else
                    {
                        (worldTotal.Parent as Grid).ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);
                        (worldTotal.Parent as Grid).ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    }
                }
            }

            foreach(KeyValuePair<string,int> impCheck in important)
            {
                ContentControl imp = this.FindName(impCheck.Key) as ContentControl;
                if (impCheck.Value > 0)
                {
                    imp.Opacity = 1;
                }
                else
                {
                    if (impCheck.Key != "Report" && impCheck.Key != "TornPage")
                    imp.Opacity = 0.25;
                }
            }
        }

        public void UpdateTotal(string world, int checks)
        {
            return;
            string worldName = world;
            totals[worldName] = checks+1;

            UpdateNumbers();
        }


        void BroadcastWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        public void OnResetHints()
        {
            return;
            GoombaRegionBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            ToadTownBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            KoopaFortressBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            KoopaRegionBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            DryDryRuinsBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            MtRuggedBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            DryDryOutpostBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            JadeJungleBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            TubbaCastleBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            ShyGuyToyboxBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            DryDryDesertBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            BooMansionBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            GustyGulchBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            ForeverForestBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            ToadTownTunnelsBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            MtLavalavaBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
        }

        public void OnReset()
        {
            return;
            worlds.Clear();
            worlds.Add("GoombaRegion", 0);
            worlds.Add("ToadTown", 0);
            worlds.Add("ToadTownTunnels", 0);
            worlds.Add("KoopaRegion", 0);
            worlds.Add("KoopaFortress", 0);
            worlds.Add("MtRugged", 0);
            worlds.Add("DryDryOutpost", 0);
            worlds.Add("DryDryDesert", 0);
            worlds.Add("DryDryRuins", 0);
            worlds.Add("ForeverForest", 0);
            worlds.Add("BooMansion", 0);
            worlds.Add("GustyGulch", 0);
            worlds.Add("TubbaCastle", 0);
            worlds.Add("ShyGuyToybox", 0);
            worlds.Add("JadeJungle", 0);
            worlds.Add("MtLavalava", 0);
            worlds.Add("FlowerFields", 0);
            worlds.Add("Report", 0);
            worlds.Add("TornPage", 0);
            worlds.Add("Fire", 0);
            worlds.Add("Blizzard", 0);
            worlds.Add("Thunder", 0);
            worlds.Add("Cure", 0);
            worlds.Add("Reflect", 0);
            worlds.Add("Magnet", 0);
            worlds.Add("ShiverMountain", 0);

            totals.Clear();
            totals.Add("GoombaRegion", -1);
            totals.Add("ToadTown", -1);
            totals.Add("ToadTownTunnels", -1);
            totals.Add("KoopaRegion", -1);
            totals.Add("KoopaFortress", -1);
            totals.Add("MtRugged", -1);
            totals.Add("DryDryOutpost", -1);
            totals.Add("DryDryDesert", -1);
            totals.Add("DryDryRuins", -1);
            totals.Add("ForeverForest", -1);
            totals.Add("BooMansion", -1);
            totals.Add("GustyGulch", -1);
            totals.Add("TubbaCastle", -1);
            totals.Add("ShyGuyToybox", -1);
            totals.Add("JadeJungle", -1);
            totals.Add("MtLavalava", -1);
            totals.Add("ShiverMountain", -1);

            important.Clear();
            important.Add("Fire", 0);
            important.Add("Blizzard", 0);
            important.Add("Thunder", 0);
            important.Add("Cure", 0);
            important.Add("Reflect", 0);
            important.Add("Magnet", 0);
            important.Add("Valor", 0);
            important.Add("Wisdom", 0);
            important.Add("Limit", 0);
            important.Add("Master", 0);
            important.Add("Final", 0);
            important.Add("Nonexistence", 0);
            important.Add("Connection", 0);
            important.Add("Peace", 0);
            important.Add("PromiseCharm", 0);
            important.Add("Feather", 0);
            important.Add("Ukulele", 0);
            important.Add("Baseball", 0);
            important.Add("Lamp", 0);
            important.Add("Report", 0);
            important.Add("TornPage", 0);
            important.Add("SecondChance", 0);
            important.Add("OnceMore", 0);

            GoombaRegionBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            ToadTownBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            KoopaFortressBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            KoopaRegionBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            DryDryRuinsBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            MtRuggedBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            DryDryOutpostBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            JadeJungleBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            TubbaCastleBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            ShyGuyToyboxBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            DryDryDesertBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            BooMansionBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            GustyGulchBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            ForeverForestBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            ToadTownTunnelsBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));
            MtLavalavaBar.Source = new BitmapImage(new Uri("Images/Bar.png", UriKind.Relative));

            Collected.Source = data.Numbers[1];
        }

        public void ToggleProgression(bool toggle)
        {
            if (toggle == true)
            {
                foreach (string key in Progression.Keys.ToList())
                {
                    Progression[key].Visibility = Visibility.Visible;
                }
            }
            else
            {
                foreach (string key in Progression.Keys.ToList())
                {
                    Progression[key].Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
