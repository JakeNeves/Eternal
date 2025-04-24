using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Rift
{
    public class UnstableHellwisp : ModNPC
    {
        int attackTimer = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 4400;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 18;
            NPC.height = 28;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.value = Item.sellPrice(gold: 30);
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Rift>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Upon opening the rift, some of the Hellwisps absorbed the unstability of the underworld's rift potential.")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!EventSystem.isRiftOpen)
                return SpawnCondition.Underworld.Chance * 0f;
            else
                return SpawnCondition.Underworld.Chance * 0.75f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShiftblightShard>(), 2, 2, 4));
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);

            NPC.rotation = NPC.velocity.X * -0.03f;

            attackTimer++;
            Attack();
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (attackTimer == 200 || attackTimer == 215 || attackTimer == 230)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);
            }

            if (attackTimer > 240)
            {
                attackTimer = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PinkTorch, 0, -1f, 0, default(Color), 1f);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
