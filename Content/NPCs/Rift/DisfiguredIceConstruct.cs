using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Rift
{
    public class DisfiguredIceConstruct : ModNPC
    {
        int attackTimer;

        public override void SetDefaults()
        {
            NPC.width = 68;
            NPC.height = 68;
            NPC.lifeMax = 45000;
            NPC.defense = 20;
            NPC.damage = 48;
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.Shatter;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SnowRift>().Type };
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {

            }
            else
            {
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Ice);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.player[Main.myPlayer];

            if (RiftSystem.isRiftOpen && DownedBossSystem.downedArkofImperious && player.ZoneSnow)
            {
                return SpawnCondition.Overworld.Chance * 1.5f;
            }
            else
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoteofOminite>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawNaquadah>(), 2, 1, 2));
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            NPC.spriteDirection = NPC.direction;
            NPC.rotation += NPC.velocity.X * 0.1f;

            attackTimer++;
            if (attackTimer == 125 || attackTimer == 150 || attackTimer == 175)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);
            }
            else if (attackTimer == 250)
            {
                attackTimer = 0;
            }
        }
    }
}
