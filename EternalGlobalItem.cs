using Eternal.Dusts;
using Eternal.Items.Materials.Elementalblights;
using Eternal.Projectiles.Weapons.Expert;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
    public class EternalGlobalItem : GlobalItem
    {
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (EternalPlayer.AoIGift)
            {
                if (item.melee)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AoIProjectile>(), damage, knockBack, player.whoAmI);
                    Main.PlaySound(SoundID.DD2_DarkMageAttack, Main.myPlayer);
                }
            }

            return true;
        }

        public override void MeleeEffects(Item item, Player player, Rectangle hitbox)
        {
            if (EternalGlobalProjectile.emperorsGift)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<EmperorFire>());
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe rodRecipe = new ModRecipe(mod);
            rodRecipe.AddTile(TileID.MythrilAnvil);
            rodRecipe.AddIngredient(ItemID.HallowedBar, 6);
            rodRecipe.AddIngredient(ItemID.SoulofLight, 16);
            rodRecipe.AddIngredient(ItemID.PixieDust, 20);
            rodRecipe.AddIngredient(ItemID.CrystalShard, 24);
            rodRecipe.AddIngredient(ItemID.UnicornHorn, 2);
            rodRecipe.SetResult(ItemID.RodofDiscord);
            rodRecipe.AddRecipe();

            ModRecipe dirtRodRecipe = new ModRecipe(mod);
            dirtRodRecipe.AddTile(TileID.WorkBenches);
            dirtRodRecipe.AddIngredient(ModContent.ItemType<NatureblightCrystal>(), 6);
            dirtRodRecipe.AddIngredient(ItemID.DirtBlock, 300);
            dirtRodRecipe.AddIngredient(ItemID.Acorn, 20);
            dirtRodRecipe.AddIngredient(ItemID.Wood, 20);
            dirtRodRecipe.SetResult(ItemID.DirtRod);
            dirtRodRecipe.AddRecipe();
        }
    }
}
