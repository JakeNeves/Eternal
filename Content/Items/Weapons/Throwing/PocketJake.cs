using Eternal.Content.Items.Misc;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Throwing
{
    public class PocketJake : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws pocket Jakes that explode on impact in a comical way" +
                             "\nThrow more than one pocket Jake when the Emperor's Trust is in your inventory" +
                             "\n'Channel your inner Jake!'" +
                             "\n[c/FC036B:Developer Item]" +
                             "\nDedicated to [c/038CFC:JakeTEM]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.damage = 800;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<PocketJakeProjectile>();
            Item.shootSpeed = 18f;
            Item.value = Item.sellPrice(platinum: 7);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 4;

            position += Vector2.Normalize(velocity) * 15f;

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<EmperorsTrust>()))
            {
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Projectile.NewProjectile(source, position.X + Main.rand.NextFloat(-60, 60), position.Y + Main.rand.NextFloat(-60, 60), velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                }
            }
            else
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
