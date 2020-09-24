using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    public class CosmicTablet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Extinction" +
                                "\nSummons the Champion of the cosmos" +
                                "\nThe tablet has been sealed by the champion himself..." +
                                "\n'The only way to break the seal, is to slay his mechanical dragon...'");
        }

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 54;
            item.rare = ItemRarityID.Red;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("Proove that you can challenge me first...", 0, 95, 215);
            player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was consumed by the jaws of a god eater"), 10000, 1, false);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
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
