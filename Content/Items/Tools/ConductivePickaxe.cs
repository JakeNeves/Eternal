using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Tools
{
    public class ConductivePickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            Tooltip.SetDefault("'The pickaxe of thunder and lightning'");
        }

        public override void SetDefaults()
        {
            Item.tileBoost = 12;
            Item.damage = 430;
            Item.DamageType = DamageClass.Melee;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.pick = 235;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(gold: 10, silver: 78);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ElectriteBar>(), 12)
                .AddIngredient(ModContent.ItemType<MachalitePickaxe>())
                .AddIngredient(ModContent.ItemType<MachaliteSheets>(), 10)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
