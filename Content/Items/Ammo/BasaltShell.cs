using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Ammo
{
    public class BasaltShell : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Hot bullets made of hot materials!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 24;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<BasaltShellProjectile>();
            Item.shootSpeed = 18f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(333)
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ModContent.ItemType<MagmaticAlloy>())
                .Register();
        }
    }
}
