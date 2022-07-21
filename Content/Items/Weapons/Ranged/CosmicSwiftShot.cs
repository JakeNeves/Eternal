using Eternal.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class CosmicSwiftShot : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Converts arrows to Swift Shot Starbusters\n'The Triple-Shot Bow of The Cosmos'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 48;
            Item.damage = 220;
            Item.knockBack = 2.6f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ItemRarityID.Red;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(30);

            position += Vector2.Normalize(velocity) * 15f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<SwiftShotStarbuster>(), damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
