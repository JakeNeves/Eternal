using Eternal.Common.Configurations;
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

namespace Eternal.Content.NPCs.DarkMoon
{
    public class Occultist : ModNPC
    {
        int attackTimer = 0;
        int frameType = 0;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.update14;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 300;
            NPC.damage = 20;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 26;
            NPC.height = 48;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(gold: 30);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.value = 10f;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Hexed skulls that will fire at anything with no hesitation!")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.darkMoon)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
            }
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.OcculticMatter>(), 0f, -2.5f, 0, default, 1.7f);

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                NPC.position.Y = targetPosition.Y;

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.OcculticMatter>(), 0f, -2.5f, 0, default, 1.7f);
            }
            if (attackTimer == 250)
            {
                frameType = 1;

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.Shadowflames, NPC.damage, 1, Main.myPlayer, 0, 0);
            }
            if (attackTimer == 275)
            {
                frameType = 0;
                attackTimer = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameType * frameHeight;
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.PsycheFire>(), 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.PsycheFire>(), 0, -1f, 0, default(Color), 1f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OcculticMatter>(), minimumDropped: 8, maximumDropped: 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PsycheMatterAshes>(), minimumDropped: 4, maximumDropped: 12));
        }
    }
}
