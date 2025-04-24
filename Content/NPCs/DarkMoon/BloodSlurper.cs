using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class BloodSlurper : ModNPC
    {
        int attackTimer = 0;

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 44;
            NPC.lifeMax = 250;
            NPC.defense = 20;
            NPC.damage = 20;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RottedHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.1f, 0.5f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RottedDeath");
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Bleeding, 1 * 60 * 60, false);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("A monsterous nightmare, brought to life by twisted occultists and necromancers.")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.darkMoon)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.15f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OcculticMatter>(), minimumDropped: 2, maximumDropped: 6));
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

            if (attackTimer == 200 || attackTimer == 225 || attackTimer == 300 || attackTimer == 325)
            {
                 if (!Main.dedServ)
                 {
                     Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                     Projectile.NewProjectile(entitySource, NPC.Center, direction, ModContent.ProjectileType<Sanguinebeam2>(), 0, 0f, Main.myPlayer);
                     SoundEngine.PlaySound(SoundID.Item167, NPC.Center);
                     SoundEngine.PlaySound(SoundID.NPCDeath22, NPC.Center);
                 }
            }

            if (attackTimer > 350)
                attackTimer = 0;
        }
    }
}
