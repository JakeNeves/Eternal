using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Mausoleum
{
    public class ArcaneWisp : ModNPC
    {
        ref float AttackTimer => ref NPC.ai[1];

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            if (Main.hardMode)
            {
                NPC.lifeMax = 250;
                NPC.damage = 20;
                NPC.defense = 30;
            }
            else
            {
                NPC.lifeMax = 25;
                NPC.damage = 5;
                NPC.defense = 10;
            }
            NPC.knockBackResist = -1f;
            NPC.width = 18;
            NPC.height = 28;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.value = Item.sellPrice(gold: 1);
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Mausoleum>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Spirits of the dungeon fear this blazing spiritual hellspawn of an amalgamation.")
            ]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule isHardmodeRule = new(new Conditions.IsHardmode());

            isHardmodeRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PsyblightEssence>(), 2, 2, 4));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MausoleumKeyFragment1>(), 12));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneMausoleum && Main.hardMode)
                return SpawnCondition.Cavern.Chance * 0.09f;
            else
                return SpawnCondition.Cavern.Chance * 0f;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);

            NPC.rotation = NPC.velocity.X * -0.03f;

            AttackTimer++;
            Attack();
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (AttackTimer == 200 || AttackTimer == 240 || AttackTimer == 280)
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot, NPC.position);

                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1, Main.myPlayer, 0, 0);
            }

            if (AttackTimer > 320)
            {
                AttackTimer = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);

                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PurpleTorch, 0, -1f, 0, default, 1f);
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
