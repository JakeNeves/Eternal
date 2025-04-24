using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Beneath
{
    public class PsyblightSkeleton : ModNPC
    {
        int attackTimer = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Skeleton];
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 20;
            NPC.defense = 15;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(gold: 8);
            NPC.knockBackResist = 0.9f;
            NPC.aiStyle = 3;
            AIType = NPCID.Skeleton;
            AnimationType = NPCID.Skeleton;
            Banner = Item.NPCtoBanner(NPCID.Skeleton);
            BannerItem = Item.BannerToItem(Banner);
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Beneath>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("These aren't you're ordinary skeletons, these ones somehow learned a bit of telepathy, but it doesn't go well everytime!")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && ModContent.GetInstance<ZoneSystem>().zoneBeneath)
                return SpawnCondition.Cavern.Chance * 0.15f;
            else
                return SpawnCondition.Cavern.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PsyblightEssence>(), minimumDropped: 2, maximumDropped: 6));
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

            if (attackTimer == 200 || attackTimer == 300 || attackTimer == 400)
            {
                if (!Main.dedServ)
                {
                    Projectile.NewProjectile(entitySource, NPC.Center, direction, ModContent.ProjectileType<Psyfireball>(), 0, 0f, Main.myPlayer);
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/Psy")
                    {
                        Volume = 0.8f,
                        PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                        MaxInstances = 0,
                    }, NPC.Center);
                }
            }

            if (attackTimer == 500)
                attackTimer = 0;
        }
    }
}
