using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class CurseblightDisk : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Lime;
            Item.damage = 85;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<CurseblightDiskProjectile>();
            Item.shootSpeed = 8f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HexititeBar>(), 12)
                .AddIngredient(ModContent.ItemType<OcculticMatter>(), 24)
                .AddIngredient(ModContent.ItemType<PsyblightEssence>(), 24)
                .AddIngredient(ModContent.ItemType<CursedAshes>(), 16)
                .AddIngredient(ModContent.ItemType<PsychicAshes>(), 16)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
