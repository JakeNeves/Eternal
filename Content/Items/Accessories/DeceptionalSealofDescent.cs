using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace Eternal.Content.Items.Accessories
{
    public class DeceptionalSealofDescent : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("45% increased summon damage");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) *= 1.45f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientFoundry>())
                .AddIngredient(ItemID.SummonerEmblem)
                .AddIngredient(ModContent.ItemType<ShadeMatter>(), 16)
                .Register();
        }
    }
}
