using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class BoonOfTheArks : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 600;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<BOTAProjectile>();
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.autoReuse = true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Item.GetSource_None();

            for (int i = 0; i < 6; i++)
            {
                Projectile.NewProjectile(entitySource, target.Center, new Vector2(Main.rand.Next(-8, 8), Main.rand.Next(-8, 8)), ModContent.ProjectileType<BOTAProjectileAOE>(), Item.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }

            SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/BOTAHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(1f, 1.25f),
                MaxInstances = 0,
            }, target.position);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ProjectileID.None;
                Item.shootSpeed = 0f;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<BOTAProjectile>();
                Item.shootSpeed = 6f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var entitySource = Item.GetSource_None();

            int numberProjectiles = 1 + Main.rand.Next(3);

            for (int index = 0; index < numberProjectiles; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)(player.position.X + player.width * 0.5 + Main.rand.Next(201) * -player.direction + (Main.mouseX + (double)Main.screenPosition.X - player.position.X)), (float)(player.position.Y + player.height * 0.5 - 600.0));
                vector2_1.X = (float)((vector2_1.X + (double)player.Center.X) / 2.0) + Main.rand.Next(-200, 201);
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
                float num15 = Item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + Main.rand.Next(-30, 31) * 0.02f;
                float SpeedY = num17 + Main.rand.Next(-15, 16) * 0.02f;
                int projectile = Projectile.NewProjectile(entitySource, vector2_1.X, vector2_1.Y, SpeedX, SpeedY, type, damage, knockback, Main.myPlayer, 0.0f, Main.rand.Next(5));
                Main.projectile[projectile].friendly = true;
                Main.projectile[projectile].hostile = false;
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkiumQuartzPlating>(), 48)
                .AddIngredient(ModContent.ItemType<ArkaniumAlloy>(), 30)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 4)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
