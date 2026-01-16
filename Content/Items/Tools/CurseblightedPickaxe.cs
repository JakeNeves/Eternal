using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Tools
{
    public class CurseblightedPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Melee;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 12;
            Item.useAnimation = 18;
            Item.pick = 190;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.tileBoost = 4;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HexititeBar>(), 10)
                .AddIngredient(ModContent.ItemType<OcculticMatter>(), 20)
                .AddIngredient(ModContent.ItemType<PsyblightEssence>(), 6)
                .AddIngredient(ModContent.ItemType<CursedAshes>(), 12)
                .AddIngredient(ModContent.ItemType<PsychicAshes>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
