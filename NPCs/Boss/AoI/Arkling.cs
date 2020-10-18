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
        
        public override void SetDefaults()
        {
            npc.lifeMax = 400;
            npc.width = 38;
            npc.height = 72;
            npc.damage = 10;
            npc.defense = 30;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.aiStyle = 5;
        }

        public override void AI()
        {
            npc.spriteDirection = npc.direction;
        }

    }
}
