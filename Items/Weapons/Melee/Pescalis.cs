using Eternal.Items.Accessories;
using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class Pescalis : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("True melee attacks heal you upon striking an enemy" +
                             "\nFires swords from all four directions when wearing the Ultimus armor with the mask" +
                             "\n[c/008060:Ancient Artifact]" +
                             "\nThis blade was once weilded by the cosmic emperor, until he met his fate with doom, something had killed the emperor in one attack!" +
                             "\nHowever, the emperor's soul quickly reincarnate with his brittle body and the emperor eventually found a way to cheat death, but his power to do so remains unknown...");
        }

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 54;
            item.damage = 2100;
            item.knockBack = 6f;
            item.value = Item.buyPrice(platinum: 1, gold: 3);
            item.useTime = 15;
            item.useAnimation = 15;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.shoot = ModContent.ProjectileType<PescalisProjectile>();
            item.shootSpeed = 16f;
            item.autoReuse = true;
            item.melee = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.DD2_DarkMageCastHeal, player.position);

            if (EternalPlayer.UltimusArmorMeleeBonus)
            {
                for (int i = 0; i < 4; ++i)
                {
                    Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, speedY, ModContent.ProjectileType<PescalisProjectile>(), damage, knockBack, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, 0, speedY, ModContent.ProjectileType<PescalisProjectile>(), damage, knockBack, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), speedX, 0, ModContent.ProjectileType<PescalisProjectile>(), damage, knockBack, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), speedX, 0, ModContent.ProjectileType<PescalisProjectile>(), damage, knockBack, Main.myPlayer, 0f, 0f);
                }
            }

            return true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Main.PlaySound(SoundID.DD2_LightningAuraZap, player.position);
            player.HealEffect(Main.rand.Next(10, 15), false);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Magenta;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<DormantHeroSword>());
            recipe.AddIngredient(ModContent.ItemType<AncientEmblem>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
