using Eternal.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class VividMilkyWayClimax : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Where's the climax?'");
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 58;
            Item.damage = 230;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3.5f;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.shoot = ModContent.ProjectileType<VividMilkyWayClimaxProjectile>();
            Item.shootSpeed = 30f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 6 + Main.rand.Next(4);
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
