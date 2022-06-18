using Eternal.Content.Projectiles.Weapons.Whips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Summon
{
    public class ExoSpinesnapper : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 24;
            Item.damage = 130;
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.useAnimation = 36;
            Item.useTime = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<ExoSpinesnapperProjectile>();
            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item152;
            Item.autoReuse = false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(30);

            position += Vector2.Normalize(velocity) * 15f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
