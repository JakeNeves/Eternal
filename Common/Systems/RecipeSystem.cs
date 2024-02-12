using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    public class RecipeSystem : ModSystem
    {
        public static RecipeGroup adamantiteForges;
        public static RecipeGroup mythrilAnvils;
        public static RecipeGroup copperBars;
        public static RecipeGroup celestialFragments;

        public override void Unload()
        {
            adamantiteForges = null;
            mythrilAnvils = null;
            copperBars = null;
            celestialFragments = null;
        }

        public override void AddRecipeGroups()
        {
            adamantiteForges = new RecipeGroup(() => "Any Adamantite Forge", ItemID.AdamantiteForge, ItemID.TitaniumForge);
            mythrilAnvils = new RecipeGroup(() => "Any Mythril Anvil", ItemID.MythrilAnvil, ItemID.OrichalcumAnvil);
            copperBars = new RecipeGroup(() => "Any Copper Bar", ItemID.CopperBar, ItemID.TinBar);
            celestialFragments = new RecipeGroup(() => "Any Celestial Fragment", ItemID.FragmentNebula, ItemID.FragmentVortex, ItemID.FragmentSolar, ItemID.FragmentStardust);

            RecipeGroup.RegisterGroup("eternal:adamantiteForges", adamantiteForges);
            RecipeGroup.RegisterGroup("eternal:mythrilAnvils", mythrilAnvils);
            RecipeGroup.RegisterGroup("eternal:copperBars", copperBars);
            RecipeGroup.RegisterGroup("eternal:celestialFragments", celestialFragments);
        }
    }
}
