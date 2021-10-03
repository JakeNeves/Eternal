using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Items.Materials;
using Eternal.Buffs;
using Eternal.Items.Potions;
using Eternal.Items.Tools;
using Eternal.Projectiles.Boss;

namespace Eternal.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperor : ModNPC
    {

        int attackTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Emperor");
            NPCID.Sets.TrailCacheLength[npc.type] = 18;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 50;
            npc.defense = 760;
            npc.damage = 760;
            npc.lifeMax = 12000000;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/EmperorHit");
            npc.DeathSound = null;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/PlayingPuny");
            npc.buffImmune[BuffID.Chilled] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Suffocation] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.BetsysCurse] = true;
            npc.buffImmune[BuffID.Daybreak] = true;
            npc.buffImmune[BuffID.DryadsWardDebuff] = true;
            npc.buffImmune[ModContent.BuffType<EmbericCombustion>()] = true;
            npc.buffImmune[ModContent.BuffType<DoomFire>()] = true;
            if (Eternal.instance.CalamityLoaded)
            {
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ExoFreeze")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("GlacialState")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TemporalSadness")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("SilvaStun")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TimeSlow")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("PearlAura")] = true;
            }
            if (Eternal.instance.FargowiltasModLoaded)
            {
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Lethargic")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Sadism")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("GodEater")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("ClippedWings")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("MutantNibble")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("OceanicMaul")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("TimeFrozen")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("LightningRod")] = true;
            }
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.knockBackResist = -1f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 24000000;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 36000000;
            }
        }

        public override void AI()
        {
            attackTimer++;

            if (EternalWorld.hellMode)
            {
                if (attackTimer == 100 || attackTimer == 102 || attackTimer == 104 || attackTimer == 106 || attackTimer == 108 || attackTimer == 110)
                {
                    if (Main.rand.Next(1) == 0)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<StarBomb>(), 12, 0, Main.myPlayer, 0f, 0f);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<CosmicRing>(), 12, 0, Main.myPlayer, 0f, 0f);
                    }
                    
                }
                if (attackTimer == 112)
                {
                    attackTimer = 0;
                }
            }
            else
            {
                if (attackTimer == 100 || attackTimer == 110 || attackTimer == 120 || attackTimer == 130 || attackTimer == 140 || attackTimer == 150)
                {
                    if (Main.rand.Next(1) == 0)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<StarBomb>(), 12, 0, Main.myPlayer, 0f, 0f);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<CosmicRing>(), 12, 0, Main.myPlayer, 0f, 0f);
                    }
                }
                if (attackTimer == 160)
                {
                    attackTimer = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width, npc.height);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                Texture2D shadowTexture = mod.GetTexture("NPCs/Boss/CosmicEmperor/CosmicEmperor_Shadow");
                SpriteEffects spriteEffects = npc.direction != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                spriteBatch.Draw(shadowTexture, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
            }
            return true;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                Main.PlaySound(SoundID.DD2_BallistaTowerShot, (int)npc.position.X, (int)npc.position.Y);
                if (Main.rand.Next(1) == 0)
                {
                    Main.NewText("No!", 0, 95, 215);
                }
                if (Main.rand.Next(2) == 0)
                {
                    Main.NewText("I shall not tolerate such action...", 0, 95, 215);
                }
                if (Main.rand.Next(3) == 0)
                {
                    Main.NewText("What is wrong with you?", 0, 95, 215);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.NewText("You think your black magic can withstand my potental?", 0, 95, 215);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.NewText("What an absolute cheater you are.", 0, 95, 215);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.NewText("Don't you butcher me with your nonsense!", 0, 95, 215);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.NewText("That did not penetrate me...", 0, 95, 215);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.NewText("Maybe you should go butcher someone else, not me!", 0, 95, 215);
                }

                damage = 0;
            }
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(npc.position, 0.75f, 0f, 0.75f);

            float speed;
            if (EternalWorld.hellMode)
            {
                speed = 48f;
            }
            else
            {
                speed = 36f;
            }
            float acceleration = 0.20f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (length > 400 && Main.expertMode)
            {
                ++speed;
                acceleration += 0.05F;
                if (length > 600)
                {
                    ++speed;
                    acceleration += 0.05F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.05F;
                    }
                }
            }
            float num10 = speed / length;
            xDir = xDir * num10;
            yDir = yDir * num10;
            if (npc.velocity.X < xDir)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if (npc.velocity.X < 0 && xDir > 0)
                    npc.velocity.X = npc.velocity.X + acceleration;
            }
            else if (npc.velocity.X > xDir)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.X > 0 && xDir < 0)
                    npc.velocity.X = npc.velocity.X - acceleration;
            }
            if (npc.velocity.Y < yDir)
            {
                npc.velocity.Y = npc.velocity.Y + acceleration;
                if (npc.velocity.Y < 0 && yDir > 0)
                    npc.velocity.Y = npc.velocity.Y + acceleration;
            }
            else if (npc.velocity.Y > yDir)
            {
                npc.velocity.Y = npc.velocity.Y - acceleration;
                if (npc.velocity.Y > 0 && yDir < 0)
                    npc.velocity.Y = npc.velocity.Y - acceleration;
            }

            return true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override void NPCLoot()
        {
            Main.NewText("You've done well... here is a gift that you can have!", 0, 95, 215);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InterstellarMetal>(), 99);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CosmoniumFragment>(), 99);

            if (Main.rand.Next(1) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ExosiivaGladiusBlade>());
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TheBigOne>());
            }
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Exelodon>());
            }
        }

    }
}
