using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.PokemonRB {
    abstract public class Split {
        public string splitID { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public abstract bool shouldSplit(PokemonRBData data);
        public virtual void reset() { }

        public Split(string _splitID, string _name) {
            splitID = _splitID;
            name = _name;
            enabled = false; ;
        }

        public class HardResetSplit : Split {
            bool sawDMA;
            public HardResetSplit(string _splitID, string _name) : base(_splitID, _name) {
                sawDMA = false;
            }

            public override bool shouldSplit(PokemonRBData data) {
                uint dma = Convert.ToUInt32(data["DMA"].Current);
                if (dma == 0x46E0C33E) sawDMA = true;
                else if (sawDMA) return true;

                byte hSoftReset = Convert.ToByte(data["hSoftReset"].Current);
                return hSoftReset != 16;
            }

            public override void reset() {
                sawDMA = false;
            }
        }

        public class PartyCountSplit : Split {
            int expectedCount;
            public PartyCountSplit(string _splitID, string _name, int _expectedCount) : base(_splitID, _name) {
                expectedCount = _expectedCount;
            }

            public override bool shouldSplit(PokemonRBData data) {
                byte count = Convert.ToByte(data["wPartyCount"].Current);
                return count == expectedCount;
            }
        }

        public class CurrentMapSplit : Split {
            int expectedMapID;
            public CurrentMapSplit(string _splitID, string _name, int _expectedMapID) : base(_splitID, _name) {
                expectedMapID = _expectedMapID;
            }

            public override bool shouldSplit(PokemonRBData data) {
                byte current = Convert.ToByte(data["wCurMap"].Current);
                return current == expectedMapID;
            }
        }

        public class ExitMapSplit : Split {
            int expectedMapID;
            bool sawTheMap;
            public ExitMapSplit(string _splitID, string _name, int _expectedMapID) : base(_splitID, _name) {
                expectedMapID = _expectedMapID;
                sawTheMap = false;
            }

            public override bool shouldSplit(PokemonRBData data) {
                byte current = Convert.ToByte(data["wCurMap"].Current);
                if (current == expectedMapID) sawTheMap = true;
                else if (sawTheMap) return true;
                return false;
            }

            public override void reset() {
                sawTheMap = false;
            }
        }

        public class EventFlagSplit : Split {
            string flagAddress;
            int bitNumber;
            public EventFlagSplit(string _splitID, string _name, string _flagAddress, int _bitNumber) : base(_splitID, _name) {
                flagAddress = _flagAddress;
                bitNumber = _bitNumber;
            }

            public override bool shouldSplit(PokemonRBData data) {
                if (bitNumber == -1) return false; // temp
                byte _byte = Convert.ToByte(data[flagAddress].Current);
                if ((_byte & (1 << bitNumber)) != 0) return true;
                return false;
            }
        }
    }
}
