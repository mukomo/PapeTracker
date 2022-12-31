using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapeTracker
{
    class World
    {
        private Dictionary<int, string> worldCodes;

        public string previousworldName;
        private string world;
        public string worldName
        {
            get { return world; }
            set
            {
                if (world != value)
                {
                    world = value;
                    if (App.logger != null)
                        App.logger.RecordWorld(value);
                }
            }
        }
        private int worldNum;
        private int worldAddress;
        private int eventCompleteAddress;
        private int SttAddress;

        public int roomNumber;
        public int eventID1;
        public int eventID2;
        public int eventID3;
        public int eventComplete;
        public int inStt;

        public int ADDRESS_OFFSET;

        MemoryReader memory;

        public World(MemoryReader mem, int offset, int address, int completeAddress, int sttAddress)
        {
            ADDRESS_OFFSET = offset;
            memory = mem;
            worldAddress = address;
            eventCompleteAddress = completeAddress;
            SttAddress = sttAddress;

            worldCodes = new Dictionary<int, string>();
            worldCodes.Add(02, "KoopaRegion");
            worldCodes.Add(03, "DestinyIsland");
            worldCodes.Add(04, "KoopaFortress");
            worldCodes.Add(05, "MtRugged");
            worldCodes.Add(06, "DryDryOutpost");
            worldCodes.Add(07, "DryDryDesert");
            worldCodes.Add(08, "DryDryRuins");
            worldCodes.Add(09, "ForeverForest");
            worldCodes.Add(10, "BooMansion");
            worldCodes.Add(11, "ShiverMountain");
            worldCodes.Add(12, "GustyGulch");
            worldCodes.Add(13, "GustyGulch"); // Timeless River
            worldCodes.Add(14, "TubbaCastle");
            worldCodes.Add(16, "ShyGuyToybox");
            worldCodes.Add(17, "JadeJungle");
            worldCodes.Add(18, "MtLavalava");
            worldCodes.Add(255, "FlowerFields");
        }

        public void UpdateMemory()
        {
            previousworldName = worldName;

            byte[] worldData = memory.ReadMemory(worldAddress + ADDRESS_OFFSET, 9);
            worldNum = worldData[0];
            roomNumber = worldData[1];
            eventID1 = worldData[4];
            eventID2 = worldData[6];
            eventID3 = worldData[8];

            byte[] eventData = memory.ReadMemory(eventCompleteAddress + ADDRESS_OFFSET, 1);
            eventComplete = eventData[0];

            byte[] sttData = memory.ReadMemory(SttAddress + ADDRESS_OFFSET, 1);
            inStt = sttData[0];


            string tempWorld;
            if (worldCodes.ContainsKey(worldNum))
            {
                tempWorld = worldCodes[worldNum];
            }
            else
            {
                tempWorld = "";
            }
            
            // Handle AS fights
            if (tempWorld == "KoopaFortress")
            {
                if (roomNumber == 26)
                    worldName = "FlowerFields";
                else if (roomNumber == 32)
                    worldName = "TubbaCastle"; // Vexen
                else if (roomNumber == 33 && (eventID3 == 122 || eventID1 == 123 || eventID1 == 142     // AS Lexaeus
                                            || eventID3 == 132 || eventID1 == 133 || eventID1 == 147))  // Data Lexaeus
                    worldName = "DryDryDesert"; // Lexaeus
                else if (roomNumber == 33 && (eventID3 == 128 || eventID1 == 129 || eventID1 == 143     // AS Larxene
                                            || eventID3 == 138 || eventID1 == 139 || eventID1 == 148))  // Data Larxene
                    worldName = "JadeJungle"; // Larxene
                else if (roomNumber == 34)
                    worldName = "DryDryOutpost"; // Zexion
                else if (roomNumber == 38)
                    worldName = "GustyGulch"; // Marluxia
                else
                    worldName = "KoopaFortress";
            }
            // Handle STT
            else if (tempWorld == "KoopaRegion")
            {
                if (inStt == 13)
                    worldName = "ToadTownTunnels";
                else if ((roomNumber == 32 && eventID1 == 1) || (roomNumber == 1 && eventID1 == 52))
                    worldName = "FlowerFields"; // Crit bonuses
                else
                    worldName = "KoopaRegion";
            }
            // Handle Data fights
            else if (tempWorld == "MtLavalava")
            {
                if (roomNumber == 10 && (eventID1 == 108))
                    worldName = "DryDryRuins"; // Xigbar
                else if (roomNumber == 15 && (eventID1 == 110))
                    worldName = "BooMansion"; // Saix
                else if (roomNumber == 14 && (eventID1 == 112))
                    worldName = "ShyGuyToybox"; // Luxord
                else if (roomNumber == 21 && (eventID1 == 114))
                    worldName = "ToadTownTunnels"; // Roxas
                else
                    worldName = "MtLavalava";
            }
            else
            {
                if (worldName != tempWorld && tempWorld != "")
                {
                    worldName = tempWorld;
                }
            }

            //(App.Current.MainWindow as MainWindow).HintText.Content = worldName;
        }
    }
}
