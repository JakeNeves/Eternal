using Eternal.Content.Projectiles.Weapons.Magic;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class SwordsofTheSwordGod : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Swords of The Sword God");
            /* Tooltip.SetDefault("Fires a barrage of swords" +
                             "\n'How about we do a little sword dance!'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.damage = 600;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 10;
            Item.knockBack = 5;
            Item.useAnimation = 6;
            Item.useTime = 6;
            Item.shoot = ModContent.ProjectileType<SwordGodProjectile>();
            Item.shootSpeed = 12f;
            Item.UseSound = SoundID.Item71;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3 + Main.rand.Next(3); ++i)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-128, 128), Main.rand.Next(-128, 128)), velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }

            return false;
        }
    }
}
