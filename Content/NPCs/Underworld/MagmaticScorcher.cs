using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Underworld
{
    public class MagmaticScorcher : ModNPC
    {
        int attackTimer = 0;
        int frameNum;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magmatic Scorcher");

            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 1000;
            NPC.damage = 20;
            NPC.defense = 30;
            NPC.knockBackResist = -1f;
            NPC.width = 28;
            NPC.height = 46;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.sellPrice(gold: 30);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

                new FlavorTextBestiaryInfoElement("These basalt constructs can ignite anything with a single cast of fire")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!DownedBossSystem.downedIncinerius)
                return SpawnCondition.Underworld.Chance * 0f;
            else
                return SpawnCondition.Underworld.Chance * 0.5f;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;

            Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

            attackTimer++;
            Attack();
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 targetPosition = Main.player[NPC.target].position;
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;

            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (attackTimer == 200)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.FlameBurst, 0f, -2.5f, 0, default, 1.7f);

                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.FlameBurst, 0f, -2.5f, 0, default, 1.7f);
            }
            if (attackTimer == 250)
            {
                frameNum = 1;

                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, NPC.damage, 1, Main.myPlayer, 0, 0);
            }
            if (attackTimer == 300)
            {
                frameNum = 0;

                attackTimer = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameNum * frameHeight;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Torch, 0, -1f, 0, default(Color), 1f);
            }
        }
    }
}
