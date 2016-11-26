using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit {
    abstract class Split {
        public string splitID { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        abstract public bool shouldSplit();

        public Split(string _splitID, string _name, bool _enabled) {
            splitID = _splitID;
            name = _name;
            enabled = _enabled;
        }

        public class HardResetSplit : Split {
            bool sawDMA;
            public HardResetSplit(string _splitID, string _name, bool _enabled) : base(_splitID, _name, _enabled) {
                sawDMA = false;
            }

            public override bool shouldSplit() {
                return false;
            }
        }

        public class PartyCountSplit : Split {
            int expectedCount;
            public PartyCountSplit(string _splitID, string _name, bool _enabled, int _expectedCount) : base(_splitID, _name, _enabled) {
                expectedCount = _expectedCount;
            }

            public override bool shouldSplit() {
                return false;
            }
        }

        public class CurrentMapSplit : Split {
            int expectedMapID;
            public CurrentMapSplit(string _splitID, string _name, bool _enabled, int _expectedMapID) : base(_splitID, _name, _enabled) {
                expectedMapID = _expectedMapID;
            }

            public override bool shouldSplit() {
                return false;
            }
        }

        public class EventFlagSplit : Split {
            int flagAddress;
            int bitNumber;
            public EventFlagSplit(string _splitID, string _name, bool _enabled, int _flagAddress, int _bitNumber) : base(_splitID, _name, _enabled) {
                flagAddress = _flagAddress;
                bitNumber = _bitNumber;
            }

            public override bool shouldSplit() {
                return false;
            }
        }

        public class ViridianMartSplit : Split { // splits only if parcel is delivered
            public ViridianMartSplit(string _splitID, string _name, bool _enabled) : base(_splitID, _name, _enabled) { }

            public override bool shouldSplit() {
                return false;
            }
        }
    }
}
