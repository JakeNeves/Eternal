using Eternal.Projectiles.Weapons.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class BaneofAtlas : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bane of Atlas");
            Tooltip.SetDefault("Fires a bomb that explodes on impact");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 46;
            item.shootSpeed = 12f;
            item.damage = 1200;
            item.knockBack = 6f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 16;
            item.useTime = 16;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.value = Item.sellPrice(gold: 16);
            item.shoot = ModContent.ProjectileType<BaneofAtlasBomb>();
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
    }
}
