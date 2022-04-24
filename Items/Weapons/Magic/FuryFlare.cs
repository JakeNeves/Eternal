using Eternal.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Magic
{
    public class FuryFlare : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("fires flare rain swords that fall from above");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.magic = true;
            item.mana = 20;
            item.width = 68;
            item.height = 68;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = Item.sellPrice(gold: 12);
            item.rare = ItemRarityID.Pink;
            item.autoReuse = true;
            item.shootSpeed = 0f;
            item.shoot = ModContent.ProjectileType<FlareRain>();
            item.UseSound = SoundID.Item8;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(12, 60);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Projectile.NewProjectile(position.X + Main.rand.Next(-850, 850), position.Y - 650, 0, speedY, type, damage, knockBack, player.whoAmI);
            }
            return false;

        }

    }
}
