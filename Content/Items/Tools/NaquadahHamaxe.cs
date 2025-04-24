using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Tools
{
    public class NaquadahHamaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.tileBoost = 16;
            Item.damage = 400;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 30;
            Item.axe = 50;
            Item.hammer = 100;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 12;
            Item.value = Item.buyPrice(gold: 60);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>(), 16)
                .AddIngredient(ModContent.ItemType<OminaquaditeBar>(), 20)
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 14)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
