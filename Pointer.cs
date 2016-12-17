using System.Collections.Generic;
using System.Linq;

namespace LiveSplit.PokemonRB {
    public static class DefaultPointers {
        public static PointerList Pointers = new PointerList {
            // WRAM pointers
            new Pointer("wTileMap", "byte", 0, 0x03A0),
            new Pointer("wCurrentMenuItem", "byte", 0, 0x0C26),
            new Pointer("wHoFMonOrPlayer", "byte", 0, 0x0D40),
            new Pointer("wPartyCount", "byte", 0, 0x1163),
            new Pointer("wObtainedBadges", "byte", 0, 0x1356),
            new Pointer("wPlayerID", "short", 0, 0x1359),
            new Pointer("wCurMap", "byte", 0, 0x135E),
            new Pointer("newGameStack", "short", 0, 0x1FFD),

            // event pointers, maybe there's a better way
            new Pointer("eventD74B", "byte", 0, 0x174B),
            new Pointer("eventD74E", "byte", 0, 0x174E),
            new Pointer("eventD764", "byte", 0, 0x1764),
            new Pointer("eventD76C", "byte", 0, 0x176C),
            new Pointer("eventD78E", "byte", 0, 0x178E),
            new Pointer("eventD7E0", "byte", 0, 0x17E0),
            new Pointer("eventD7EF", "byte", 0, 0x17EF),
            new Pointer("eventD7F3", "byte", 0, 0x17F3),
            new Pointer("eventD803", "byte", 0, 0x1803),
            new Pointer("eventD838", "byte", 0, 0x1838),
            new Pointer("eventD857", "byte", 0, 0x1857),
            new Pointer("eventD863", "byte", 0, 0x1863),
            new Pointer("eventD864", "byte", 0, 0x1864),
            new Pointer("eventD865", "byte", 0, 0x1865),
            new Pointer("eventD866", "byte", 0, 0x1866),
            new Pointer("eventD867", "byte", 0, 0x1867),

            // HRAM pointers
            new Pointer("DMA", "int", 1, 0x00),
            new Pointer("hJoy5", "byte", 1, 0x35),

            new Pointer("Reset", "int", 2, 0x2598),

            new Pointer("rBGP", "byte", 3, 0x18)
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
