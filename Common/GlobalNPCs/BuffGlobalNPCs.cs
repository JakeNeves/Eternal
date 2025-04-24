using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Players;
using Microsoft.Xna.Framework;

namespace Eternal.Common.GlobalNPCs
{
    public class BuffGlobalNPCs : GlobalNPC
    {
        public override void ResetEffects(NPC npc)
        {
            ModContent.GetInstance<BuffSystem>().error = false;
            ModContent.GetInstance<BuffSystem>().immenseFracture = false;
        }

        public override void AI(NPC npc)
        {
            if (ModContent.GetInstance<BuffSystem>().error)
                npc.rotation = 105f;

            if (ModContent.GetInstance<BuffSystem>().immenseFracture)
                npc.color = Color.DarkRed;
        }
    }
}
