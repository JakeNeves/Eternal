using Eternal.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Magic
{
    public class Incinerator : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eruption");
            Tooltip.SetDefault("Fires flaming swords from below" +
                              "\n'Hot Objects Included'");
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.magic = true;
            item.mana = 20;
            item.width = 32;
            item.height = 30;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.value = Item.sellPrice(silver: 9);
            item.rare = ItemRarityID.Pink;
            item.autoReuse = true;
            item.shootSpeed = 20f;
            item.shoot = ModContent.ProjectileType<FlameSword>();
            item.UseSound = SoundID.DD2_BetsyFireballShot; //SoundID.NPCDeath52;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(6, 36);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Projectile.NewProjectile(position.X + Main.rand.Next(-850, 850), position.Y + 600, 0, speedY, type, damage, knockBack, player.whoAmI);
            }
            return false;

        }

    }
}
