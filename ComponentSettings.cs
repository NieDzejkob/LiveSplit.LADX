using System;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;

namespace LiveSplit.PokemonRB {
    public partial class PokemonRBSettings : UserControl {
        public int category;
        public bool AutoReset;
        public Split[][] SplitsByCategory = new Split[][] {
            new Split[] {                                                                                                           // Any% No Save Corruption
                new Split.EventFlagSplit("nsc_rival", "Rival Fight", "eventD74B", 3),                                                   // EVENT_BATTLED_RIVAL_IN_OAKS_LAB
                new Split.EventFlagSplit("nsc_parcel1", "Get Parcel", "eventD74E", 1),                                                  // EVENT_GOT_OAKS_PARCEL
                new Split.EventFlagSplit("nsc_parcel2", "Deliver Parcel", "eventD74E", 0),                                              // EVENT_OAK_GOT_PARCEL
                new Split.ExitMapSplit("nsc_mart1", "Viridian Shopping - when you exit the mart (ignores the parcel visit)", 0x2A),     // VIRIDIAN_MART
                new Split.HardResetSplit("nsc_manip1", "Pidgey Manip Reset"),
                new Split.PartyCountSplit("nsc_pidgey", "Get Pidgey", 2),
                new Split.CurrentMapSplit("nsc_enter_forest", "Enter Forest", 0x33),                                                    // VIRIDIAN_FOREST
                new Split.EventFlagSplit("nsc_weedle", "Defeat the Bug Catcher", "eventD7F3", 4),                                       // EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_2
                new Split.CurrentMapSplit("nsc_exit_forest", "Exit Forest", 0x2F),                                                      // VIRIDIAN_FOREST_EXIT
                new Split.ExitMapSplit("nsc_mart2", "Pewter Shopping - when you exit the mart", 0x38),                                  // PEWTER_MART
                new Split.HardResetSplit("nsc_btw", "Brock Through Walls Reset"),
                new Split.CurrentMapSplit("nsc_dungeon", "Enter Unknown Dungeon", 0xE4),                                                // UNKNOWN_DUNGEON_1
                new Split.HardResetSplit("nsc_manip2", "Ditto Manip Reset"),
                new Split.PartyCountSplit("nsc_ditto", "Get Ditto", 3),
                new Split.PartyCountSplit("nsc_glitch1", "Get Rhydon", 4),
                new Split.PartyCountSplit("nsc_glitch2", "Get F4", 5)
            }, new Split[] {                                                                                                        // Reverse Badge Acquisition
                new Split.EventFlagSplit("rba_rival", "Rival Fight", "eventD74B", 3),                                                   // EVENT_BATTLED_RIVAL_IN_OAKS_LAB
                new Split.EventFlagSplit("rba_parcel1", "Get Parcel", "eventD74E", 1),                                                  // EVENT_GOT_OAKS_PARCEL
                new Split.EventFlagSplit("rba_parcel2", "Deliver Parcel", "eventD74E", 0),                                              // EVENT_OAK_GOT_PARCEL
                new Split.ExitMapSplit("rba_mart1", "Viridian Shopping - when you exit the mart (ignores the parcel visit)", 0x2A),     // VIRIDIAN_MART
                new Split.HardResetSplit("rba_manip1", "Pidgey Manip Reset"),
                new Split.PartyCountSplit("rba_pidgey", "Get Pidgey", 2),
                new Split.CurrentMapSplit("rba_enter_forest", "Enter Forest", 0x33),                                                    // VIRIDIAN_FOREST
                new Split.EventFlagSplit("rba_weedle", "Defeat the Bug Catcher", "eventD7F3", 4),                                       // EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_2
                new Split.CurrentMapSplit("rba_exit_forest", "Exit Forest", 0x2F),                                                      // VIRIDIAN_FOREST_EXIT
                new Split.ExitMapSplit("rba_mart2", "Pewter Shopping - when you exit the mart", 0x38),                                  // PEWTER_MART
                new Split.HardResetSplit("rba_btw", "Brock Through Walls Reset"),
                new Split.CurrentMapSplit("rba_dungeon", "Enter Unknown Dungeon", 0xE4),                                                // UNKNOWN_DUNGEON_1
                new Split.HardResetSplit("rba_manip2", "Ditto Manip Reset"),
                new Split.PartyCountSplit("rba_ditto", "Get Ditto", 3),
                new Split.PartyCountSplit("rba_missingno1", "Missingno 1", 4),
                new Split.PartyCountSplit("rba_missingno2", "Missingno 2", 5),
                new Split.PartyCountSplit("rba_mewtwo", "Mewtwo", 6),
                new Split.EventFlagSplit("rba_giovanni", "Giovanni", "wObtainedBadges", 7),
                new Split.EventFlagSplit("rba_blaine", "Blaine", "wObtainedBadges", 6),
                new Split.EventFlagSplit("rba_sabrina", "Sabrina", "wObtainedBadges", 5),
                new Split.EventFlagSplit("rba_koga", "Koga", "wObtainedBadges", 4),
                new Split.EventFlagSplit("rba_erika", "Erika", "wObtainedBadges", 3),
                new Split.EventFlagSplit("rba_surge", "Lt. Surge", "wObtainedBadges", 2),
                new Split.EventFlagSplit("rba_misty", "Misty", "wObtainedBadges", 1),
                new Split.EventFlagSplit("rba_brock", "Brock", "wObtainedBadges", 0),
            }, new Split[] {                                                                                                        // Catch 'em All
                new Split.EventFlagSplit("151_rival", "Rival Fight", "eventD74B", 3),                                                   // EVENT_BATTLED_RIVAL_IN_OAKS_LAB
                new Split.EventFlagSplit("151_parcel1", "Get Parcel", "eventD74E", 1),                                                  // EVENT_GOT_OAKS_PARCEL
                new Split.EventFlagSplit("151_parcel2", "Deliver Parcel", "eventD74E", 0),                                              // EVENT_OAK_GOT_PARCEL
                new Split.ExitMapSplit("151_mart1", "Viridian Shopping - when you exit the mart (ignores the parcel visit)", 0x2A),     // VIRIDIAN_MART
                new Split.HardResetSplit("151_manip1", "Pidgey Manip Reset"),
                new Split.PartyCountSplit("151_pidgey", "Get Pidgey", 2),
                new Split.CurrentMapSplit("151_enter_forest", "Enter Forest", 0x33),                                                    // VIRIDIAN_FOREST
                new Split.EventFlagSplit("151_weedle", "Defeat the Bug Catcher", "eventD7F3", 4),                                       // EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_2
                new Split.CurrentMapSplit("151_exit_forest", "Exit Forest", 0x2F),                                                      // VIRIDIAN_FOREST_EXIT
                new Split.HardResetSplit("151_btw", "Brock Through Walls Reset"),
                new Split.CurrentMapSplit("151_enter_center", "Enter Saffron Center", 0xB6),                                            // SAFFRON_POKECENTER
                new Split.CurrentMapSplit("151_exit_center", "Exit Saffron Center", 0x0A),                                              // SAFFRON_CITY
                new Split.HardResetSplit("151_deathfly1", "First Deathfly Manip Reset"),
                new Split.HardResetSplit("151_deathfly2", "Second Deathfly Manip Reset"),
                new Split.CurrentMapSplit("151_monster", "Monster Menu (enter dojo)", 0xB1),                                            // FIGHTING_DOJO
                new Split.CurrentMapSplit("151_plant", "Power Plant (warp to cinnabar)", 0x08),                                         // CINNABAR_ISLAND
                new Split.ExitMapSplit("151_mansion", "Cinnabar Mansion (exit mansion)", 0xA5),                                         // MANSION_1
                new Split.CurrentMapSplit("151_safari", "Safari Zone (fly to pewter)", 0x02),                                           // PEWTER_CITY
                new Split.CurrentMapSplit("151_dratini", "Dratini (fly to viridian)", 0x01),                                            // VIRIDIAN_CITY
                new Split.CurrentMapSplit("151_cerulean_cave", "Cerulean Cave (fly to vermillion)", 0x05),                              // VERMILION_CITY
                new Split.CurrentMapSplit("151_interlude1", "\"Interlude 1\" (enter viridian forest)", 0x33),                           // VIRIDIAN_FOREST
                new Split.HardResetSplit("151_forest", "Viridian Forest (dugtrio manip reset)"),
                new Split.CurrentMapSplit("151_enter_vroad", "Enter Victory Road", 0xC6),                                               // VICTORY_ROAD_3
                new Split.CurrentMapSplit("151_enter_seafoam", "Enter Seaform Island", 0xA1),                                           // SEAFOAM_ISLANDS_4
            }, new Split[] {                                                                                                        // Glitchless
                new Split.EventFlagSplit("gle_rival", "Rival Fight", "eventD74B", 3),                                                   // EVENT_BATTLED_RIVAL_IN_OAKS_LAB
                new Split.EventFlagSplit("gle_parcel1", "Get Parcel", "eventD74E", 1),                                                  // EVENT_GOT_OAKS_PARCEL
                new Split.EventFlagSplit("gle_parcel2", "Deliver Parcel", "eventD74E", 0),                                              // EVENT_OAK_GOT_PARCEL
                new Split.ExitMapSplit("gle_mart1", "Viridian Shopping - when you exit the mart (ignores the parcel visit)", 0x2A),     // VIRIDIAN_MART
                new Split.HardResetSplit("gle_manip1", "Nidoran Manip Reset"),
                new Split.PartyCountSplit("gle_nidoran", "Nidoran", 2),
                new Split.CurrentMapSplit("gle_enter_forest", "Enter Forest", 0x33),                                                    // VIRIDIAN_FOREST
                new Split.EventFlagSplit("gle_weedle", "Forest Bug Catcher", "eventD7F3", 4),                                           // EVENT_BEAT_VIRIDIAN_FOREST_TRAINER_2
                new Split.CurrentMapSplit("gle_exit_forest", "Exit Forest", 0x2F),                                                      // VIRIDIAN_FOREST_EXIT
                new Split.EventFlagSplit("gle_brock", "Brock", "wObtainedBadges", 0),
                new Split.CurrentMapSplit("gle_moon_enter", "Enter Mt. Moon", 0x3B),                                                    // MT_MOON_1
                new Split.CurrentMapSplit("gle_moon_exit", "Exit Mt. Moon", 0x0F),                                                      // ROUTE_4
                new Split.EventFlagSplit("gle_nugget6", "Nugget Rocket", "eventD7EF", 1),                                               // EVENT_BEAT_ROUTE24_ROCKET
                new Split.EventFlagSplit("gle_misty", "Misty", "wObtainedBadges", 1),
                new Split.EventFlagSplit("gle_cut", "Obtain Cut", "eventD803", 0),                                                      // EVENT_GOT_HM01
                new Split.HardResetSplit("gle_manip3", "Cans Manip Reset"),
                new Split.EventFlagSplit("gle_surge", "Lt. Surge", "wObtainedBadges", 2),
                new Split.EventFlagSplit("gle_fly", "Obtain Fly", "eventD7E0", 6),                                                      // EVENT_GOT_HM02
                new Split.EventFlagSplit("gle_tower_rival", "Pokemon Tower Rival", "eventD764", 7),                                     // EVENT_BEAT_POKEMON_TOWER_RIVAL
                new Split.EventFlagSplit("gle_flute", "Poke Flute", "eventD76C", 0),                                                    // EVENT_GOT_POKE_FLUTE
                new Split.EventFlagSplit("gle_surf", "Obtain Surf", "eventD857", 0),                                                    // EVENT_GOT_HM03
                new Split.EventFlagSplit("gle_silph_giovanni", "Silph Giovanni", "eventD838", 7),                                       // EVENT_BEAT_SILPH_CO_GIOVANNI
                new Split.EventFlagSplit("gle_koga", "Koga", "wObtainedBadges", 4),
                new Split.EventFlagSplit("gle_strength", "Obtain Strength", "eventD78E", 0),                                            // EVENT_GOT_HM04
                new Split.EventFlagSplit("gle_erika", "Erika", "wObtainedBadges", 3),
                new Split.EventFlagSplit("gle_blaine", "Blaine", "wObtainedBadges", 6),
                new Split.EventFlagSplit("gle_sabrina", "Sabrina", "wObtainedBadges", 5),
                new Split.EventFlagSplit("gle_giovanni", "Giovanni", "wObtainedBadges", 7),
                new Split.CurrentMapSplit("gle_plateau", "Enter Indigo Plateau Lobby", 0xAE),                                           // INDIGO_PLATEAU_LOBBY
                new Split.EventFlagSplit("gle_lorelei", "Lorelei", "eventD863", 1),                                                     // EVENT_BEAT_LORELEIS_ROOM_TRAINER_0
                new Split.EventFlagSplit("gle_bruno", "Bruno", "eventD864", 1),                                                         // EVENT_BEAT_BRUNOS_ROOM_TRAINER_0
                new Split.EventFlagSplit("gle_agatha", "Agatha", "eventD865", 1),                                                       // EVENT_BEAT_AGATHAS_ROOM_TRAINER_0
                new Split.EventFlagSplit("gle_lance", "Lance", "eventD866", 1),                                                         // EVENT_BEAT_LANCES_ROOM_TRAINER_0
                new Split.EventFlagSplit("gle_champion", "Champion", "eventD867", 1),                                                   // EVENT_BEAT_CHAMPION_RIVAL
            }
        };

