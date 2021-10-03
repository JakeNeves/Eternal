using System;
using Terraria.ModLoader;

namespace Eternal.Integration
{
    public abstract class ModIntegration
    {
        protected ModIntegration(Mod callerMod, string modName)
        {
            CallerMod = callerMod;

            ModName = modName;
        }

        public virtual ModIntegration TryLoad() => (ModInstance = ModLoader.GetMod(ModName)) == null ? null : this;

        public Mod CallerMod { get; }
        public string ModName { get; }

        public Mod ModInstance { get; private set; }
    }
}
