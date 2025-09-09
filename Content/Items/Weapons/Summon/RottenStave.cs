using Eternal.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Summon
{
    public class RottenStave : ModItem
    {

        public override void SetStaticDefaults()
        {
			ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;
            Item.mana = 10;
            Item.width = 38;
            Item.height = 32;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(gold: 30);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item83;
            Item.shoot = ModContent.ProjectileType<RottenMindSentry>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool canPlaceInAir = false;

            if (player.direction == 1)
            {
                canPlaceInAir = true;
            }

            position = Main.MouseWorld;
            player.LimitPointToPlayerReachableArea(ref position);
            int halfProjectileHeight = (int)Math.Ceiling(ContentSamples.ProjectilesByType[type].height / 2f);

            if (!canPlaceInAir)
            {
                player.FindSentryRestingSpot(type, out int worldX, out int worldY, out int pushYUp);
                position = new Vector2(worldX, worldY - halfProjectileHeight);
            }
            else
            {
                position.Y -= halfProjectileHeight;
            }

            Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, Main.myPlayer, ai2: canPlaceInAir ? 0 : 1);

            player.UpdateMaxTurrets();

            return false;
        }
    }
}
