using Eternal.Common.Players;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Arkspear : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("<right> to throw" +
                             "\n'This little ark was unfortunatley turned into a spear...'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 600;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 24;
            Item.shootSpeed = 4.5f;
            Item.knockBack = 6f;
            Item.width = 70;
            Item.height = 70;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(gold: 60);

            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<ArkspearProjectile>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 18;
                Item.useAnimation = 18;
                Item.shoot = ModContent.ProjectileType<ArkspearProjectileThrown>();
                Item.shootSpeed = 16f;
                Item.useStyle = ItemUseStyleID.Swing;
            }
            else
            {
                Item.useTime = 24;
                Item.useAnimation = 18;
                Item.shoot = ModContent.ProjectileType<ArkspearProjectile>();
                Item.shootSpeed = 4.5f;
                Item.useStyle = ItemUseStyleID.Shoot;

                return player.ownedProjectileCounts[Item.shoot] < 1;
            }

            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position += Vector2.Normalize(velocity) * 15f;

            if (player.altFunctionUse == 2)
            {
                SoundEngine.PlaySound(SoundID.Item71, player.position);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            }
            return true;
        }
    }
}
