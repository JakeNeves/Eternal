using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Misc
{
    public class Portadummy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.TargetDummy);
            NPC.lifeMax = int.MaxValue;
            NPC.aiStyle = -1;
            NPC.width = 32;
            NPC.height = 36;
            NPC.immortal = false;
            NPC.npcSlots = 0;
            NPC.dontCountMe = true;
            NPC.HitSound = null;
            NPC.DeathSound = null;
            NPC.noGravity = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

        public override void AI()
        {
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            NPC.spriteDirection = NPC.direction;

            NPC.life = NPC.lifeMax;

            if (NPC.active && NPC.boss)
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.active = false;
            }
        }

        public override bool CheckDead() => false;
    }
}
