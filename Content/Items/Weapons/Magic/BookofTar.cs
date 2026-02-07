using Eternal.Content.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class BookofTar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 6;
            Item.knockBack = 6f;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.shoot = ModContent.ProjectileType<Tarball>();
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item111;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.noMelee = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int spread = 15;
            float spreadMult = 0.05f;

            for (int i = 0; i < Main.rand.Next(4, 6); i++)
            {
                float vX = velocity.X + Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = velocity.Y + Main.rand.Next(-spread, spread + 1) * spreadMult;

                Projectile.NewProjectile(source, position, new Vector2(vX, vY), type, damage, knockback);
            }

            return true;
        }
    }
}
