using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;

namespace Eternal.NPCs.Boss.Dunekeeper
{
    public class DunekeeperHandL : ModNPC
    {
        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dunekeeper");
        }

        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 36;
            npc.lifeMax = 3000;
            npc.defense = 10;
            npc.damage = 12;
            npc.HitSound = SoundID.NPCDeath3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0f;
            npc.aiStyle = 5;
        }

        public override void AI()
        {
            npc.spriteDirection = npc.direction;
        }

    }
}
