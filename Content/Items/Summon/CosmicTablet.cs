using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class CosmicTablet : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 30;
            Item.rare = ModContent.RarityType<Ultramarine>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CoreofExodus>(), 12)
                .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>(), 16)
                .AddIngredient(ModContent.ItemType<ProviditeBar>(), 4)
                .AddIngredient(ModContent.ItemType<MindCrystalCluster>(), 4)
                .AddIngredient(ModContent.ItemType<BodyCrystalCluster>(), 4)
                .AddIngredient(ModContent.ItemType<SoulCrystalCluster>(), 4)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
        }
    }
}
