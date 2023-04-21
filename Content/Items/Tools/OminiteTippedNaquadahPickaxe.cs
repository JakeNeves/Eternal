using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Tools
{
    public class OminiteTippedNaquadahPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.tileBoost = 20;
            Item.damage = 450;
            Item.DamageType = DamageClass.Melee;
            Item.width = 54;
            Item.height = 60;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.pick = 260;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CrystalizedOminite>(), 12)
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 10)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
