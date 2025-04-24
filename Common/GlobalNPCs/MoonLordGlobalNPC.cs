using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalNPCs
{
    public class MoonLordGlobalNPC : GlobalNPC
    {
        public static LocalizedText DropFallenComet { get; private set; }

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.MoonLordCore;
        }

        public override void SetStaticDefaults()
        {
            DropFallenComet = Mod.GetLocalization($"WorldGen.{nameof(DropFallenComet)}");
        }

        public override void OnKill(NPC npc)
        {
            if (!NPC.downedMoonlord)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(DropFallenComet.Value, 0, 215, 215);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(DropFallenComet.ToNetworkText(), new Color(0, 215, 215));
                }

                CometSystem.DropComet();
            }
            else
            {
                if (Main.rand.NextBool(2))
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText(DropFallenComet.Value, 0, 215, 215);
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        ChatHelper.BroadcastChatMessage(DropFallenComet.ToNetworkText(), new Color(0, 215, 215));
                    }

                    CometSystem.DropComet();
                }
            }
        }
    }
}
