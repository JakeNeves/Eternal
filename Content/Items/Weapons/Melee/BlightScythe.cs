using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class BlightScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 72;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Yellow;
            Item.damage = 90;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<BlightScytheProjectile>();
            Item.shootSpeed = 6f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulofBlight>(), 12)
                .AddIngredient(ItemID.SoulofNight, 12)
                .AddIngredient(ModContent.ItemType<EssenceofBlight>(), 6)
                .AddIngredient(ModContent.ItemType<EssenceofNight>(), 6)
                .AddIngredient(ModContent.ItemType<OcculticMatter>(), 16)
                .AddIngredient(ModContent.ItemType<CursedAshes>(), 12)
                .AddIngredient(ModContent.ItemType<SterlingSilverBar>(), 16)
                .AddIngredient(ItemID.IronBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulofBlight>(), 12)
                .AddIngredient(ItemID.SoulofNight, 12)
                .AddIngredient(ModContent.ItemType<EssenceofBlight>(), 6)
                .AddIngredient(ModContent.ItemType<EssenceofNight>(), 6)
                .AddIngredient(ModContent.ItemType<OcculticMatter>(), 16)
                .AddIngredient(ModContent.ItemType<CursedAshes>(), 12)
                .AddIngredient(ModContent.ItemType<SterlingSilverBar>(), 16)
                .AddIngredient(ItemID.LeadBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
