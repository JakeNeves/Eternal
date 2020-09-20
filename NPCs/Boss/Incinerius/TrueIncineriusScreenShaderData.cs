using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.Incinerius
{
    public class TrueIncineriusScreenShaderData : ScreenShaderData
    {

        private int TrueIncineriusIndex;

        public TrueIncineriusScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateTrueIncineriusIndex()
        {
            int TrueIncineriusType = ModLoader.GetMod("Eternal").NPCType("Incinerius");
            if (TrueIncineriusIndex >= 0 && Main.npc[TrueIncineriusIndex].active && Main.npc[TrueIncineriusIndex].type == TrueIncineriusType)
            {
                return;
            }
            TrueIncineriusIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == TrueIncineriusType)
                {
                    TrueIncineriusIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateTrueIncineriusIndex();
            if (TrueIncineriusIndex != -1)
                UseTargetPosition(Main.npc[TrueIncineriusIndex].Center);

            base.Apply();
        }

    }
}
