using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Ammo
{
    public class EndlessBasaltShellBundle : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Unlimited Ammo" +
                                                                            "\n'Hot bullets made of hot materials!'");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 32;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<BasaltShellProjectile>();
            Item.shootSpeed = 18f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ModContent.ItemType<BasaltShell>(), 3996)
                .Register();
        }
    }
}
