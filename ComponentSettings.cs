using System;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;

namespace LiveSplit.PokemonRB {
    public partial class PokemonRBSettings : UserControl {
        int category;
        public bool AutoStartTimer, AutoReset;
        Split[][] SplitsByCategory = new Split[][] {
            new Split[] { // Any% No Save Corruption
                    new Split.EventFlagSplit("nsc_rival", "Rival Fight", false, -1, -1),
                    new Split.EventFlagSplit("nsc_parcel1", "Get Parcel", false, -1, -1),
                    new Split.EventFlagSplit("nsc_parcel2", "Deliver Parcel", false, -1, -1),
                    new Split.ViridianMartSplit("nsc_mart1", "Viridian Shopping - when you exit the mart (ignores the parcel visit)", false),
                    new Split.HardResetSplit("nsc_manip1", "Pidgey Manip Reset", false),
                    new Split.PartyCountSplit("nsc_pidgey", "Get Pidgey", false, 2),
                    new Split.CurrentMapSplit("nsc_enter_forest", "Enter Forest", false, -1),
                    new Split.EventFlagSplit("nsc_weedle", "Defeat the Bug Catcher", false, -1, -1),
                    new Split.CurrentMapSplit("nsc_exit_forest", "Exit Forest", false, -1),
                    new Split.CurrentMapSplit("nsc_mart2", "Pewter Shopping - when you exit the mart", false, -1),
                    new Split.HardResetSplit("nsc_btw", "Brock Through Walls Reset", false),
                    new Split.CurrentMapSplit("nsc_dungeon", "Enter Unknown Dungeon", false, -1),
                    new Split.HardResetSplit("nsc_manip2", "Ditto Manip Reset", false),
                    new Split.PartyCountSplit("nsc_ditto", "Get Ditto", false, 3),
                    new Split.PartyCountSplit("nsc_glitch1", "Get Rhydon", false, 4),
                    new Split.PartyCountSplit("nsc_glitch2", "Get F4", false, 5)
            }, new Split[] { // Reverse Badge Acquisition
                    new Split.EventFlagSplit("rba_rival", "Rival Fight", false, -1, -1),
                    new Split.EventFlagSplit("rba_parcel1", "Get Parcel", false, -1, -1),
                    new Split.EventFlagSplit("rba_parcel2", "Deliver Parcel", false, -1, -1),
                    new Split.ViridianMartSplit("rba_mart1", "Viridian Shopping - when you exit the mart (ignores the parcel visit)", false),
                    new Split.HardResetSplit("rba_manip1", "Pidgey Manip Reset", false),
                    new Split.PartyCountSplit("rba_pidgey", "Get Pidgey", false, 2),
                    new Split.CurrentMapSplit("rba_enter_forest", "Enter Forest", false, -1),
                    new Split.EventFlagSplit("rba_weedle", "Defeat the Bug Catcher", false, -1, -1),
                    new Split.CurrentMapSplit("rba_exit_forest", "Exit Forest", false, -1),
                    new Split.CurrentMapSplit("rba_mart2", "Pewter Shopping - when you exit the mart", false, -1),
                    new Split.HardResetSplit("rba_btw", "Brock Through Walls Reset", false),
                    new Split.CurrentMapSplit("rba_dungeon", "Enter Unknown Dungeon", false, -1),
                    new Split.HardResetSplit("rba_manip2", "Ditto Manip Reset", false),
                    new Split.PartyCountSplit("rba_ditto", "Get Ditto", false, 3),
                    new Split.PartyCountSplit("rba_missingno1", "Missingno 1", false, 4),
                    new Split.PartyCountSplit("rba_missingno2", "Missingno 2", false, 5),
                    new Split.PartyCountSplit("rba_mewtwo", "Mewtwo", false, 6),
                    new Split.EventFlagSplit("rba_giovanni", "Giovanni", false, -1, -1),
                    new Split.EventFlagSplit("rba_blaine", "Blaine", false, -1, -1),
                    new Split.EventFlagSplit("rba_sabrina", "Sabrina", false, -1, -1),
                    new Split.EventFlagSplit("rba_koga", "Koga", false, -1, -1),
                    new Split.EventFlagSplit("rba_erika", "Erika", false, -1, -1),
                    new Split.EventFlagSplit("rba_surge", "Lt. Surge", false, -1, -1),
                    new Split.EventFlagSplit("rba_misty", "Misty", false, -1, -1),
                    new Split.EventFlagSplit("rba_brock", "Brock", false, -1, -1),
                }
        };

        public PokemonRBSettings() {
            InitializeComponent();
            this.comboBox1.SelectedIndex = category = 0;
            this.lblVersion.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

            AutoStartTimer = false;
            AutoReset = false;
        }

        public XmlNode GetSettings(XmlDocument document) {
            var settingsNode = document.CreateElement("Settings");

            settingsNode.AppendChild(ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(ToElement(document, "AutoStartTimer", AutoStartTimer.ToString()));
            settingsNode.AppendChild(ToElement(document, "AutoReset", AutoReset.ToString()));
            settingsNode.AppendChild(ToElement(document, "Category", category.ToString()));

            foreach (Split[] category in SplitsByCategory) {
                foreach (Split split in category) {
                    settingsNode.AppendChild(ToElement(document, split.splitID, split.enabled.ToString()));
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

                if (element["AutoStartTimer"] != null) {
                    AutoStartTimer = Convert.ToBoolean(element["AutoStartTimer"].InnerText);
                    chkStartTimer.Checked = AutoStartTimer;
                }

                if (element["AutoReset"] != null) {
                    AutoReset = Convert.ToBoolean(element["AutoReset"].InnerText);
                    chkAutoReset.Checked = AutoReset;
                }

                foreach (Split[] category in SplitsByCategory) {
                    foreach (Split split in category) {
                        if (element[split.splitID] != null) {
                            split.enabled = Convert.ToBoolean(element[split.splitID].InnerText);
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

        private void checkStartTimer_CheckedChanged(object sender, EventArgs e) {
            AutoStartTimer = chkStartTimer.Checked;
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
