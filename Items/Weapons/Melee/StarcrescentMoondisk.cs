using Eternal.Projectiles.Weapons.Melee;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal.Items.Weapons.Melee
{
    public class StarcrescentMoondisk : ModItem
    {
        bool rightClick;

        public override void SetStaticDefaults() {
             Tooltip.SetDefault("<right> to throw Starcrescent Shards\n'Free the moonlight!'");
        }

        public override void SetDefaults()
        {
            item.damage = 20000;
            item.melee = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 12;
            item.useAnimation = 12;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 20f;
            item.value = Item.buyPrice(gold: 30, silver: 95);
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 8.2f;
            item.shoot = ProjectileType<StarcrescentMoondiskProjectile>();
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(5, 35, 215);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                rightClick = true;
                item.useTime = 8;
                item.useAnimation = 8;
                item.shoot = ProjectileType<StarcrescentProjectile>();
                item.shootSpeed = 4.4f;
            }
            else
            {
                rightClick = false;
                item.useTime = 12;
                item.useAnimation = 12;
                item.shoot = ProjectileType<StarcrescentMoondiskProjectile>();
                item.shootSpeed = 8.2f;
                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                    {
                        return false;
                    }
                }
            }
            return true;

        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (rightClick == true)
            {
                int numberProjectiles = Main.rand.Next(2, 4);
                for (int j = 0; j < numberProjectiles; j++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                /*int spread = 4;
                float spreadMult = 0.5f;
                for (int i = 0; i < 3; i++)
                {
                    float vX = speedX + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                    float vY = speedY + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                    Projectile.NewProjectile(position.X, position.Y, vX, vY, type, damage, knockBack, Main.myPlayer);
                }*/
                return false;
            }
            return true;
        }

    }
}
