using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class Vexation : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Heals you upon striking an enemy");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 44;
            item.damage = 440;
            item.knockBack = 3.5f;
            item.value = Item.buyPrice(platinum: 1, gold: 3);
            item.useTime = 16;
            item.useAnimation = 16;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.melee = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (EternalPlayer.StarbornArmorMeleeBonus)
            {
                player.HealEffect(6, false);
            }
            else
            {
                player.HealEffect(3, false);
            }
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
