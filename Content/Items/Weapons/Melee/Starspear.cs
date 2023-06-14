using Eternal.Common.Players;
using Eternal.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Starspear : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("<right> to throw and leave a trail of bombs" +
                             "\nMore Starspears spawn while wearing Starborn Armor or better" +
                             "\n'A Starsharp Spear'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 24;
            Item.shootSpeed = 4.5f;
            Item.knockBack = 4f;
            Item.width = 66;
            Item.height = 66;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 10);

            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<StarspearProjectile>();
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
                Item.shoot = ModContent.ProjectileType<StarspearProjectileThrown>();
                Item.shootSpeed = 16f;
                Item.useStyle = ItemUseStyleID.Swing;
            }
            else
            {
                Item.useTime = 24;
                Item.useAnimation = 18;
                Item.shoot = ModContent.ProjectileType<StarspearProjectile>();
                Item.shootSpeed = 4.5f;
                Item.useStyle = ItemUseStyleID.Shoot;

                return player.ownedProjectileCounts[Item.shoot] < 1;
            }

            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2 + Main.rand.Next(2);

            position += Vector2.Normalize(velocity) * 15f;

            if (ArmorSystem.StarbornArmor)
            {
                if (player.altFunctionUse == 2)
                {
                    SoundEngine.PlaySound(SoundID.Item71, player.position);

                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Projectile.NewProjectile(source, position.X + Main.rand.NextFloat(-150, 150), position.Y + Main.rand.NextFloat(-150, 150), velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                    }
                }
                else
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                }

            }
            return true;
        }
    }
}
