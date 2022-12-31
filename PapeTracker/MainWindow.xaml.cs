using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Documents;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace PapeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data data;
        private BroadcastWindow broadcast;
        public int collected;
        private int total = 51;
        public static List<Grid> itemPools;

        public MainWindow()
        {
            InitializeComponent();

            InitData();

            InitOptions();

            collectedChecks = new List<ImportantCheck>();
            newChecks = new List<ImportantCheck>();
            previousChecks = new List<ImportantCheck>();
        }

        private void InitData()
        {
            data = new Data();

            data.WorldsData.Add("GoombaRegion", new WorldData(GoombaRegionTop, GoombaRegion, null, GoombaRegionHint, GoombaRegionGrid, GoombaRegionBar, false));
            data.WorldsData.Add("ToadTown", new WorldData(ToadTownTop, ToadTown, null, ToadTownHint, ToadTownGrid, ToadTownBar, false));
            data.WorldsData.Add("ToadTownTunnels", new WorldData(ToadTownTunnelsTop, ToadTownTunnels, null, ToadTownTunnelsHint, ToadTownTunnelsGrid, ToadTownTunnelsBar, false));
            data.WorldsData.Add("KoopaRegion", new WorldData(KoopaRegionTop, KoopaRegion, null, KoopaRegionHint, KoopaRegionGrid, KoopaRegionBar, false));
            data.WorldsData.Add("KoopaFortress", new WorldData(KoopaFortressTop, KoopaFortress, null, KoopaFortressHint, KoopaFortressGrid, KoopaFortressBar, false));
            data.WorldsData.Add("MtRugged", new WorldData(MtRuggedTop, MtRugged, null, MtRuggedHint, MtRuggedGrid, MtRuggedBar, false));
            data.WorldsData.Add("DryDryOutpost", new WorldData(DryDryOutpostTop, DryDryOutpost, null, DryDryOutpostHint, DryDryOutpostGrid, DryDryOutpostBar, false));
            data.WorldsData.Add("DryDryDesert", new WorldData(DryDryDesertTop, DryDryDesert, null, DryDryDesertHint, DryDryDesertGrid, DryDryDesertBar, false));
            data.WorldsData.Add("DryDryRuins", new WorldData(DryDryRuinsTop, DryDryRuins, null, DryDryRuinsHint, DryDryRuinsGrid, DryDryRuinsBar, false));
            data.WorldsData.Add("ForeverForest", new WorldData(ForeverForestTop, ForeverForest, null, ForeverForestHint, ForeverForestGrid, ForeverForestBar, false));
            data.WorldsData.Add("BooMansion", new WorldData(BooMansionTop, BooMansion, null, BooMansionHint, BooMansionGrid, BooMansionBar, false));
            data.WorldsData.Add("GustyGulch", new WorldData(GustyGulchTop, GustyGulch, null, GustyGulchHint, GustyGulchGrid, GustyGulchBar, false));
            data.WorldsData.Add("TubbaCastle", new WorldData(TubbaCastleTop, TubbaCastle, null, TubbaCastleHint, TubbaCastleGrid, TubbaCastleBar, false));
            data.WorldsData.Add("ShyGuyToybox", new WorldData(ShyGuyToyboxTop, ShyGuyToybox, null, ShyGuyToyboxHint, ShyGuyToyboxGrid, ShyGuyToyboxBar, false));
            data.WorldsData.Add("JadeJungle", new WorldData(JadeJungleTop, JadeJungle, null, JadeJungleHint, JadeJungleGrid, JadeJungleBar, false));
            data.WorldsData.Add("MtLavalava", new WorldData(MtLavalavaTop, MtLavalava, null, MtLavalavaHint, MtLavalavaGrid, MtLavalavaBar, false));
            data.WorldsData.Add("FlowerFields", new WorldData(FlowerFieldsTop, FlowerFields, null, null, FlowerFieldsGrid, FlowerFieldsBar, true));
            data.WorldsData.Add("ShiverMountain", new WorldData(ShiverMountainTop, ShiverMountain, null, null, ShiverMountainGrid, ShiverMountainBar, false));
            data.WorldsData.Add("CrystalPalace", new WorldData(CrystalPalaceTop, CrystalPalace, null, null, CrystalPalaceGrid, CrystalPalaceBar, false));
            data.WorldsData.Add("StartingGear", new WorldData(StartingGearTop, StartingGear, null, null, StartingGearGrid, StartingGearBar, false));

            //data.ProgressKeys.Add("ToadTownTunnels", new List<string>() { "", "STTChests", "TwilightThorn", "Struggle", "ComputerRoom", "Axel", "DataRoxas" });
            //data.ProgressKeys.Add("KoopaRegion", new List<string>() { "", "MysteriousTower", "Sandlot", "Mansion", "BetwixtAndBetween", "DataAxel" });
            //data.ProgressKeys.Add("KoopaFortress", new List<string>() { "", "HBChests", "Bailey", "AnsemStudy", "Corridor", "Dancers", "HBDemyx", "FinalFantasy", "1000Heartless", "Sephiroth", "DataDemyx" });
            //data.ProgressKeys.Add("MtRugged", new List<string>() { "", "BCChests", "Thresholder", "Beast", "DarkThorn", "Dragoons", "Xaldin", "DataXaldin" });
            //data.ProgressKeys.Add("DryDryOutpost", new List<string>() { "", "OCChests", "Cerberus", "OCDemyx", "OCPete", "Hydra", "AuronStatue", "Hades", "Zexion" });
            //data.ProgressKeys.Add("DryDryDesert", new List<string>() { "", "AGChests", "Abu", "Chasm", "TreasureRoom", "Lords", "Carpet", "GenieJafar", "Lexaeus" });
            //data.ProgressKeys.Add("DryDryRuins", new List<string>() { "", "LoDChests", "Cave", "Summmit", "ShanYu", "ThroneRoom", "StormRider", "DataXigbar" });
            //data.ProgressKeys.Add("ForeverForest", new List<string>() { "", "Pooh", "Piglet", "Rabbit", "Kanga", "SpookyCave", "StarryHill" });
            //data.ProgressKeys.Add("BooMansion", new List<string>() { "", "PLChests", "Simba", "Scar", "GroundShaker", "DataSaix" });
            //data.ProgressKeys.Add("GustyGulch", new List<string>() { "", "DCChests", "Minnie", "OldPete", "Windows", "BoatPete", "DCPete", "Marluxia", "LingeringWill" });
            //data.ProgressKeys.Add("TubbaCastle", new List<string>() { "", "HTChests", "CandyCaneLane", "PrisonKeeper", "OogieBoogie", "Presents", "Experiment", "Vexen" });
            //data.ProgressKeys.Add("ShyGuyToybox", new List<string>() { "", "PRChests", "Town", "Barbossa", "Gambler", "GrimReaper", "DataLuxord" });
            //data.ProgressKeys.Add("JadeJungle", new List<string>() { "", "SPChests", "Screens", "HostileProgram", "SolarSailer", "MCP", "Larxene" });
            //data.ProgressKeys.Add("MtLavalava", new List<string>() { "", "MtLavalavaChests", "Roxas", "Xigbar", "Luxord", "Saix", "Xemnas1", "DataXemnas" });
            //data.ProgressKeys.Add("ShiverMountain", new List<string>() { "", "Tutorial", "Ursula", "NewDay" });

            itemPools = new List<Grid>();
            itemPools.Add(PartnersAndGearGrid);
            itemPools.Add(MajorItemsGrid);
            itemPools.Add(MinorItemsGrid);
            itemPools.Add(BadgesGrid);

            foreach(Grid pool in itemPools)
            {
                foreach (ContentControl item in pool.Children)
                {
                    if (item is Item)
                    {
                        data.Items.Add(item as Item);
                    }
                }
            }
            

            broadcast = new BroadcastWindow(data);
        }

        private void InitOptions()
        {
            PromiseCharmOption.IsChecked = Properties.Settings.Default.PromiseCharm;
            //HandleItemToggle(PromiseCharmOption.IsChecked, PromiseCharm, true);

            ReportsOption.IsChecked = Properties.Settings.Default.AnsemReports;
            for (int i = 0; i < data.Reports.Count; ++i)
            {
                HandleItemToggle(ReportsOption.IsChecked, data.Reports[i], true);
            }

            AbilitiesOption.IsChecked = Properties.Settings.Default.Abilities;
            //HandleItemToggle(AbilitiesOption.IsChecked, OnceMore, true);
            //HandleItemToggle(AbilitiesOption.IsChecked, SecondChance, true);

            TornPagesOption.IsChecked = Properties.Settings.Default.TornPages;
            for (int i = 0; i < data.TornPages.Count; ++i)
            {
                HandleItemToggle(TornPagesOption.IsChecked, data.TornPages[i], true);
            }

            CureOption.IsChecked = Properties.Settings.Default.Cure;
            //HandleItemToggle(CureOption.IsChecked, Cure1, true);
            //HandleItemToggle(CureOption.IsChecked, Cure2, true);
            //HandleItemToggle(CureOption.IsChecked, Cure3, true);

            FinalFormOption.IsChecked = Properties.Settings.Default.FinalForm;
            //HandleItemToggle(FinalFormOption.IsChecked, Final, true);

            SimpleOption.IsChecked = Properties.Settings.Default.Simple;
            if (SimpleOption.IsChecked)
                SimpleToggle(null, null);

            //OrbOption.IsChecked = Properties.Settings.Default.Orb;
            //if (OrbOption.IsChecked)
            //    OrbToggle(null, null);

            //AltOption.IsChecked = Properties.Settings.Default.Alt;
            //if (AltOption.IsChecked)
            //    AltToggle(null, null);

            WorldProgressOption.IsChecked = Properties.Settings.Default.WorldProgress;
            WorldProgressToggle(null, null);

            SoraHeartOption.IsChecked = Properties.Settings.Default.SoraHeart;
            SoraHeartToggle(SoraHeartOption.IsChecked);
            SimulatedOption.IsChecked = Properties.Settings.Default.Simulated;
            SimulatedToggle(SimulatedOption.IsChecked);
            ForeverForestOption.IsChecked = Properties.Settings.Default.HundredAcre;
            ForeverForestToggle(ForeverForestOption.IsChecked);
            ShiverMountainOption.IsChecked = Properties.Settings.Default.ShiverMountain;
            ShiverMountainToggle(ShiverMountainOption.IsChecked);

            DragAndDropOption.IsChecked = Properties.Settings.Default.DragDrop;
            DragDropToggle(null, null);

            TopMostOption.IsChecked = Properties.Settings.Default.TopMost;
            TopMostToggle(null, null);

            BroadcastStartupOption.IsChecked = Properties.Settings.Default.BroadcastStartup;
            BroadcastStartupToggle(null, null);

            FormsGrowthOption.IsChecked = Properties.Settings.Default.FormsGrowth;
            FormsGrowthToggle(null, null);

            Top = Properties.Settings.Default.WindowY;
            Left = Properties.Settings.Default.WindowX;

            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;
        }

        /// 
        /// Input Handling
        /// 
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;

            if (e.ChangedButton == MouseButton.Left)
            {
                if (data.selected != null)
                {
                    data.WorldsData[data.selected.Name].selectedBar.Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
                }

                data.selected = button;
                data.WorldsData[button.Name].selectedBar.Source = new BitmapImage(new Uri("Images\\VerticalBar.png", UriKind.Relative));
            }
            else if (e.ChangedButton == MouseButton.Middle)
            {
                if (data.WorldsData.ContainsKey(button.Name) && data.WorldsData[button.Name].hint != null && data.mode == Mode.None)
                {
                    data.WorldsData[button.Name].hint.Text = "?";
                }
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Button button = sender as Button;

            if (data.WorldsData.ContainsKey(button.Name) && data.WorldsData[button.Name].hint != null)
            {
                HandleReportValue(data.WorldsData[button.Name].hint, e.Delta);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown && data.selected != null)
            {
                if (data.WorldsData.ContainsKey(data.selected.Name) && data.WorldsData[data.selected.Name].hint != null)
                {
                    HandleReportValue(data.WorldsData[data.selected.Name].hint, -1);
                }
            }
            if (e.Key == Key.PageUp && data.selected != null)
            {
                if (data.WorldsData.ContainsKey(data.selected.Name) && data.WorldsData[data.selected.Name].hint != null)
                {
                    HandleReportValue(data.WorldsData[data.selected.Name].hint, 1);
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Save("kh2fm-tracker-autosave.txt");
            Properties.Settings.Default.Save();
            broadcast.canClose = true;
            broadcast.Close();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WindowY = RestoreBounds.Top;
            Properties.Settings.Default.WindowX = RestoreBounds.Left;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Properties.Settings.Default.Width = RestoreBounds.Width;
            Properties.Settings.Default.Height = RestoreBounds.Height;
        }

        /// 
        /// Handle UI Changes
        /// 
        private void HandleReportValue(TextBlock Hint, int delta)
        {
            if (data.mode != Mode.None)
                return;

            int num = Int32.Parse(Hint.Text);

            if (delta > 0)
                ++num;
            else
                --num;

            // cap hint value to 51
            if (num > 52)
                num = 52;

            if (num < 0)
                Hint.Text = 0.ToString();
            else
                Hint.Text = num.ToString();

            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), num - 1);
        }

        public void SetReportValue(TextBlock Hint, int value)
        {
            Hint.Text = value.ToString();
            
            broadcast.UpdateTotal(Hint.Name.Remove(Hint.Name.Length - 4, 4), value - 1);
        }

        public void IncrementCollected()
        {
            ++collected;

            Collected.Text = collected.ToString();
            //broadcast.Collected.Source = data.Numbers[collected + 1];

        }

        public void DecrementCollected()
        {
            --collected;
            if (collected < 0)
                collected = 0;

            Collected.Text = collected.ToString();
            //broadcast.Collected.Source = data.Numbers[collected + 1];
        }

        public void IncrementTotal()
        {
            ++total;
            if (total > 51)
                total = 51;

            Collected.Text = collected.ToString();
            //broadcast.CheckTotal.Source = data.Numbers[total + 1];
        }

        public void DecrementTotal()
        {
            --total;
            if (total < 0)
                total = 0;

            Collected.Text = collected.ToString();
            //broadcast.CheckTotal.Source = data.Numbers[total + 1];
        }

        public void SetHintText(string text)
        {
            //HintText.Content = text;
        }

        private void ResetSize(object sender, RoutedEventArgs e)
        {
            Width = 570;
            Height = 880;

            broadcast.Width = 500;
            broadcast.Height = 680;
        }

        private void ItemGridSwap(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var gridToSwapTo = button.Tag;
            foreach(Grid itemGrid in itemPools)
            {
                if(itemGrid.Name.Equals(gridToSwapTo))
                {
                    itemGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    itemGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void StarCompletionToggle(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if(button.Opacity == 0.25)
            {
                button.Opacity = 1;
            }
            else
            {
                button.Opacity = .25;
            }
        }
    }
}
