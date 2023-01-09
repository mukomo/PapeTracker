using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Linq;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace PapeTracker
{
    public partial class MainWindow : Window
    {
        /// 
        /// Options
        ///

        private void SaveProgress(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FileName = "pape-tracker-save";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (saveFileDialog.ShowDialog() == true)
            {
                Save(saveFileDialog.FileName);
            }
        }

        public void Save(string filename)
        {
            string mode = "Mode: " + data.mode.ToString();
            // save settings
            string settings = "Settings: ";
            //if (PromiseCharmOption.IsChecked)
            //    settings += "Promise Charm - ";
            //if (ReportsOption.IsChecked)
            //    settings += "Secret Ansem Reports - ";
            //if (AbilitiesOption.IsChecked)
            //    settings += "Second Chance & Once More - ";
            //if (TornPagesOption.IsChecked)
            //    settings += "Torn Pages - ";
            //if (CureOption.IsChecked)
            //    settings += "Cure - ";
            //if (FinalFormOption.IsChecked)
            //    settings += "Final Form - ";
            //if (SoraHeartOption.IsChecked)
            //    settings += "Sora's Heart - ";
            //if (SimulatedOption.IsChecked)
            //    settings += "Simulated Twilight Town - ";
            //if (ForeverForestOption.IsChecked)
            //    settings += "100 Acre Wood - ";
            //if (ShiverMountainOption.IsChecked)
            //    settings += "ShiverMountain - ";

            // save hint state (hint info, hints, track attempts)
            string attempts = "";
            string hintValues = "";
            if (data.mode == Mode.Hints || data.mode == Mode.OpenKHHints)
            {
                attempts = "Attempts: ";
                if (data.hintsLoaded)
                {
                    foreach (int num in data.reportAttempts)
                    {
                        attempts += " - " + num.ToString();
                    }
                }
                // store hint values
                hintValues = "HintValues:";
                foreach (WorldData worldData in data.WorldsData.Values.ToList())
                {
                    if (worldData.hint == null)
                        continue;

                    int num = Int32.Parse(worldData.hint.Text);
                    hintValues += " " + num.ToString();
                }
            }

            // Save progress of worlds
            //string Progress = "Progress:";
            //Progress += " " + data.WorldsData["ToadTownTunnels"].progress.ToString();
            //Progress += " " + data.WorldsData["KoopaRegion"].progress.ToString();
            //Progress += " " + data.WorldsData["KoopaFortress"].progress.ToString();
            //Progress += " " + data.WorldsData["MtRugged"].progress.ToString();
            //Progress += " " + data.WorldsData["DryDryOutpost"].progress.ToString();
            //Progress += " " + data.WorldsData["DryDryDesert"].progress.ToString();
            //Progress += " " + data.WorldsData["DryDryRuins"].progress.ToString();
            //Progress += " " + data.WorldsData["ForeverForest"].progress.ToString();
            //Progress += " " + data.WorldsData["BooMansion"].progress.ToString();
            //Progress += " " + data.WorldsData["GustyGulch"].progress.ToString();
            //Progress += " " + data.WorldsData["TubbaCastle"].progress.ToString();
            //Progress += " " + data.WorldsData["ShyGuyToybox"].progress.ToString();
            //Progress += " " + data.WorldsData["JadeJungle"].progress.ToString();
            //Progress += " " + data.WorldsData["MtLavalava"].progress.ToString();

            // save items in worlds
            string GoombaRegion = "GoombaRegion:";
            foreach (Item item in data.WorldsData["GoombaRegion"].worldGrid.Children)
            {
                GoombaRegion += " " + item.Name;
            }
            string ToadTown = "ToadTown:";
            foreach (Item item in data.WorldsData["ToadTown"].worldGrid.Children)
            {
                ToadTown += " " + item.Name;
            }
            string simulated = "ToadTownTunnels:";
            foreach (Item item in data.WorldsData["ToadTownTunnels"].worldGrid.Children)
            {
                simulated += " " + item.Name;
            }
            string KoopaRegion = "KoopaRegion:";
            foreach (Item item in data.WorldsData["KoopaRegion"].worldGrid.Children)
            {
                KoopaRegion += " " + item.Name;
            }
            string KoopaFortress = "KoopaFortress:";
            foreach (Item item in data.WorldsData["KoopaFortress"].worldGrid.Children)
            {
                KoopaFortress += " " + item.Name;
            }
            string beastCastle = "MtRugged:";
            foreach (Item item in data.WorldsData["MtRugged"].worldGrid.Children)
            {
                beastCastle += " " + item.Name;
            }
            string DryDryOutpost = "DryDryOutpost:";
            foreach (Item item in data.WorldsData["DryDryOutpost"].worldGrid.Children)
            {
                DryDryOutpost += " " + item.Name;
            }
            string DryDryDesert = "DryDryDesert:";
            foreach (Item item in data.WorldsData["DryDryDesert"].worldGrid.Children)
            {
                DryDryDesert += " " + item.Name;
            }
            string DryDryRuins = "DryDryRuins:";
            foreach (Item item in data.WorldsData["DryDryRuins"].worldGrid.Children)
            {
                DryDryRuins += " " + item.Name;
            }
            string ForeverForest = "ForeverForest:";
            foreach (Item item in data.WorldsData["ForeverForest"].worldGrid.Children)
            {
                ForeverForest += " " + item.Name;
            }
            string BooMansion = "BooMansion:";
            foreach (Item item in data.WorldsData["BooMansion"].worldGrid.Children)
            {
                BooMansion += " " + item.Name;
            }
            string GustyGulch = "GustyGulch:";
            foreach (Item item in data.WorldsData["GustyGulch"].worldGrid.Children)
            {
                GustyGulch += " " + item.Name;
            }
            string TubbaCastle = "TubbaCastle:";
            foreach (Item item in data.WorldsData["TubbaCastle"].worldGrid.Children)
            {
                TubbaCastle += " " + item.Name;
            }
            string ShyGuyToybox = "ShyGuyToybox:";
            foreach (Item item in data.WorldsData["ShyGuyToybox"].worldGrid.Children)
            {
                ShyGuyToybox += " " + item.Name;
            }
            string JadeJungle = "JadeJungle:";
            foreach (Item item in data.WorldsData["JadeJungle"].worldGrid.Children)
            {
                JadeJungle += " " + item.Name;
            }
            string MtLavalava = "MtLavalava:";
            foreach (Item item in data.WorldsData["MtLavalava"].worldGrid.Children)
            {
                MtLavalava += " " + item.Name;
            }
            string ShiverMountain = "ShiverMountain:";
            foreach (Item item in data.WorldsData["ShiverMountain"].worldGrid.Children)
            {
                ShiverMountain += " " + item.Name;
            }
            string FlowerFields = "FlowerFields:";
            foreach (Item item in data.WorldsData["FlowerFields"].worldGrid.Children)
            {
                FlowerFields += " " + item.Name;
            }
            string CrystalPalace = "CrystalPalace:";
            foreach (Item item in data.WorldsData["CrystalPalace"].worldGrid.Children)
            {
                FlowerFields += " " + item.Name;
            }
            string StartingGear = "StartingGear:";
            foreach (Item item in data.WorldsData["StartingGear"].worldGrid.Children)
            {
                FlowerFields += " " + item.Name;
            }

            FileStream file = File.Create(filename);
            StreamWriter writer = new StreamWriter(file);

            writer.WriteLine(mode);
            writer.WriteLine(settings);
            if (data.mode == Mode.Hints)
            {
                writer.WriteLine(attempts);
                writer.WriteLine(data.hintFileText[0]);
                writer.WriteLine(data.hintFileText[1]);
                writer.WriteLine(hintValues);
            }
            else if (data.mode == Mode.OpenKHHints)
            {
                writer.WriteLine(attempts);
                writer.WriteLine(data.openKHHintText);
                writer.WriteLine(hintValues);
            }
            else if (data.mode == Mode.AltHints)
            {
                Dictionary<string, List<string>> test = new Dictionary<string, List<string>>();
                foreach (string key in data.WorldsData.Keys.ToList())
                {
                    test.Add(key, data.WorldsData[key].checkCount);
                }
                string hintObject = JsonSerializer.Serialize(test);
                string hintText = Convert.ToBase64String(Encoding.UTF8.GetBytes(hintObject));
                writer.WriteLine(hintText);
            }
            else if (data.mode == Mode.OpenKHAltHints)
            {
                writer.WriteLine(data.openKHHintText);
            }
            //writer.WriteLine(Progress);
            writer.WriteLine(GoombaRegion);
            writer.WriteLine(ToadTown);
            writer.WriteLine(simulated);
            writer.WriteLine(KoopaRegion);
            writer.WriteLine(KoopaFortress);
            writer.WriteLine(beastCastle);
            writer.WriteLine(DryDryOutpost);
            writer.WriteLine(DryDryDesert);
            writer.WriteLine(DryDryRuins);
            writer.WriteLine(ForeverForest);
            writer.WriteLine(BooMansion);
            writer.WriteLine(GustyGulch);
            writer.WriteLine(TubbaCastle);
            writer.WriteLine(ShyGuyToybox);
            writer.WriteLine(JadeJungle);
            writer.WriteLine(MtLavalava);
            writer.WriteLine(ShiverMountain);
            writer.WriteLine(FlowerFields);
            writer.WriteLine(CrystalPalace);
            writer.WriteLine(StartingGear);

            foreach (var world in data.WorldsData)
            {
                writer.Write(world.Key + ": ");
                foreach (var item in world.Value.Checks)
                {
                    if (item.Item is object) writer.Write(item.Value + ", ");
                }
                writer.WriteLine();
            }

            writer.Close();
        }

        private void LoadProgress(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FileName = "pape-tracker-save";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                Load(openFileDialog.FileName);
            }
        }

        private void Load(string filename)
        {
            Stream file = File.Open(filename, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            // reset tracker
            OnReset(null, null);

            string mode = reader.ReadLine().Substring(6);
            //if (mode == "Hints")
            //    SetMode(Mode.Hints);
            //else if (mode == "AltHints")
            //    SetMode(Mode.AltHints);
            //else if (mode == "OpenKHHints")
            //    SetMode(Mode.OpenKHHints);
            //else if (mode == "OpenKHAltHints")
            //    SetMode(Mode.OpenKHAltHints);

            //// set settings
            string settings = reader.ReadLine();
            //LoadSettings(settings.Substring(10));

            //// set hint state
            //if (mode == "Hints")
            //{
            //    string attempts = reader.ReadLine();
            //    attempts = attempts.Substring(13);
            //    string[] attemptsArray = attempts.Split('-');
            //    for (int i = 0; i < attemptsArray.Length; ++i)
            //    {
            //        data.reportAttempts[i] = int.Parse(attemptsArray[i]);
            //    }

            //    string line1 = reader.ReadLine();
            //    data.hintFileText[0] = line1;
            //    string[] reportvalues = line1.Split('.');

            //    string line2 = reader.ReadLine();
            //    data.hintFileText[1] = line2;
            //    line2 = line2.TrimEnd('.');
            //    string[] reportorder = line2.Split('.');

            //    for (int i = 0; i < reportorder.Length; ++i)
            //    {
            //        data.reportLocations.Add(data.codes.FindCode(reportorder[i]));
            //        string[] temp = reportvalues[i].Split(',');
            //        data.reportInformation.Add(new Tuple<string, int>(data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
            //    }

            //    data.hintsLoaded = true;
            //    HintText.Content = "Hints Loaded";
            //}
            //else if (mode == "AltHints")
            //{
            //    var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadLine()));
            //    var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintText);

            //    foreach (var world in worlds)
            //    {
            //        if (world.Key == "FlowerFields")
            //        {
            //            continue;
            //        }
            //        foreach (var item in world.Value)
            //        {
            //            data.WorldsData[world.Key].checkCount.Add(item);
            //        }

            //    }
            //    foreach (var key in data.WorldsData.Keys.ToList())
            //    {
            //        if (key == "FlowerFields")
            //            continue;

            //        data.WorldsData[key].worldGrid.WorldComplete();
            //        SetReportValue(data.WorldsData[key].hint, 1);
            //    }
            //}
            //else if (mode == "OpenKHHints")
            //{
            //    string attempts = reader.ReadLine();
            //    attempts = attempts.Substring(13);
            //    string[] attemptsArray = attempts.Split('-');
            //    for (int i = 0; i < attemptsArray.Length; ++i)
            //    {
            //        data.reportAttempts[i] = int.Parse(attemptsArray[i]);
            //    }
            //    data.openKHHintText = reader.ReadLine();
            //    var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
            //    var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
            //    var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(hintObject["Reports"].ToString());

            //    List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
            //    reportKeys.Sort();

            //    foreach (var report in reportKeys)
            //    {
            //        var world = convertOpenKH[reports[report.ToString()]["World"].ToString()];
            //        var count = reports[report.ToString()]["Count"].ToString();
            //        var location = convertOpenKH[reports[report.ToString()]["Location"].ToString()];
            //        data.reportInformation.Add(new Tuple<string, int>(world, int.Parse(count)));
            //        data.reportLocations.Add(location);
            //    }

            //    data.hintsLoaded = true;
            //    HintText.Content = "Hints Loaded";
            //}
            //else if (mode == "OpenKHAltHints")
            //{
            //    data.openKHHintText = reader.ReadLine();
            //    var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
            //    var hintObject = JsonSerializer.Deserialize<Dictionary<string, object>>(hintText);
            //    var worlds = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(hintObject["world"].ToString());

            //    foreach (var world in worlds)
            //    {
            //        if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
            //        {
            //            continue;
            //        }
            //        foreach (var item in world.Value)
            //        {
            //            data.WorldsData[convertOpenKH[world.Key]].checkCount.Add(convertOpenKH[item]);
            //        }

            //    }
            //    foreach (var key in data.WorldsData.Keys.ToList())
            //    {
            //        if (key == "FlowerFields")
            //            continue;

            //        data.WorldsData[key].worldGrid.WorldComplete();
            //        SetReportValue(data.WorldsData[key].hint, 1);
            //    }
            //}


            // set hint values (DUMB)
            if (data.hintsLoaded)
            {
                string[] hintValues = reader.ReadLine().Substring(12).Split(' ');
                SetReportValue(data.WorldsData["GoombaRegion"].hint, int.Parse(hintValues[0]));
                SetReportValue(data.WorldsData["ToadTown"].hint, int.Parse(hintValues[1]));
                SetReportValue(data.WorldsData["ToadTownTunnels"].hint, int.Parse(hintValues[2]));
                SetReportValue(data.WorldsData["KoopaRegion"].hint, int.Parse(hintValues[3]));
                SetReportValue(data.WorldsData["KoopaFortress"].hint, int.Parse(hintValues[4]));
                SetReportValue(data.WorldsData["MtRugged"].hint, int.Parse(hintValues[5]));
                SetReportValue(data.WorldsData["DryDryOutpost"].hint, int.Parse(hintValues[6]));
                SetReportValue(data.WorldsData["DryDryDesert"].hint, int.Parse(hintValues[7]));
                SetReportValue(data.WorldsData["DryDryRuins"].hint, int.Parse(hintValues[8]));
                SetReportValue(data.WorldsData["ForeverForest"].hint, int.Parse(hintValues[9]));
                SetReportValue(data.WorldsData["BooMansion"].hint, int.Parse(hintValues[10]));
                SetReportValue(data.WorldsData["GustyGulch"].hint, int.Parse(hintValues[11]));
                SetReportValue(data.WorldsData["TubbaCastle"].hint, int.Parse(hintValues[12]));
                SetReportValue(data.WorldsData["ShyGuyToybox"].hint, int.Parse(hintValues[13]));
                SetReportValue(data.WorldsData["JadeJungle"].hint, int.Parse(hintValues[14]));
                SetReportValue(data.WorldsData["MtLavalava"].hint, int.Parse(hintValues[15]));
                SetReportValue(data.WorldsData["ShiverMountain"].hint, int.Parse(hintValues[16]));
                SetReportValue(data.WorldsData["FlowerFields"].hint, int.Parse(hintValues[17]));
                SetReportValue(data.WorldsData["CrystalPalace"].hint, int.Parse(hintValues[18]));
                SetReportValue(data.WorldsData["StartingGear"].hint, int.Parse(hintValues[19]));
            }

            //string[] progress = reader.ReadLine().Substring(10).Split(' ');
            //data.WorldsData["ToadTownTunnels"].progress = int.Parse(progress[0]);
            //data.WorldsData["KoopaRegion"].progress = int.Parse(progress[1]);
            //data.WorldsData["KoopaFortress"].progress = int.Parse(progress[2]);
            //data.WorldsData["MtRugged"].progress = int.Parse(progress[3]);
            //data.WorldsData["DryDryOutpost"].progress = int.Parse(progress[4]);
            //data.WorldsData["DryDryDesert"].progress = int.Parse(progress[5]);
            //data.WorldsData["DryDryRuins"].progress = int.Parse(progress[6]);
            //data.WorldsData["ForeverForest"].progress = int.Parse(progress[7]);
            //data.WorldsData["BooMansion"].progress = int.Parse(progress[8]);
            //data.WorldsData["GustyGulch"].progress = int.Parse(progress[9]);
            //data.WorldsData["TubbaCastle"].progress = int.Parse(progress[10]);
            //data.WorldsData["ShyGuyToybox"].progress = int.Parse(progress[11]);
            //data.WorldsData["JadeJungle"].progress = int.Parse(progress[12]);
            //data.WorldsData["MtLavalava"].progress = int.Parse(progress[13]);

            //SetProgressIcons();

            // add items to worlds
            while (reader.EndOfStream == false)
            {
                string world = reader.ReadLine();
                string worldName = world.Substring(0, world.IndexOf(':'));
                string items = world.Substring(world.IndexOf(':') + 1).Trim();
                if (items != string.Empty)
                {
                    foreach (string item in items.Split(' '))
                    {
                        WorldGrid grid = FindName(worldName + "Grid") as WorldGrid;
                        Item importantCheck = FindName(item) as Item;

                        if (grid.Handle_Report(importantCheck, this, data))
                            grid.Add_Item(importantCheck, this);
                    }
                }
            }

            reader.Close();
        }

        //private void SetProgressIcons()
        //{
        //    string STTkey = data.ProgressKeys["ToadTownTunnels"][data.WorldsData["ToadTownTunnels"].progress];
        //    data.WorldsData["ToadTownTunnels"].progression.SetResourceReference(ContentProperty, STTkey);
        //    broadcast.ToadTownTunnelsProgression.SetResourceReference(ContentProperty, STTkey);

        //    string TTkey = data.ProgressKeys["KoopaRegion"][data.WorldsData["KoopaRegion"].progress];
        //    data.WorldsData["KoopaRegion"].progression.SetResourceReference(ContentProperty, TTkey);
        //    broadcast.KoopaRegionProgression.SetResourceReference(ContentProperty, TTkey);

        //    string HBkey = data.ProgressKeys["KoopaFortress"][data.WorldsData["KoopaFortress"].progress];
        //    data.WorldsData["KoopaFortress"].progression.SetResourceReference(ContentProperty, HBkey);
        //    broadcast.KoopaFortressProgression.SetResourceReference(ContentProperty, HBkey);

        //    string BCkey = data.ProgressKeys["MtRugged"][data.WorldsData["MtRugged"].progress];
        //    data.WorldsData["MtRugged"].progression.SetResourceReference(ContentProperty, BCkey);
        //    broadcast.MtRuggedProgression.SetResourceReference(ContentProperty, BCkey);

        //    string OCkey = data.ProgressKeys["DryDryOutpost"][data.WorldsData["DryDryOutpost"].progress];
        //    data.WorldsData["DryDryOutpost"].progression.SetResourceReference(ContentProperty, OCkey);
        //    broadcast.DryDryOutpostProgression.SetResourceReference(ContentProperty, OCkey);

        //    string AGkey = data.ProgressKeys["DryDryDesert"][data.WorldsData["DryDryDesert"].progress];
        //    data.WorldsData["DryDryDesert"].progression.SetResourceReference(ContentProperty, AGkey);
        //    broadcast.DryDryDesertProgression.SetResourceReference(ContentProperty, AGkey);

        //    string LoDkey = data.ProgressKeys["DryDryRuins"][data.WorldsData["DryDryRuins"].progress];
        //    data.WorldsData["DryDryRuins"].progression.SetResourceReference(ContentProperty, LoDkey);
        //    broadcast.DryDryRuinsProgression.SetResourceReference(ContentProperty, LoDkey);

        //    string HAWkey = data.ProgressKeys["ForeverForest"][data.WorldsData["ForeverForest"].progress];
        //    data.WorldsData["ForeverForest"].progression.SetResourceReference(ContentProperty, HAWkey);
        //    broadcast.ForeverForestProgression.SetResourceReference(ContentProperty, LoDkey);

        //    string PLkey = data.ProgressKeys["BooMansion"][data.WorldsData["BooMansion"].progress];
        //    data.WorldsData["BooMansion"].progression.SetResourceReference(ContentProperty, PLkey);
        //    broadcast.BooMansionProgression.SetResourceReference(ContentProperty, PLkey);

        //    string DCkey = data.ProgressKeys["GustyGulch"][data.WorldsData["GustyGulch"].progress];
        //    data.WorldsData["GustyGulch"].progression.SetResourceReference(ContentProperty, DCkey);
        //    broadcast.GustyGulchProgression.SetResourceReference(ContentProperty, DCkey);

        //    string HTkey = data.ProgressKeys["TubbaCastle"][data.WorldsData["TubbaCastle"].progress];
        //    data.WorldsData["TubbaCastle"].progression.SetResourceReference(ContentProperty, HTkey);
        //    broadcast.TubbaCastleProgression.SetResourceReference(ContentProperty, HTkey);

        //    string PRkey = data.ProgressKeys["ShyGuyToybox"][data.WorldsData["ShyGuyToybox"].progress];
        //    data.WorldsData["ShyGuyToybox"].progression.SetResourceReference(ContentProperty, PRkey);
        //    broadcast.ShyGuyToyboxProgression.SetResourceReference(ContentProperty, PRkey);

        //    string SPkey = data.ProgressKeys["JadeJungle"][data.WorldsData["JadeJungle"].progress];
        //    data.WorldsData["JadeJungle"].progression.SetResourceReference(ContentProperty, SPkey);
        //    broadcast.JadeJungleProgression.SetResourceReference(ContentProperty, SPkey);

        //    string MtLavalavakey = data.ProgressKeys["MtLavalava"][data.WorldsData["MtLavalava"].progress];
        //    data.WorldsData["MtLavalava"].progression.SetResourceReference(ContentProperty, MtLavalavakey);
        //    broadcast.MtLavalavaProgression.SetResourceReference(ContentProperty, MtLavalavakey);

        //    string ATkey = data.ProgressKeys["ShiverMountain"][data.WorldsData["ShiverMountain"].progress];
        //    data.WorldsData["ShiverMountain"].progression.SetResourceReference(ContentProperty, ATkey);
        //}

        private void DropFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                //if (Path.GetExtension(files[0]).ToUpper() == ".HINT")
                //    LoadHints(files[0]);
                //else if (Path.GetExtension(files[0]).ToUpper() == ".PNACH")
                //    ParseSeed(files[0]);
                //else if (Path.GetExtension(files[0]).ToUpper() == ".ZIP")
                //    OpenKHSeed(files[0]);
                if (Path.GetExtension(files[0]).ToUpper().Equals(".TXT"))
                {
                    LoadSpoiler(files[0]);
                }
                //else if (Path.GetExtension(files[0]).ToUpper().Equals(".JSON"))
                //{
                //    LoadJSONSpioler(files[0]);
                //}
            }
        }

        public void LoadSpoiler(string fileName)
        {
            StreamReader fileReader = new StreamReader(fileName);
            string fileContents = fileReader.ReadToEnd();
            if (fileContents[0] == '{')
            {
                LoadJSONSpoiler(fileContents);
            }
            else
            {
                MessageBox.Show("Invalid spoiler log. Make sure the seed was generated after Jan 7, 2023 and that the first character in the log is \"{\".");
                //LoadTextSpoiler(fileName);
            }
        }

        private void LoadJSONSpoiler(string fileContents)
        {
            foreach (var world in data.WorldsData)
            {
                if (world.Value.hint is null) continue;
                world.Value.hint.Text = "0";
                world.Value.hint.Foreground = Brushes.Teal;
            }

            var jsonObject = JObject.Parse(fileContents);
            foreach (var region in data.WorldsData)
            {
                string regionName = region.Value.SpoilerLogRegion;
                if (!(jsonObject[regionName] is null))
                {
                    List<Check> checks = new List<Check>();
                    int itemValueTotal = 0;
                    foreach (JProperty item in jsonObject[regionName])
                    {
                        string name = item.Name;
                        string value = item.Value.ToString().Split(' ')[0];
                        Item checkItem;
                        try
                        {
                            Item matchingItem = data.Items.First(itemToUse => GetItemName(itemToUse).Equals(value));
                            checkItem= matchingItem;
                            itemValueTotal += MainWindow.itemValues[matchingItem.Tag.ToString()];
                        }
                        catch (Exception)
                        {
                            checkItem = null;
                        }

                        Check checkToAdd = new Check();
                        checkToAdd.Name = name;
                        checkToAdd.Value = value;
                        checkToAdd.Item= checkItem;
                        checks.Add(checkToAdd);
                    }
                    region.Value.Checks = checks;
                    if (region.Value.hint is null) continue;
                    region.Value.hint.Text = itemValueTotal.ToString();
                    region.Value.worldGrid.UpdateWorldHintColor();
                }

            }
            SeedLoadedDisplay.Header = "Seed Loaded";
        }

        private void LoadValues(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "PM64R Spoiler Log (*.txt)|*.txt",
                Title = "Select Spoiler Log",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true)
            {
                LoadTextSpoiler(openFileDialog.FileName);
            }
        }

        public void LoadTextSpoiler(string filename)
        {
            StreamReader streamReader = new StreamReader(filename);
            if(streamReader.EndOfStream)
            {
                MessageBox.Show("Invalid spoiler log. Make sure it's not empty.");
                //HintText.Content = "Error loading hints";
                streamReader.Close();
                return;
            }

            string line1 = streamReader.ReadLine();
            while (!line1.Trim().Equals(MainWindow.data.WorldsData["GoombaRegion"].SpoilerLogRegion))
            {
                line1 = streamReader.ReadLine();
            }
                //{
                //    MessageBox.Show("Invalid spoiler log.");
                //    streamReader.Close();
                //    return;
                //}

            var regionDict = new Dictionary<string, List<Item>>();
            string currentRegion = "GoombaRegion";
            List<Item> itemsinRegion = new List<Item>();
            while (!streamReader.EndOfStream)
            {
                string currentLine = streamReader.ReadLine();
                if (currentLine.Length == 0 && currentRegion.Length > 0)
                {
                    regionDict.Add(currentRegion, itemsinRegion.ToList<Item>());
                    currentRegion = "";
                    itemsinRegion.Clear();
                }
                else if (currentRegion.Length > 0 && currentLine[0] == ' ')
                {
                    string item = currentLine.Split(':')[1].Trim();
                    //if (item[item.Length - 1] == '*')
                    //{
                    //    item = item.Substring(0, item.Length - 1);
                    //}
                    try
                    {
                        Item matchingItem = data.Items.First(itemToUse => GetItemName(itemToUse).Equals(item));
                        itemsinRegion.Add(matchingItem);
                    }
                    catch(Exception)
                    {
                        continue;
                    }
                }
                else
                {
                    try
                    {
                        var matchingWorld = data.WorldsData.First(region => region.Value.SpoilerLogRegion.Equals(currentLine.Trim()));
                        currentRegion = matchingWorld.Key;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            foreach (var world in data.WorldsData)
            {
                if (world.Value.hint is null) continue;
                world.Value.hint.Text = "0";
                world.Value.hint.Foreground = Brushes.Teal;
            }

            foreach (var region in regionDict)
            {
                WorldData worldData = data.WorldsData[region.Key];
                int itemValueTotal = 0;
                foreach(Item item in region.Value)
                {
                    itemValueTotal += MainWindow.itemValues[item.Tag.ToString()];
                }
                if (worldData.hint is null) continue;
                worldData.hint.Text = itemValueTotal.ToString();
                if (itemValueTotal != 0) worldData.hint.Foreground = Brushes.White;
            }
        }

        private string GetItemName(Item item)
        {
            char lastChar = item.Name[item.Name.Length-1];
            if (char.IsUpper(lastChar))
            {
                return item.Name.Substring(0, item.Name.Length - 1);
            }
            else
            {
                return item.Name;
            }
        }

        private void LoadHints(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".hint";
            openFileDialog.Filter = "hint files (*.hint)|*.hint";
            openFileDialog.Title = "Select Hints File";
            if (openFileDialog.ShowDialog() == true)
            {
                LoadHints(openFileDialog.FileName);
            }
        }

        public void LoadHints(string filename)
        {
            SetMode(Mode.Hints);
            ResetHints();

            StreamReader streamReader = new StreamReader(filename);

            if (streamReader.EndOfStream)
            {
                //HintText.Content = "Error loading hints";
                streamReader.Close();
                return;
            }

            string line1 = streamReader.ReadLine();
            data.hintFileText[0] = line1;
            string[] reportvalues = line1.Split('.');

            if (streamReader.EndOfStream)
            {
                //HintText.Content = "Error loading hints";
                streamReader.Close();
                return;
            }

            string line2 = streamReader.ReadLine();
            data.hintFileText[1] = line2;
            line2 = line2.TrimEnd('.');
            string[] reportorder = line2.Split('.');

            LoadSettings(streamReader.ReadLine().Substring(24));

            streamReader.Close();

            for (int i = 0; i < reportorder.Length; ++i)
            {
                string location = data.codes.FindCode(reportorder[i]);
                if (location == "")
                    location = data.codes.GetDefault(i);

                data.reportLocations.Add(location);
                string[] temp = reportvalues[i].Split(',');
                data.reportInformation.Add(new Tuple<string, int>(data.codes.FindCode(temp[0]), int.Parse(temp[1]) - 32));
            }

            data.hintsLoaded = true;
            //HintText.Content = "Hints Loaded";
        }

        private void ResetHints()
        {
            data.hintsLoaded = false;
            data.reportLocations.Clear();
            data.reportInformation.Clear();
            data.reportAttempts = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].hinted = false;
            }
            data.WorldsData["FlowerFields"].hinted = true;

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].hintedHint = false;
            }

            foreach (ContentControl report in data.ReportAttemptVisual)
            {
                report.SetResourceReference(ContentProperty, "Fail0");
            }
            
            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                if (worldData.hint != null)
                    worldData.hint.Text = "?";
            }

            for (int i = 0; i < data.Reports.Count; ++i)
            {
                data.Reports[i].HandleItemReturn();
            }

            //broadcast.OnResetHints();
        }

        private void LoadSettings(string settings)
        {
            //bool[] newsettings = new bool[10];

            //string[] settinglist = settings.Split('-');
            //foreach (string setting in settinglist)
            //{
            //    string trimmed = setting.Trim();
            //    switch (trimmed)
            //    {
            //        case "Promise Charm":
            //            newsettings[0] = true;
            //            break;
            //        case "Secret Ansem Reports":
            //            newsettings[1] = true;
            //            break;
            //        case "Second Chance & Once More":
            //            newsettings[2] = true;
            //            break;
            //        case "Torn Pages":
            //            newsettings[3] = true;
            //            break;
            //        case "Cure":
            //            newsettings[4] = true;
            //            break;
            //        case "Final Form":
            //            newsettings[5] = true;
            //            break;
            //        case "Sora's Heart":
            //            newsettings[6] = true;
            //            break;
            //        case "Simulated Twilight Town":
            //            newsettings[7] = true;
            //            break;
            //        case "100 Acre Wood":
            //            newsettings[8] = true;
            //            break;
            //        case "ShiverMountain":
            //            newsettings[9] = true;
            //            break;
            //    }
            //}

            //PromiseCharmToggle(newsettings[0]);
            //ReportsToggle(newsettings[1]);
            //AbilitiesToggle(newsettings[2]);
            //TornPagesToggle(newsettings[3]);
            //CureToggle(newsettings[4]);
            //FinalFormToggle(newsettings[5]);
            //SoraHeartToggle(newsettings[6]);
            //SimulatedToggle(newsettings[7]);
            //ForeverForestToggle(newsettings[8]);
            //ShiverMountainToggle(newsettings[9]);

        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            //ModeDisplay.Header = "";
            data.mode = Mode.None;

            collected = 0;
            Collected.Text = collected.ToString();
            //HintText.Content = "";

            if (data.selected != null)
            {
                data.WorldsData[data.selected.Name].selectedBar.Source = new BitmapImage(new Uri("Images\\VerticalBarWhite.png", UriKind.Relative));
            }
            data.selected = null;

            foreach (WorldData worldData in data.WorldsData.Values.ToList())
            {
                for (int j = worldData.worldGrid.Children.Count - 1; j >= 0; --j)
                {
                    Item item = worldData.worldGrid.Children[j] as Item;
                    worldData.worldGrid.Children.Remove(worldData.worldGrid.Children[j]);
                    itemPools.First(p => p.Name.Equals(item.Tag)).Children.Add(item);

                    item.MouseDown -= item.Item_Return;
                    item.MouseEnter -= item.Report_Hover;
                    if (data.dragDrop)
                    {
                        item.MouseDoubleClick -= item.Item_Click;
                        item.MouseDoubleClick += item.Item_Click;
                        item.MouseMove -= item.Item_MouseMove;
                        item.MouseMove += item.Item_MouseMove;
                    }
                    else
                    {
                        item.MouseDown -= item.Item_MouseDown;
                        item.MouseDown += item.Item_MouseDown;
                        item.MouseUp -= item.Item_MouseUp;
                        item.MouseUp += item.Item_MouseUp;
                    }
                }
            }

            // Reset 1st column row heights
            RowDefinitionCollection rows1 = ((data.WorldsData["GoombaRegion"].worldGrid.Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows1)
            {
                // don't reset turned off worlds
                if (row.Height.Value != 0)
                    row.Height = new GridLength(1, GridUnitType.Star);
            }

            // Reset 2nd column row heights
            RowDefinitionCollection rows2 = ((data.WorldsData["ToadTown"].worldGrid.Parent as Grid).Parent as Grid).RowDefinitions;
            foreach (RowDefinition row in rows2)
            {
                // don't reset turned off worlds
                if (row.Height.Value != 0)
                    row.Height = new GridLength(1, GridUnitType.Star);
            }

            //ReportsToggle(true);
            //ReportRow.Height = new GridLength(1, GridUnitType.Star);
            //ResetHints();

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[key].hinted = false;
                data.WorldsData[key].hintedHint = false;
                data.WorldsData[key].complete = false;
                data.WorldsData[key].checkCount.Clear();
                data.WorldsData[key].progress = 0;
            }
            //data.WorldsData["FlowerFields"].hinted = true;            

            //broadcast.KoopaRegionProgression.SetResourceReference(ContentProperty, "");
            //broadcast.KoopaFortressProgression.SetResourceReference(ContentProperty, "");
            //broadcast.DryDryRuinsProgression.SetResourceReference(ContentProperty, "");
            //broadcast.MtRuggedProgression.SetResourceReference(ContentProperty, "");
            //broadcast.DryDryOutpostProgression.SetResourceReference(ContentProperty, "");
            //broadcast.JadeJungleProgression.SetResourceReference(ContentProperty, "");
            //broadcast.TubbaCastleProgression.SetResourceReference(ContentProperty, "");
            //broadcast.ShyGuyToyboxProgression.SetResourceReference(ContentProperty, "");
            //broadcast.DryDryDesertProgression.SetResourceReference(ContentProperty, "");
            //broadcast.BooMansionProgression.SetResourceReference(ContentProperty, "");
            //broadcast.GustyGulchProgression.SetResourceReference(ContentProperty, "");
            //broadcast.ForeverForestProgression.SetResourceReference(ContentProperty, "");
            //broadcast.ToadTownTunnelsProgression.SetResourceReference(ContentProperty, "");
            //broadcast.MtLavalavaProgression.SetResourceReference(ContentProperty, "");

            //KoopaRegionProgression.SetResourceReference(ContentProperty, "");
            //KoopaFortressProgression.SetResourceReference(ContentProperty, "");
            //DryDryRuinsProgression.SetResourceReference(ContentProperty, "");
            //MtRuggedProgression.SetResourceReference(ContentProperty, "");
            //DryDryOutpostProgression.SetResourceReference(ContentProperty, "");
            //JadeJungleProgression.SetResourceReference(ContentProperty, "");
            //TubbaCastleProgression.SetResourceReference(ContentProperty, "");
            //ShyGuyToyboxProgression.SetResourceReference(ContentProperty, "");
            //DryDryDesertProgression.SetResourceReference(ContentProperty, "");
            //BooMansionProgression.SetResourceReference(ContentProperty, "");
            //GustyGulchProgression.SetResourceReference(ContentProperty, "");
            //ForeverForestProgression.SetResourceReference(ContentProperty, "");
            //ToadTownTunnelsProgression.SetResourceReference(ContentProperty, "");
            //MtLavalavaProgression.SetResourceReference(ContentProperty, "");

            //LevelIcon.Visibility = Visibility.Hidden;
            //Level.Visibility = Visibility.Hidden;
            //StrengthIcon.Visibility = Visibility.Hidden;
            //Strength.Visibility = Visibility.Hidden;
            //MagicIcon.Visibility = Visibility.Hidden;
            //Magic.Visibility = Visibility.Hidden;
            //DefenseIcon.Visibility = Visibility.Hidden;
            //Defense.Visibility = Visibility.Hidden;
            //Weapon.Visibility = Visibility.Hidden;

            broadcast.LevelIcon.Visibility = Visibility.Hidden;
            broadcast.Level.Visibility = Visibility.Hidden;
            broadcast.StrengthIcon.Visibility = Visibility.Hidden;
            broadcast.Strength.Visibility = Visibility.Hidden;
            broadcast.MagicIcon.Visibility = Visibility.Hidden;
            broadcast.Magic.Visibility = Visibility.Hidden;
            broadcast.DefenseIcon.Visibility = Visibility.Hidden;
            broadcast.Defense.Visibility = Visibility.Hidden;
            broadcast.Weapon.Visibility = Visibility.Hidden;

            broadcast.WorldRow.Height = new GridLength(7, GridUnitType.Star);
            broadcast.GrowthAbilityRow.Height = new GridLength(0, GridUnitType.Star);
            //FormRow.Height = new GridLength(0, GridUnitType.Star);

            //ValorM.Opacity = .25;
            //WisdomM.Opacity = .25;
            //LimitM.Opacity = .25;
            //MasterM.Opacity = .25;
            //FinalM.Opacity = .25;

            //HighJump.Opacity = .25;
            //QuickRun.Opacity = .25;
            //DodgeRoll.Opacity = .25;
            //AerialDodge.Opacity = .25;
            //Glide.Opacity = .25;

            //ValorLevel.Source = null;
            //WisdomLevel.Source = null;
            //LimitLevel.Source = null;
            //MasterLevel.Source = null;
            //FinalLevel.Source = null;

            //HighJumpLevel.Source = null;
            //QuickRunLevel.Source = null;
            //DodgeRollLevel.Source = null;
            //AerialDodgeLevel.Source = null;
            //GlideLevel.Source = null;
            
            // Reset / Turn off auto tracking
            collectedChecks.Clear();
            newChecks.Clear();
            if (aTimer != null)
                aTimer.Stop();

            fireLevel = 0;
            blizzardLevel = 0;
            thunderLevel = 0;
            cureLevel = 0;
            reflectLevel = 0;
            magnetLevel = 0;
            tornPageCount = 0;

            if (fire != null)
                fire.Level = 0;
            if (blizzard != null)
                blizzard.Level = 0;
            if (thunder != null)
                thunder.Level = 0;
            if (cure != null)
                cure.Level = 0;
            if (reflect != null)
                reflect.Level = 0;
            if (magnet != null)
                magnet.Level = 0;
            if (pages != null)
                pages.Quantity = 0;

            broadcast.OnReset();
            broadcast.UpdateNumbers();
        }
        
        private void BroadcastWindow_Open(object sender, RoutedEventArgs e)
        {
            broadcast.Show();
        }

        private void ParseSeed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".pnach";
            openFileDialog.Filter = "pnach files (*.pnach)|*.pnach";
            openFileDialog.Title = "Select Seed File";
            if (openFileDialog.ShowDialog() == true)
            {
                ParseSeed(openFileDialog.FileName);
            }
        }

        public void ParseSeed(string filename)
        {
            SetMode(Mode.AltHints);

            foreach (string world in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[world].checkCount.Clear();
            }

            StreamReader streamReader = new StreamReader(filename);
            bool check1 = false;
            bool check2 = false;

            while (streamReader.EndOfStream == false)
            {
                string line = streamReader.ReadLine();

                // ignore comment lines
                if (line.Length >= 2 && line[0] == '/' && line[1] == '/')
                    continue;

                string[] codes = line.Split(',');
                if (codes.Length == 5)
                {
                    string world = data.codes.FindCode(codes[2]);

                    //stupid fix
                    string[] idCode = codes[4].Split('/', ' ');

                    int id = Convert.ToInt32(idCode[0], 16);
                    if (world == "" || world == "FlowerFields" || data.codes.itemCodes.ContainsKey(id) == false || (id >= 226 && id <= 238))
                        continue;

                    string item = data.codes.itemCodes[Convert.ToInt32(codes[4], 16)];
                    data.WorldsData[world].checkCount.Add(item);
                }
                else if (codes.Length == 1)
                {
                    if (codes[0] == "//Remove High Jump LVl" || codes[0] == "//Remove Quick Run LVl")
                    {
                        check1 = true;
                    }
                    else if (codes[0] == "//Remove Dodge Roll LVl")
                    {
                        check2 = true;
                    }
                }
            }
            streamReader.Close();

            if (check1 == true && check2 == false)
            {
                foreach (string world in data.WorldsData.Keys.ToList())
                {
                    data.WorldsData[world].checkCount.Clear();
                }
            }

            foreach (var key in data.WorldsData.Keys.ToList())
            {
                if (key == "FlowerFields")
                    continue;

                data.WorldsData[key].worldGrid.WorldComplete();
                SetReportValue(data.WorldsData[key].hint, 1);
            }
        }

        private void SetMode(Mode mode)
        {
            if ((data.mode != mode && data.mode != Mode.None) || mode == Mode.AltHints || mode == Mode.OpenKHAltHints)
                OnReset(null, null);

            if (mode == Mode.AltHints || mode == Mode.OpenKHAltHints)
            {
                ModeDisplay.Header = "Alt Hints Mode";
                data.mode = mode;
                //ReportsToggle(false);
                //ReportRow.Height = new GridLength(0, GridUnitType.Star);
            }
            else if (mode == Mode.Hints || mode == Mode.OpenKHHints)
            {
                ModeDisplay.Header = "Hints Mode";
                data.mode = mode;
                //ReportRow.Height = new GridLength(1, GridUnitType.Star);
            }
        }

        private void OpenKHSeed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".zip";
            openFileDialog.Filter = "OpenKH Seeds (*.zip)|*.zip";
            openFileDialog.Title = "Select Seed File";
            if (openFileDialog.ShowDialog() == true)
            {
                OpenKHSeed(openFileDialog.FileName);
            }
        }

        private Dictionary<string, string> convertOpenKH = new Dictionary<string, string>()
        {
            {"Level", "GoombaRegion" },
            {"Form Levels", "ToadTown" },
            {"Simulated Twilight Town", "ToadTownTunnels" },
            {"Twilight Town", "KoopaRegion" },
            {"Hollow Bastion", "KoopaFortress" },
            {"Beast's Castle", "MtRugged" },
            {"Olympus Coliseum", "DryDryOutpost" },
            {"DryDryDesert", "DryDryDesert" },
            {"Land of Dragons", "DryDryRuins" },
            {"Hundred Acre Wood", "ForeverForest" },
            {"Pride Lands", "BooMansion" },
            {"Disney Castle / Timeless River", "GustyGulch" },
            {"Halloween Town", "TubbaCastle" },
            {"Port Royal", "ShyGuyToybox" },
            {"Space Paranoids", "JadeJungle" },
            {"The World That Never Was", "MtLavalava" },
            {"ShiverMountain", "ShiverMountain" },
            {"Proof of Connection", "Connection" },
            {"Proof of Nonexistence", "Nonexistence" },
            {"Proof of Peace", "Peace" },
            {"PromiseCharm", "PromiseCharm" },
            {"Valor Form", "Valor" },
            {"Wisdom Form", "Wisdom" },
            {"Limit Form", "Limit" },
            {"Master Form", "Master" },
            {"Final Form", "Final" },
            {"Fire Element", "Fire" },
            {"Blizzard Element", "Blizzard" },
            {"Thunder Element", "Thunder" },
            {"Cure Element", "Cure" },
            {"Magnet Element", "Magnet" },
            {"Reflect Element", "Reflect" },
            {"Ukulele Charm (Stitch)", "Ukulele" },
            {"Baseball Charm (Chicken Little)", "Baseball" },
            {"Lamp Charm (Genie)", "Lamp" },
            {"Feather Charm (Peter Pan)", "Feather" },
            {"Torn Pages", "TornPage" },
            {"Second Chance", "SecondChance" },
            {"Once More", "OnceMore" },
            {"", "FlowerFields"}
        };



        private void OpenKHSeed(string filename)
        {
            foreach (string world in data.WorldsData.Keys.ToList())
            {
                data.WorldsData[world].checkCount.Clear();
            }
            using (ZipArchive archive = ZipFile.OpenRead(filename))
            {
                
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".Hints"))
                    {
                        using (StreamReader reader = new StreamReader(entry.Open()))
                        {
                            data.openKHHintText = reader.ReadToEnd();
                            var hintText = Encoding.UTF8.GetString(Convert.FromBase64String(data.openKHHintText));
                            var hintObject = JsonSerializer.Deserialize<Dictionary<string,object>>(hintText);
                            switch (hintObject["hintsType"].ToString())
                            {
                                case "Shananas":
                                    SetMode(Mode.OpenKHAltHints);
                                    var worlds = JsonSerializer.Deserialize<Dictionary<string,List<string>>>(hintObject["world"].ToString());

                                    foreach (var world in worlds)
                                    {
                                        if (world.Key == "Critical Bonuses" || world.Key == "Garden of Assemblage")
                                        {
                                            continue;
                                        }
                                        foreach (var item in world.Value)
                                        {
                                            data.WorldsData[convertOpenKH[world.Key]].checkCount.Add(convertOpenKH[item]);
                                        }

                                    }
                                    foreach (var key in data.WorldsData.Keys.ToList())
                                    {
                                        if (key == "FlowerFields")
                                            continue;

                                        data.WorldsData[key].worldGrid.WorldComplete();
                                        SetReportValue(data.WorldsData[key].hint, 1);
                                    }

                                    break;

                                case "JSmartee":
                                    SetMode(Mode.OpenKHHints);
                                    var reports = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string,object>>>(hintObject["Reports"].ToString());

                                    List<int> reportKeys = reports.Keys.Select(int.Parse).ToList();
                                    reportKeys.Sort();

                                    foreach (var report in reportKeys)
                                    {
                                        var world = convertOpenKH[reports[report.ToString()]["World"].ToString()];
                                        var count = reports[report.ToString()]["Count"].ToString();
                                        var location = convertOpenKH[reports[report.ToString()]["Location"].ToString()];
                                        data.reportInformation.Add(new Tuple<string, int>(world, int.Parse(count)));
                                        data.reportLocations.Add(location);
                                    }
                                    //ReportsToggle(true);
                                    data.hintsLoaded = true;
                                    //HintText.Content = "Hints Loaded";

                                    break;

                                default:
                                    break;
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
