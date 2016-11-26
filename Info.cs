using System.Collections.Generic;
using System.Linq;

namespace LiveSplit.PokemonRB {
    public static class DefaultPointers {
        public static PointerList Pointers = new PointerList {
            //WRAM pointers
            new Pointer("wCurrentMenuItem", "byte", 0, 0x0C26),
            new Pointer("wTileMap", "byte", 0, 0x03A0),
            new Pointer("wPlayerID", "short", 0, 0x1359),
            new Pointer("newGameStack", "short", 0, 0x1FFD),

            //HRAM pointers
            new Pointer("DMA", "int", 1, 0x00),
            new Pointer("hJoy5", "byte", 1, 0x35)
        };
    }

    public class Pointer {
        public string Name { get; }

        public string Type { get; }
        public int Index { get; }
        public int Offset { get; }

        public Pointer(string _name, string _type, int _index, int _offset) {
            Name = _name;
            Type = _type;
            Index = _index;
            Offset = _offset;
        }

    }

    public class PointerList : List<Pointer> {
        public Pointer this[string name] {
            get { return this.First(w => w.Name == name); }
        }
    }
}
