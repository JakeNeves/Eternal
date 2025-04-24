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
        public static RecipeGroup ironBars;
        public static RecipeGroup celestialFragments;
        public static RecipeGroup rottenChunks;

        public override void Unload()
        {
            adamantiteForges = null;
            mythrilAnvils = null;
            copperBars = null;
            ironBars = null;
            celestialFragments = null;
            rottenChunks = null;
        }

        public override void AddRecipeGroups()
        {
            adamantiteForges = new RecipeGroup(() => "Any Adamantite Forge", ItemID.AdamantiteForge, ItemID.TitaniumForge);
            mythrilAnvils = new RecipeGroup(() => "Any Mythril Anvil", ItemID.MythrilAnvil, ItemID.OrichalcumAnvil);
            copperBars = new RecipeGroup(() => "Any Copper Bar", ItemID.CopperBar, ItemID.TinBar);
            ironBars = new RecipeGroup(() => "Any Iron Bar", ItemID.IronBar, ItemID.LeadBar);
            celestialFragments = new RecipeGroup(() => "Any Celestial Fragment", ItemID.FragmentNebula, ItemID.FragmentVortex, ItemID.FragmentSolar, ItemID.FragmentStardust);
            rottenChunks = new RecipeGroup(() => "Any Rotten Chunk", ItemID.RottenChunk, ItemID.Vertebrae);

            RecipeGroup.RegisterGroup("eternal:adamantiteForges", adamantiteForges);
            RecipeGroup.RegisterGroup("eternal:mythrilAnvils", mythrilAnvils);
            RecipeGroup.RegisterGroup("eternal:copperBars", copperBars);
            RecipeGroup.RegisterGroup("eternal:ironBars", ironBars);
            RecipeGroup.RegisterGroup("eternal:celestialFragments", celestialFragments);
            RecipeGroup.RegisterGroup("eternal:rottenChunks", rottenChunks);
        }
    }
}
