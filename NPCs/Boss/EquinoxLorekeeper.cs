using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Eternal.NPCs.Boss
{
    [AutoloadBossHead]
    class EquinoxLorekeeper : ModNPC
    {
        private float speed;

        public float targetX = 0f;
        public float targetY = 0f;

        private Player player;

        public int vMax = 0;
        public float vAccel = 0;

        public float tVel = 0;
        public float vMag = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equinox Lorekeeper");
            Main.npcFrameCount[npc.type] = 4;
            NPCID.Sets.TrailCacheLength[npc.type] = 10;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            vMax = 10;
            vAccel = .5f;
            npc.width = 40;
            npc.height = 61;
            npc.boss = true;
            music = MusicID.Boss3;
            npc.lifeMax = 10000;
            npc.damage = 10;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1; //mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/EquinoxLorekeeperDeath");
            npc.aiStyle = -1;
            npc.npcSlots = 1f;
            npc.value = Item.buyPrice(gold: 5);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "An " + name;
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
            npc.damage = (int)(npc.damage * 1f);
        }

        public override void AI()
        {

            Player player = Main.player[npc.target];
            targetX = player.Center.X;
            targetY = player.Center.Y;

            float dist = Vector2.Distance(npc.Center, Main.player[npc.target].Center);
            tVel = dist / 20;
            if(vMag < vMax && vMag < tVel)
            {
                vMag = vAccel;
            }
            if(vMag > tVel)
            {
                vMag -= vAccel;
            }

            if(dist != 0)
            {
                Vector2 tPos;
                tPos.X = targetX;
                tPos.Y = targetY;
                npc.velocity = npc.DirectionTo(tPos) * vMag;
            }

        }

    }
}
