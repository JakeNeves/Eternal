using Eternal.Content.Projectiles.Weapons.Radiant;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Radiant
{
    public class PrimordialElementalBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 600;
            Item.DamageType = ModContent.GetInstance<DamageClasses.Radiant>();
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0f;
            Item.value = Item.sellPrice(platinum: 9);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.autoReuse = true;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<PrimordialElementalBreakerProjectile>();
            Item.UseSound = SoundID.Item8;
            Item.noUseGraphic = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }

            for (int i = 0; i < Main.rand.Next(6, 10); i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            for (int i = 0; i < 3 + Main.rand.Next(5); ++i)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-250, 250), Main.rand.Next(-250, 250)), velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }

            return false;
        }
    }
}
