using Eternal.Projectiles.Weapons.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class HypercoreRipsaw : ModItem
    {

        public override void SetStaticDefaults() {
             Tooltip.SetDefault("Fires shards that pierce through your foes" + 
                               "\n'Powered with an Intel Core i9-12900K 3.2 GHz Processor and NVIDIA GeForce RTX-3090 Ti' graphics card");
        }

        public override void SetDefaults()
        {
            item.damage = 2000;
            item.melee = true;
            item.width = 50;
            item.height = 22;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 4f;
            item.value = Item.buyPrice(platinum: 6, gold: 30);
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 12f;
            item.shoot = ProjectileType<HypercoreRipsawProjectile>();
            item.UseSound = SoundID.Item23;
            item.autoReuse = true;
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

    }
}
