using Eternal.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class VividMilkyWayClimax : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("<right> to use like a shortsword");
        }

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 58;
            item.damage = 220;
            item.knockBack = 30;
            item.value = Item.sellPrice(gold: 30);
            item.useTime = 12;
            item.useAnimation = 12;
            item.UseSound = SoundID.Item71;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.melee = true;
            item.shootSpeed = 6.75f;
            item.shoot = ModContent.ProjectileType<VividMilkyWayClimaxProjectile>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shootSpeed = 0f;
                item.shoot = ProjectileID.None;
                item.useStyle = ItemUseStyleID.Stabbing;
            }
            else
            {
                item.shootSpeed = 6.75f;
                item.shoot = ModContent.ProjectileType<VividMilkyWayClimaxProjectile>();
                item.useStyle = ItemUseStyleID.SwingThrow;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(1, 2);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
