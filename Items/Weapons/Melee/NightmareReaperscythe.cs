using Eternal.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class NightmareReaperscythe : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("<right> to throw\n'Not to be confused with the Death Sickle'");
        }

        public override void SetDefaults()
        {
            item.damage = 86;
            item.melee = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 24;
            item.useAnimation = 24;
            item.noUseGraphic = false;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 20f;
            item.value = Item.buyPrice(gold: 30, silver: 95);
            item.rare = ItemRarityID.Yellow;
            item.shootSpeed = 8.2f;
            item.shoot = ProjectileID.None;
            item.UseSound = SoundID.Item71;
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.noUseGraphic = true;
                item.useTime = 12;
                item.useAnimation = 12;
                item.shoot = ModContent.ProjectileType<NightmareReaperscytheProjectile>();
                item.shootSpeed = 8.2f;

                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                    {
                        return false;
                    }
                }
            }
            else
            {
                item.noUseGraphic = false;
                item.useTime = 24;
                item.useAnimation = 24;
                item.shoot = ProjectileID.None;
            }
            return true;

        }

    }
}
