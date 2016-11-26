using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;

[assembly: ComponentFactory(typeof(LiveSplit.PokemonRB.PokemonRBFactory))]

namespace LiveSplit.PokemonRB {
    public class PokemonRBFactory : IComponentFactory {
        public string ComponentName => "Pokemon Red/Blue Auto Splitter";
        public string Description => "Autosplitter for Pokemon Red and Blue with BGB and Gambatte";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public ComponentCategory Category => ComponentCategory.Control;

        public string UpdateName => ComponentName;
        public string UpdateURL => "https://raw.githubusercontent.com/NieDzejkob/LiveSplit.PokemonRB/master/";
        public string XMLURL => UpdateURL + "Components/update.LiveSplit.PokemonRB.xml";

        public IComponent Create(LiveSplitState state) {
            return new PokemonRBComponent(state);
        }
    }
}
