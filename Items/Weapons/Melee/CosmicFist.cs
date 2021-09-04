using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Projectiles.Weapons.Melee;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eternal.Items.Weapons.Melee
{
    public class CosmicFist : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Cosmic Fists That Follow your cursor" +
                "\nHold down your your Left Mouse Button for create an AoE Projectile at Your cursor" +
                "\n'Sebastion might have an idea that involves this...'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.damage = 930;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileType<CosmicFistProjectile>();
            item.rare = ItemRarityID.Red;
            item.useTime = 16;
            item.useAnimation = 16;
            item.shootSpeed = 8;
            item.autoReuse = true;
            item.channel = true;
            item.knockBack = 8.3f;
            item.UseSound = SoundID.Item1;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (EternalGlobalProjectile.cometGauntlet == true)
            {
                int numberProjectiles = Main.rand.Next(1, 2);
                for (int j = 0; j < numberProjectiles; j++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                return false;
            }
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }
    }
}
