using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class ValtoricKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Lime;
            Item.damage = 50;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item39;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<ValtoricKnivesProjectile>();
            Item.shootSpeed = 12f;
        }

        public override bool MeleePrefix() => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(4);
            float rotation = MathHelper.ToRadians(15);

            position += Vector2.Normalize(velocity) * 15f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
