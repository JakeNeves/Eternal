using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CarminiteAmalgamation
{
    public class DeadCarminiteAmalgamation : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 92;
            NPC.height = 68;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1;
            NPC.lavaImmune = true;
            NPC.knockBackResist = -1f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.dontTakeDamage = true;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            player.AddBuff(BuffID.Blackout, 1, false);
        }
    }
}
