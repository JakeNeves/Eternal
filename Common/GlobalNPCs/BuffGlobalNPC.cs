using Eternal.Common.Configurations;
using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Misc;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalNPCs
{
    public class BuffGlobalNPC : GlobalNPC
    {
        public static bool fidget = false;

        public static int fidgetTimer;

        public override void ResetEffects(NPC npc)
        {
            fidget = false;

            fidgetTimer = 0;
        }

        public override void PostAI(NPC npc)
        {
            if (fidget)
            {
                fidgetTimer++;

                if (fidgetTimer > 30)
                {
                    npc.velocity.X = Main.rand.NextFloat(-1f, 1f);
                }
                else if (fidgetTimer == 45)
                {
                    fidgetTimer = 0;
                }
            }
        }
    }
}
