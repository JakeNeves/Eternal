using Eternal.Projectiles.Weapons.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class DoomfireTridentMelee : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomfire Trident");
            Tooltip.SetDefault("'The Representation of Wath and Dispair!'");
        }

        public override void SetDefaults()
        {
            item.damage = 2400;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 12;
            item.useTime = 24;
            item.shootSpeed = 4.5f;
            item.knockBack = 65f;
            item.width = 69;
            item.height = 67;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 1);

            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<DoomfireTridentProjectile>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkTeal;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

    }
}
