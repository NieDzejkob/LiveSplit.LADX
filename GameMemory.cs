using LiveSplit.ComponentUtil;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace LiveSplit.PokemonRB
{
    class PokemonRBMemory
    {
        public Emulator emulator { get; set; }

        private PokemonRBData data;

        public PokemonRBMemory() {

        }
        
        public void setPointers() {
            data = new PokemonRBData(emulator);
        }

        public void setSplits(PokemonRBSettings settings) {
            /*splits = new InfoList();
            splits.AddRange(DefaultInfo.BaseSplits);

            foreach (var _setting in settings.CheckedSplits)
            {
                if (!_setting.isEnabled)
                    splits.Remove(splits[_setting.Name]);
            }*/
        }

        public bool doStart(Process game) {
            data.UpdateAll(game);

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
            /*data["ResetCheck"].Update(game);

            byte _byte = Convert.ToByte(data["ResetCheck"].Current);
            if (_byte > 0)
                return true;*/

            return false;
        }

        public bool doSplit(Process game) {
            data.UpdateAll(game);

            /*foreach (var _split in splits)
            {
                int count = 0;
                foreach (var _trigger in _split.Triggers)
                {
                    int _int = Convert.ToInt32(data[_trigger.Pointer].Current);
                    if (_trigger.Operator == ">=")
                    {
                        if (_int >= _trigger.Value)
                            count++;
                    }
                    else
                    {
                        if (_int == _trigger.Value)
                            count++;
                    }
                }

                if (count == _split.Triggers.Count)
                {
                    splits.Remove(_split);
                    return true;
                }
            }*/

            return false;
        }
    }

    class PokemonRBData : MemoryWatcherList
    {
        private int ptrBase;
        private List<int>[] ptrOffsets;

        public PokemonRBData(Emulator emulator)
        {
            switch (emulator)
            {
                case Emulator.bgb152:
                    ptrBase = 0x127284;
                    ptrOffsets = new List<int>[] { new List<int> { 0x204 }, new List<int> { 0x22C } };
                    break;
            }

            foreach (var _ptr in DefaultPointers.Pointers)
            {
                if (_ptr.Type == "byte")
                    this.Add(new MemoryWatcher<byte>(new DeepPointer(ptrBase, getOffsets(_ptr.Index, _ptr.Offset))) { Name = _ptr.Name });
                else if (_ptr.Type == "short")
                    this.Add(new MemoryWatcher<short>(new DeepPointer(ptrBase, getOffsets(_ptr.Index, _ptr.Offset))) { Name = _ptr.Name });
                else if (_ptr.Type == "int")
                    this.Add(new MemoryWatcher<int>(new DeepPointer(ptrBase, getOffsets(_ptr.Index, _ptr.Offset))) { Name = _ptr.Name });
            }
        }

        private int[] getOffsets(int index, int offset)
        {
            var list = new List<int>();
            list.AddRange(ptrOffsets[index]);
            list.Add(offset);

            return list.ToArray();
        }
    }

    public enum Emulator
    {
        unknown,
        bgb152
    }
}
