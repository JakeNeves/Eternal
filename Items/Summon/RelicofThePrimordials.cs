using Eternal.NPCs.Boss.Empraynia;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    public class RelicofThePrimordials : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unleashes The Trinity\n'Reality Trembles...'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;
            item.rare = ItemRarityID.Red;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        /*public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCType<Empraynia>());
        }*/

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

        public override bool UseItem(Player player)
        {
            //NPC.SpawnOnPlayer(player.whoAmI, NPCType<Empraynia>());
            Main.NewText("The Trinity approaches!", 175, 75, 255);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}
