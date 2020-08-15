using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal.NPCs.Boss.AoI
{
    [AutoloadBossHead]
    public class ArkofImperious : ModNPC
    {
        int Timer;
        private float speed;
        private Player player;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ark of Imperious");
            NPCID.Sets.TrailCacheLength[npc.type] = 12;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 60;
            npc.height = 184;
            npc.lifeMax = 1200000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.boss = true;
            music = MusicID.Boss3;
            npc.defense = 70;
            npc.damage = 62;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.knockBackResist = 0f;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Bleeding, 180, false);
            player.AddBuff(BuffID.BrokenArmor, 180, false);
            if (EternalWorld.hellMode)
            {
                player.AddBuff(BuffID.Cursed, 180, false);
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void AI()
        {
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
