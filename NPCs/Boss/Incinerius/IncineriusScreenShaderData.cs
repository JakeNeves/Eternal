using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.Incinerius
{
    public class IncineriusScreenShaderData : ScreenShaderData
    {

        private int incineriusIndex;

        public IncineriusScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateIncineriusIndex()
        {
            int IncineriusType = ModLoader.GetMod("Eternal").NPCType("Incinerius");
            if (incineriusIndex >= 0 && Main.npc[incineriusIndex].active && Main.npc[incineriusIndex].type == IncineriusType)
            {
                return;
            }
            incineriusIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == IncineriusType)
                {
                    incineriusIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateIncineriusIndex();
            if (incineriusIndex != -1)
            {
                UseTargetPosition(Main.npc[incineriusIndex].Center);
            }
            base.Apply();
        }

    }
}
