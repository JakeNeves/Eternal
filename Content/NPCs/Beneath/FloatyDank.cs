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

namespace Eternal.Content.NPCs.Beneath
{
    public class FloatyDank : ModNPC
    {
        int attackTimer = 0;

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 44;
            NPC.lifeMax = 250;
            NPC.defense = 20;
            NPC.damage = 20;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Beneath>().Type ];
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Slow, 1 * 60 * 60, false);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Not to be confused with the Floaty Gross, they're known to cause havoc to those who step into it's territory.")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneBeneath)
            {
                return SpawnCondition.Cavern.Chance * 0.1f;
            }
            else
            {
                return SpawnCondition.Cavern.Chance * 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PsyblightEssence>(), 1, 2, 4));
        }

        public override void AI()
        {
            attackTimer++;

            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.02f;

            if (attackTimer == 150)
            {
                for (int i = 0; i < Main.rand.Next(2, 4); i++)
                {
                     if (!Main.dedServ)
                     {
                         Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                         Projectile.NewProjectile(entitySource, NPC.Center, direction, ProjectileID.InsanityShadowHostile, 0, 0f, Main.myPlayer);
                         SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                     }
                }

                attackTimer = 0;
            }
        }
    }
}
