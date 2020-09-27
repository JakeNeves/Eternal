using Eternal.NPCs.Boss.AoI;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    public class RoyalLabrynthSword : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons the Ark of Imperious, the god of living swords\n'The blades of the labrynth are rising'");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            item.rare = ItemRarityID.Red;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
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
            return Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneLabrynth && !NPC.AnyNPCs(NPCType<ArkofImperious>());
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<ArkofImperious>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}
