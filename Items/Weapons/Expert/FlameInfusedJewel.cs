using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Expert
{
    public class FlameInfusedJewel : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Multiple Fireballs\n'I swear, how can you cast spells from a jewel?'");
        }

		public override void SetDefaults()
		{
			item.damage = 100;
			item.magic = true;
			item.mana = 4;
			item.knockBack = 2f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shoot = ProjectileID.BallofFire;
            item.shootSpeed = 2f;
			item.width = 16;
			item.height = 30;
			item.UseSound = SoundID.Item8;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = ItemRarityID.Expert;
			item.expert = true;
			item.noMelee = true;
			item.value = Item.sellPrice(gold: 5);
            item.autoReuse = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(2, 4);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.InfernoFriendlyBolt, damage, knockBack, player.whoAmI);
                Main.PlaySound(SoundID.Item8, Main.myPlayer);
            }
            int spread = 2;
            float spreadMult = 2.2f;
            for (int i = 0; i < 3; i++)
            {
                float vX = speedX + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = speedY + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                Projectile.NewProjectile(position.X, position.Y, vX, vY, type, damage, knockBack, Main.myPlayer);
                Main.PlaySound(SoundID.Item8, Main.myPlayer);
            }
            return false;
        }

    }
}
