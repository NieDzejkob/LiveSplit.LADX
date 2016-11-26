using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using LiveSplit.ComponentUtil;

namespace LiveSplit.PokemonRB {
    class PokemonRBComponent : LogicComponent {
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public override string ComponentName => "Pokemon RB Auto Splitter";

        public PokemonRBSettings settings { get; set; }

        private Process game { get; set; }
        private TimerModel model { get; set; }
        private PokemonRBMemory memory { get; set; }

        private Timer processTimer;

        public PokemonRBComponent(LiveSplitState state) {
            settings = new PokemonRBSettings();

            model = new TimerModel() { CurrentState = state };

            processTimer = new Timer() { Interval = 2000, Enabled = true };
            processTimer.Tick += processTimer_OnTick;

            memory = new PokemonRBMemory();
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) {
            if (game != null && !game.HasExited) {
                if (state.CurrentPhase == TimerPhase.NotRunning) {
                    if (settings.AutoStartTimer) {
                        if (memory.doStart(game)) {
                            model.Start();
                        }
                    }
                } else {
                    if (settings.AutoReset) {
                        if (memory.doReset(game)) {
                            model.Reset();
                        }
                    }
                }

                if (state.CurrentPhase == TimerPhase.Running) {
                    if (memory.doSplit(game)) {
                        model.Split();
                    }
                }
            }
            else if (!processTimer.Enabled) {
                processTimer.Enabled = true;
            }
        }

        Process getGameProcess() {
            Process process = null;

            foreach (Process p in Process.GetProcesses()) {
                try {
                    if (p.MainModuleWow64Safe().ModuleMemorySize == 1699840) {
                        process = p;
                        break;
                    }
                }
                catch { }
            }

            if (process != null) {
                switch (process.MainModuleWow64Safe().ModuleMemorySize) {
                    case 1699840:
                        memory.emulator = Emulator.bgb152;
                        break;
                }

                memory.setPointers();
                return process;
            }

            return null;
        }

        void processTimer_OnTick(object sender, EventArgs e) {
            if (game == null || game.HasExited) {
                game = getGameProcess();
            } else {
                processTimer.Enabled = false;
            }
        }

        public override void Dispose() {
            processTimer.Tick -= processTimer_OnTick;
        }

        public override System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document) {
            return settings.GetSettings(document);
        }

        public override System.Windows.Forms.Control GetSettingsControl(UI.LayoutMode mode) {
            return settings;
        }

        public override void SetSettings(System.Xml.XmlNode settings) {
            this.settings.SetSettings(settings);
        }
    }
}
