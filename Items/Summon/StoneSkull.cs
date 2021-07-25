using Eternal.Dusts;
using Eternal.NPCs.Boss.InfernoGuardian;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Summon
{
    public class StoneSkull : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons The Great Ignus");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;
            item.rare = ItemRarityID.LightPurple;
            item.useAnimation = 45;
            item.useTime = 45;
            item.maxStack = 99;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<InfernoGuardian>());
        }

        public override bool UseItem(Player player)
        {
            NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 100, ModContent.NPCType<InfernoGuardian>());
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(player.Center, (int)player.Center.X, (int)player.Center.Y + 100, ModContent.DustType<EmbericCombustion>());
            }
            Main.NewText("The Great Igneous has awoken!", 175, 75, 255);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}
