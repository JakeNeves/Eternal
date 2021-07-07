using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class StormBeholder : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires One to Two Arrows" + 
                "\n'Feel the thunder!'");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 64;
            item.damage = 26;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 15.5f;
            item.shoot = AmmoID.Arrow;
            item.useAmmo = AmmoID.Arrow;
            item.rare = ItemRarityID.Green;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 60);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(1, 2);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
