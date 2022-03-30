using Terraria;
using Terraria.ModLoader;
using Eternal.Items.Ammo;
using Eternal.Dusts;
using Terraria.ID;

namespace Eternal.NPCs.Boss.InfernoGuardian
{
    [AutoloadBossHead]
    public class InfernoGuardian : ModNPC
    {
        int spawnType;
        int stage = 0;
        public static float GIKill = 0f;
        bool isDefeated = false;
        int timer;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Great Ignus");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 6;
            npc.defense = 999;
            npc.width = 72;
            npc.height = 65;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            npc.boss = true;
            Mod musicMod = ModLoader.GetMod("EternalMusic");
            if (musicMod == null)
            {
                music = MusicID.Boss5;
            }
            else
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/UnfatefulStrike");
            }
        }

        public override void AI()
        {

            Lighting.AddLight(npc.position, 2.55f, 1.55f, 0.33f);

            #region GI Stage Handling
            switch (stage)
            {
                case 0:
                    npc.life = 1;
                    break;
                case 1:
                    npc.life = 2;
                    break;
                case 2:
                    npc.life = 3;
                    break;
                case 3:
                    npc.life = 4;
                    break;
                case 4:
                    npc.life = 5;
                    break;
                case 5:
                    npc.life = 6;
                    break;
            }
            #endregion

            if(!isDefeated)
            {
                timer++;

                switch (timer)
                {
                    case 200:
                        spawnType = Main.rand.Next(1, 2);
                        SpawnEnemies();
                        break;
                    case 300:
                        timer = 0;
                        break;
                }
            }

            if (GIKill >= 6)
            {
                isDefeated = true;
                timer = 0;
            }

            if (isDefeated)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/AltBossDefeat"), Main.myPlayer);

                #region GI Loot
                if (Main.expertMode)
                {
                    //npc.DropBossBags();
                }
                else
                {
                    if (Main.rand.Next(1) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EmberArrow>(), Main.rand.Next(6, 24));
                    }
                }
                #endregion

                npc.life = 0;

                GIKill = 0;
                stage = 0;

            }

            if (npc.life == 0)
            {
                Main.NewText("The Great Ignus has been defeated!", 175, 75, 255);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/InfernoGuardian"), 1f);
            }
        }

        public void SpawnEnemies()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<SmolIgnusling>()) && !NPC.AnyNPCs(ModContent.NPCType<HellGazer>()))
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<EmbericCombustion>(), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                }

                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<HellGazer>(), 0, 2, 1, 0, npc.whoAmI, npc.target);
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<SmolIgnusling>(), 0, 2, 1, 0, npc.whoAmI, npc.target);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.10f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }
    }
}
