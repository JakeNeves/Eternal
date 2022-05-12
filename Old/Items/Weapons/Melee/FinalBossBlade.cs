using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class FinalBossBlade : ModItem
    {
        int fbbAttackMode = 0;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("<right> to change attack modes" +
                             "\nMode 1 [c/35bce6:Penetrator] - Fires a penetration spike that pierces enemies" +
                             "\nMode 2 [c/35bce6:Homing Chakram Swarm] - Fires a swarm of chakrams that chase your enemies when nearby" +
                             "\nMode 3 [c/35bce6:Disk Rain] - Fires a disk that retreats to you like a boomerang that penetrates through enemies" +
                             "\n'How good can it be?'");
        }

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 56;
            item.damage = 10000;
            item.knockBack = 15;
            item.value = Item.buyPrice(platinum: 1, gold: 3);
            item.useTime = 20;
            item.useAnimation = 20;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            if (fbbAttackMode == 1)
                item.shoot = ModContent.ProjectileType<FinalBossBladeProjectile2>();
            else if (fbbAttackMode == 2)
                item.shoot = ModContent.ProjectileType<FinalBossBladeProjectile3>();
            else
                item.shoot = ModContent.ProjectileType<FinalBossBladeProjectile1>();
            item.melee = true;
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                fbbAttackMode += 1;

                if (fbbAttackMode >= 3)
                {
                    fbbAttackMode = 0;
                }

                item.UseSound = SoundID.DD2_MonkStaffSwing;
                item.shootSpeed = 0f;
                item.shoot = ProjectileID.None;
                item.autoReuse = false;
            }
            else
            {
                item.UseSound = SoundID.Item1;
                item.shootSpeed = 12f;
                item.shoot = ModContent.ProjectileType<FinalBossBladeProjectile1>();
                item.autoReuse = true;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkBlue;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            switch (fbbAttackMode)
            {
                case 1:
                    for (int j = 0; j < 6; j++)
                    {
                        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                        Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FinalBossBladeProjectile2>(), damage, knockBack, player.whoAmI);
                        Main.PlaySound(SoundID.DD2_DrakinShot, Main.myPlayer);
                    }
                    break;
                case 2:
                    for (int index = 0; index < 6; ++index)
                    {
                        Vector2 vector2_1 = new Vector2((float)(player.position.X + player.width * 0.5 + Main.rand.Next(200) * -player.direction + (Main.mouseX + (double)Main.screenPosition.X - player.position.X)), (float)(player.position.Y + player.height * 0.5 - 600.0));
                        vector2_1.X = (float)((vector2_1.X + (double)player.Center.X) / 2.0) + Main.rand.Next(-150, 200);
                        vector2_1.Y -= 100 * index;
                        float num12 = Main.mouseX + Main.screenPosition.X - vector2_1.X;
                        float num13 = Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                        if (num13 < 0.0)
                        {
                            num13 *= -1f;
                        }

                        if (num13 < 20.0)
                        {
                            num13 = 20f;
                        }

                        float num14 = (float)Math.Sqrt(num12 * (double)num12 + num13 * (double)num13);
                        float num15 = item.shootSpeed / num14;
                        float num16 = num12 * num15;
                        float num17 = num13 * num15;
                        float SpeedX = num16 + Main.rand.Next(-4, 6) * 0.02f;
                        float SpeedY = num17 + Main.rand.Next(-4, 6) * 0.02f;
                        Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, ModContent.ProjectileType<FinalBossBladeProjectile3>(), damage, knockBack, Main.myPlayer, 0.0f, Main.rand.Next(5));
                    }
                    Main.PlaySound(SoundID.DD2_PhantomPhoenixShot, Main.myPlayer);
                    break;
                default:
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
                    Main.PlaySound(SoundID.DD2_SkyDragonsFurySwing, Main.myPlayer);
                    break;
            }

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<StellarAlloy>(), 12);
            recipe.AddIngredient(ModContent.ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ModContent.ItemType<PrismaticFractal>());
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 24);
            recipe.AddIngredient(ModContent.ItemType<CometiteCrystal>(), 24);
            recipe.AddIngredient(ModContent.ItemType<CoreofEternal>(), 16);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
