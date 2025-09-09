using Eternal.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class RottenFangspear : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 96;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 36;
            Item.useTime = 48;
            Item.shootSpeed = 4.5f;
            Item.knockBack = 2f;
            Item.width = 46;
            Item.height = 54;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 15);

            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<RottenFangspearProjectile>();
        }

        public override bool MeleePrefix() => true;

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
                Item.shoot = ModContent.ProjectileType<RottenFangspearProjectileThrown>();
                Item.shootSpeed = 16f;
                Item.useStyle = ItemUseStyleID.Swing;
            }
            else
            {
                Item.useTime = 24;
                Item.useAnimation = 18;
                Item.shoot = ModContent.ProjectileType<RottenFangspearProjectile>();
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
