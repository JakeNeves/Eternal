using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Items.BossBags;
using Eternal.Items;
using Eternal.Items.Accessories.Hell;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.Weapons.Summon;

namespace Eternal.NPCs.Boss.SubzeroElemental
{
    [AutoloadBossHead]
    public class SubzeroElemental : ModNPC
    {

        #region Fundimentals
        private Player player;
        int AttackTimer = 0;
        int Phase = 0;
        int Speed = 0;
        #endregion

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 59;
            npc.height = 71;
            npc.lifeMax = 46000;
            npc.damage = 50;
            npc.defense = 75;
            npc.boss = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.aiStyle = -1;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Electrified] = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            bossBag = ItemType<SubzeroElementalBag>();
            music = MusicID.Boss3;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 81200;
            npc.damage = (int)(npc.damage + 1f);
            npc.defense = (int)(npc.defense + numPlayers);
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 12250;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalHead1"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalHead2"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalBody"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalCrystal"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalLeftArm"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalLeftHand"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalRightArm"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalRightHand"), 1f);
            }
        }

        public override void AI()
        {

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
            }
            
            npc.spriteDirection = npc.direction;

            Speed = 5;

            AttackTimer++;

            Player player = Main.player[npc.target];
            npc.TargetClosest(true);

            npc.spriteDirection = npc.direction;

            Vector2 moveTo = player.Center - npc.Center;
            moveTo.Normalize();
            moveTo = moveTo * Speed;

            npc.velocity = moveTo;

            //Despawn Handler
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.velocity.Y = npc.velocity.Y - 0.1f;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                    return;
                }
            }

            if (Phase == 0)
            {
                if(AttackTimer == 100)
                {
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -90, 0, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 90, 0, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 0, 90, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 0, -90, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                }
                if (AttackTimer == 175)
                {
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -90, -45, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 90, -45, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -90, 45, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 90, 45, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                }
                if (AttackTimer == 250)
                {
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -90, 0, ProjectileID.CultistBossIceMist, 5, 0, Main.myPlayer, 0f, 0f);
                }
                if(AttackTimer == 325)
                {
                    AttackTimer = 0;
                }
            }
            else if(Phase == 1 || EternalWorld.hellMode)
            {
                Speed = 15;
                if(AttackTimer == 125)
                {
                    NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCID.IceElemental);
                    NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCID.IceElemental);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -90, 0, ProjectileID.CultistBossIceMist, 5, 0, Main.myPlayer, 0f, 0f);
                }
                if(AttackTimer == 150)
                {
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -30, 0, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 30, 0, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 0, 30, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 0, -30, ProjectileID.FrostBlastHostile, 5, 0, Main.myPlayer, 0f, 0f);
                }
                if(AttackTimer == 200)
                {
                    Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -90, 0, ProjectileID.CultistBossIceMist, 5, 0, Main.myPlayer, 0f, 0f);
                    AttackTimer = 0;
                }
            }
        }

        public override void NPCLoot()
        {
            if(!EternalWorld.downedSubzeroElemental)
            {
                Main.NewText("The air is getting fridged in the tundra...", 0, 95, 215);
                Main.NewText("Someone is reconizing your devotion into preventing the underworld from freezing...", 0, 80, 200);
                EternalWorld.downedSubzeroElemental = true;
            }

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                    player.QuickSpawnItem(ItemType<TheKelvinator>());
                }

                if (Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(ItemType<FrostGladiator>());
                }

                if (Main.rand.Next(3) == 0)
                {
                    player.QuickSpawnItem(ItemType<FrostyImmaterializer>());
                }
            }

        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
            name = "A " + name;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
