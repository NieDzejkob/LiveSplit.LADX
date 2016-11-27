using LiveSplit.ComponentUtil;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace LiveSplit.PokemonRB {
    class PokemonRBMemory {
        public Emulator emulator { get; set; }

        private PokemonRBData data;
        private List<Split> splits;
        private bool saw;

        public PokemonRBMemory() {

        }
        
        public void setPointers() {
            data = new PokemonRBData(emulator);
        }

        public void setSplits(PokemonRBSettings settings) {
            splits = new List<Split>();
            splits.AddRange(settings.SplitsByCategory[settings.category]);

            foreach(Split split in splits) {
                split.reset();
            }
        }

        public bool doStart(Process game) {
            data.UpdateAll(game);
            saw = false;

            ushort wPlayerID = Convert.ToUInt16(data["wPlayerID"].Current);
            byte topLeftTile = Convert.ToByte(data["wTileMap"].Current);
            byte wCurrentMenuItem = Convert.ToByte(data["wCurrentMenuItem"].Current);
            byte hJoy5 = Convert.ToByte(data["hJoy5"].Current);
            ushort stack = Convert.ToUInt16(data["newGameStack"].Current);

            if (stack != 0x5B91) return false; // credits to lucky for that one
            if (wPlayerID != 0) return false; // in game or savefile exists
            if (topLeftTile != 0x79) return false; // not in the menu we want
            if ((wCurrentMenuItem == 0) && ((hJoy5 & 0x09) != 0)) return true;

            return false;
        }

        public bool doReset(Process game) {
            data.UpdateAll(game);
            uint dma = Convert.ToUInt32(data["DMA"].Current);
            uint reset = Convert.ToUInt32(data["Reset"].Current);
            return dma != 0x46E0C33E && reset == 0xFFFFFFFF;
        }

        public bool doSplit(Process game) {
            data.UpdateAll(game);

            if (splits.Count == 0) {
                // detect HoF fade here
                byte wHoFMonOrPlayer = Convert.ToByte(data["wHoFMonOrPlayer"].Current);
                byte rBGP = Convert.ToByte(data["rBGP"].Current);
                if (wHoFMonOrPlayer == 1 && rBGP != 0) saw = true;
                if (wHoFMonOrPlayer == 1 && rBGP == 0) return saw;
            }

            Split split = splits[0];
            if (split.shouldSplit(data)) {
                splits.RemoveAt(0);
                if (split.enabled) return true;
            }

            return false;
        }
    }

    public class PokemonRBData : MemoryWatcherList {
        private int ptrBase;
        private List<int>[] ptrOffsets;

        public PokemonRBData(Emulator emulator) {
            switch (emulator) {
                case Emulator.bgb152:
                    ptrBase = 0x127284;
                    ptrOffsets = new List<int>[] { new List<int> { 0x204 }, new List<int> { 0x22C }, new List<int> { 0x88, 0x7C, 0x34 }, new List<int> { 0xF4 } }; // WRAM, HRAM, SRAM, rBGP
                    break;
            }

            foreach (var _ptr in DefaultPointers.Pointers) {
                if (_ptr.Type == "byte")
                    this.Add(new MemoryWatcher<byte>(new DeepPointer(ptrBase, getOffsets(_ptr.Index, _ptr.Offset))) { Name = _ptr.Name });
                else if (_ptr.Type == "short")
                    this.Add(new MemoryWatcher<ushort>(new DeepPointer(ptrBase, getOffsets(_ptr.Index, _ptr.Offset))) { Name = _ptr.Name });
                else if (_ptr.Type == "int")
                    this.Add(new MemoryWatcher<uint>(new DeepPointer(ptrBase, getOffsets(_ptr.Index, _ptr.Offset))) { Name = _ptr.Name });
            }
        }

        private int[] getOffsets(int index, int offset) {
            var list = new List<int>();
            list.AddRange(ptrOffsets[index]);
            list.Add(offset);

            return list.ToArray();
        }
    }

    public enum Emulator {
        unknown,
        bgb152
    }
}
