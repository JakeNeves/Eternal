using Eternal.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalNPCs
{
    public class MoonLordGlobalNPC : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.MoonLordCore;
        }

        public override void OnKill(NPC npc)
        {
            if (!NPC.downedMoonlord)
            {
                Main.NewText("A comet has landed and struck the world!", 0, 215, 215);
                CometSystem.DropComet();
            }
            else
            {
                if (Main.rand.NextBool(2))
                {
                    Main.NewText("A comet has landed and struck the world!", 0, 215, 215);
                    CometSystem.DropComet();
                }
            }
        }
    }
}
