using Eternal.Common.Systems;
using Eternal.Content.Biomes;
using Eternal.Content.Items.Misc;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Town
{
    [AutoloadHead]
    public class Apollyon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 600;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = 1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<Beneath>(AffectionLevel.Love)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
            ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 35;
            NPC.height = 55;
            NPC.aiStyle = 7;
            NPC.damage = 20;
            NPC.defense = 60;
            NPC.lifeMax = 45000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<EmperorsTrust>()))
                    return true;
            }
            return false;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Salvage";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        Main.npcChatText = "Here take this, I'm pretty sure it has some future use or something.";
                        break;
                    case 1:
                        Main.npcChatText = "I'll take that and yoink! Here, this might have a use but i'm not sure by any chance.";
                        break;
                    default:
                        Main.npcChatText = "There you go, talk to me again to salvage more weird things worth salvaging soon!";
                        break;
                }
            }
            else
            {
                Player player = Main.player[Main.myPlayer];
                /*if (Main.LocalPlayer.HasItem(ModContent.ItemType<DepthsDebris>()))
                {
                    Main.PlaySound(SoundID.Tink);
                    Main.npcChatText = "Sweet, here is your reward, feel free to come see me again if you want to exchange more materials for my Apollyon Coins, otherwise spend some on what I have to offer " + player.name + "!";
                    int debris = Main.LocalPlayer.FindItem(ModContent.ItemType<DepthsDebris>());
                    Main.LocalPlayer.inventory[debris].TurnToAir();
                    Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<ApollyonCoin>());
                    return;
                }
                else
                {
                    Main.npcChatText = "I would let you have some of my special tokens, however you must find me something that I can use to make them with " + player.name + ", try looking for creatures from deep below in the darkest area of the world.";
                }*/
                switch (Main.rand.Next(3))
                {
                    case 0:
                        Main.npcChatText = "If there's anything I can salvage for you great, otherwise I don't know what to salvage...";
                        break;
                    case 1:
                        Main.npcChatText = "Find me something worth a good salvage, I'm sure I can give you something in return.";
                        break;
                    default:
                        Main.npcChatText = "Do you even have anything salvagable by any chance?";
                        break;
                }
            }
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Shadowmere",
                "Auditor",
                "Antrialfardeus",
                "Baaldra",
                "Arsakugane",
                "Solatas",
                "Bruuh",
                "Shadesied",
                "Deraantus",
                "Suudokode",
                "Taartayog",
                "Dusky",
                "Zantuuga"
            };
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Wraith);
            }
        }

        public override string GetChat()
        {
            int merchant = NPC.FindFirstNPC(NPCID.Merchant);
            int tMerchant = NPC.FindFirstNPC(NPCID.TravellingMerchant);
            if (merchant >= 0 && Main.rand.NextBool(9) || tMerchant >= 0 && Main.rand.NextBool(9))
            {
                return "This merchant guy around here... He tried to offer me some sort of angel statue... Someone told me it does nothing apparently.";
            }
            int wizard = NPC.FindFirstNPC(NPCID.Wizard);
            if (wizard >= 0 && Main.rand.NextBool(9))
            {
                return "Have I ever told you, that I was into dark arts... Probably not. I would perform some twisted necromancy, but I would probably cause chaos...";
            }
            int emperor = NPC.FindFirstNPC(ModContent.NPCType<Emperor>());
            if (emperor >= 0 && Main.rand.NextBool(9))
            {
                return "I prase our glorious emperor of Gallahard, I gaze upon his power, I respect him well!";
            }
            switch (Main.rand.Next(9))
            {
                case 0:
                    return "Some people seem be frightened by my appearence, I don't know why?";
                case 1:
                    return "Hello, I am error!";
                case 2:
                    if (ModContent.GetInstance<ZoneSystem>().zoneBeneath)
                        return "I am pretty sure, you should stay out of here, unless you have some sort of lantern that can help you navigate this kind of darkness, torches will not work, they just simply can't provide enough light here!";
                    else
                        return "Dang, I have never seen the sun in years!";
                case 3:
                    return "I know something that can really spice this place up a little bit, but I just won't tell you!";
                case 4:
                    return "If you compare me to a Wraith, then I don't know what's wrong with you...";
                default:
                    return "I may look evil, but I'm actually quite nice!";
            }
        }

        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 40;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.InfernoFriendlyBolt;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

    }
}
