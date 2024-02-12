using Eternal.Content.Projectiles.Weapons.Melee.Shortsword;
using Terraria.DataStructures;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Eternal.Content.Projectiles.Boss;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class CarminiteShortsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Orange;
            Item.damage = 24;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 2.4f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<CarminiteShortswordProjectile>();
            Item.shootSpeed = 2.1f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.position, velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            var projectile = Projectile.NewProjectileDirect(source, player.position, velocity, ModContent.ProjectileType<CarminiteTooth>(), damage, knockback, Main.myPlayer);

            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.DamageType = DamageClass.Melee;

            return true;
        }
    }
}
