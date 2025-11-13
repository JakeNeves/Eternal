using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Trinitalas : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 18;
            Item.height = 18;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 30f;
            Item.knockBack = 9f;
            Item.damage = 300;
            Item.rare = ModContent.RarityType<Azure>();

            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 10);
            Item.shoot = ModContent.ProjectileType<TrinitalasProjectile>();
        }

        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

        public override bool MeleePrefix() => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 2; i++)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<TrinitalasMindProjectile>(), damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<TrinitalasBodyProjectile>(), damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<TrinitalasSoulProjectile>(), damage, knockback, player.whoAmI);
            }

            return true;
        }
    }
}
