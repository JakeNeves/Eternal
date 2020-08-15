using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.AoI
{
    public class Arkling : ModNPC
    {
        const float Speed = 10;
        const float Acceleration = 5;
        private Player player;

        public override void SetDefaults()
        {
            npc.lifeMax = 3200;
            npc.width = 28;
            npc.height = 62;
            npc.damage = 10;
            npc.defense = 30;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
        }

    }
}