        public PokemonRBSettings() {
            InitializeComponent();
            this.comboBox1.SelectedIndex = category = 0;
            this.lblVersion.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

            AutoReset = false;
        }

        public XmlNode GetSettings(XmlDocument document) {
            if (checkedListBox1.Items.Count == SplitsByCategory[category].Length) {
                for (int i = 0; i < checkedListBox1.Items.Count; i++) {
                    SplitsByCategory[category][i].enabled = (checkedListBox1.GetItemCheckState(i) == CheckState.Checked);
                }
            }

            var settingsNode = document.CreateElement("Settings");

            settingsNode.AppendChild(ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(ToElement(document, "AutoReset", AutoReset.ToString()));
            settingsNode.AppendChild(ToElement(document, "Category", category.ToString()));

            foreach (Split[] category in SplitsByCategory) {
                foreach (Split split in category) {
                    settingsNode.AppendChild(ToElement(document, "split_" + split.splitID, split.enabled.ToString()));
                }
            }

            return settingsNode;
        }

        public void SetSettings(XmlNode settings) {
            var element = (XmlElement)settings;
            if (!element.IsEmpty) {
                Version version;
                if (element["Version"] != null) {
                    version = Version.Parse(element["Version"].InnerText);
                } else {
                    version = new Version(1, 0, 0);
                }

                if (element["AutoReset"] != null) {
                    AutoReset = Convert.ToBoolean(element["AutoReset"].InnerText);
                    chkAutoReset.Checked = AutoReset;
                }

                foreach (Split[] category in SplitsByCategory) {
                    foreach (Split split in category) {
                        if (element["split_" + split.splitID] != null) {
                            split.enabled = Convert.ToBoolean(element["split_" + split.splitID].InnerText);
                        }
                    }
                }

                if (element["Category"] != null) {
                    category = Convert.ToInt32(element["Category"].InnerText);
                }

                comboBox1.SelectedIndex = category;
                this.checkedListBox1.Items.Clear();

                foreach (Split split in SplitsByCategory[category]) {
                    this.checkedListBox1.Items.Add(split.name, split.enabled);
                }
            }
        }

        private void checkAutoReset_CheckedChanged(object sender, EventArgs e) {
            AutoReset = chkAutoReset.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (checkedListBox1.Items.Count == SplitsByCategory[category].Length) { // this requires unique split counts, it might be needed to add special cases
                for (int i = 0; i < checkedListBox1.Items.Count; i++) {
                    SplitsByCategory[category][i].enabled = (checkedListBox1.GetItemCheckState(i) == CheckState.Checked);
                }
            }

            category = comboBox1.SelectedIndex;
            this.checkedListBox1.Items.Clear();

            foreach (Split split in SplitsByCategory[category]) {
                this.checkedListBox1.Items.Add(split.name, split.enabled);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e) {
            if(checkedListBox1.Items.Count == SplitsByCategory[category].Length) {
                for(int i = 0;i < checkedListBox1.Items.Count; i++) {
                    SplitsByCategory[category][i].enabled = (checkedListBox1.GetItemCheckState(i) == CheckState.Checked);
                }
            }
        }

        private XmlElement ToElement<T>(XmlDocument document, String name, T value)
        {
            var element = document.CreateElement(name);
            element.InnerText = value.ToString();
            return element;
        }
    }
}
