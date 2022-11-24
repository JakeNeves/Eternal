using Eternal.Common.Configurations;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    public class RecipeSystem : ModSystem
    {
        public static RecipeGroup adamantiteForges;
        public static RecipeGroup mythrilAnvils;
        public static RecipeGroup copperBars;

        public override void Unload()
        {
            adamantiteForges = null;
            mythrilAnvils = null;
            copperBars = null;
        }

        public override void AddRecipeGroups()
        {
            adamantiteForges = new RecipeGroup(() => "Any Adamantite Forge", ItemID.AdamantiteForge, ItemID.TitaniumForge);
            mythrilAnvils = new RecipeGroup(() => "Any Mythril Anvil", ItemID.MythrilAnvil, ItemID.OrichalcumAnvil);
            copperBars = new RecipeGroup(() => "Any Copper Bar", ItemID.CopperBar, ItemID.TinBar);

            RecipeGroup.RegisterGroup("eternal:adamantiteForges", adamantiteForges);
            RecipeGroup.RegisterGroup("eternal:mythrilAnvils", mythrilAnvils);
            RecipeGroup.RegisterGroup("eternal:copperBars", copperBars);
        }
    }
}
