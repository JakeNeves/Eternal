using Eternal.Content.Projectiles.Weapons.Ranged;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Ammo
{
    public class EndlessArcaneShellBundle : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 10;
            Item.height = 22;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ModContent.ProjectileType<ArcaneShellProjectile>();
            Item.shootSpeed = 18f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ModContent.ItemType<ArcaneShell>(), 3996)
                .Register();
        }
    }
}
