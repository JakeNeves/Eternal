using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Tools
{
    public class ArkaniumPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.tileBoost = 5;
            Item.damage = 400;
            Item.DamageType = DamageClass.Melee;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.pick = 240;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 4)
                .AddIngredient(ModContent.ItemType<ArkaniumAlloy>(), 20)
                .AddIngredient(ModContent.ItemType<GalaciteBar>(), 30)
                .AddIngredient(ModContent.ItemType<ElectriteBar>(), 30)
                .AddIngredient(ModContent.ItemType<IgniumBar>(), 30)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
