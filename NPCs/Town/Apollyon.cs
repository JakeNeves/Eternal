using System;
using Eternal.Items;
using Eternal.Items.Materials;
using Eternal.Items.Placeable.Torch;
using Eternal.Items.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.NPCs.Town
{
    [AutoloadHead]
    public class Apollyon : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            name = "Apollyon";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 35;
            npc.height = 55;
            npc.aiStyle = 7;
            npc.damage = 20;
            npc.defense = 30;
            npc.lifeMax = 500;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }
                if (NPC.downedBoss1)
                    return true;
            }
            return false;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Apollyon Coins";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
                shop = true;
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
                switch(Main.rand.Next(3))
                {
                    case 0:
                        Main.npcChatText = "Apollyon Coins have a chance to be dropped by creatures within the darkest part of this world.";
                        break;
                    case 1:
                        Main.npcChatText = "Apollyon Coins are basically a way of buying items from me without having to spend real money on what I sell...";
                        break;
                    default:
                        Main.npcChatText = "Apollyon Coins are the currency that I use for exchanging for items with, just like actual money. What else can you do with this mysterious stone?";
                        break;
                }
            }
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(10))
            {
                case 0:
                    return "Ian";
                case 1:
                    return "Judas";
                case 2:
                    return "Midnight";
                case 3:
                    return "Wolfgang";
                case 4:
                    return "Wheatly";
                case 5:
                    return "Victor";
                case 6:
                    return "Hank";
                case 7:
                    return "Fredrickson";
                case 8:
                    return "Auditor";
                case 9:
                    return "Leland";
                default:
                    return "Azazel";
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num = npc.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Wraith);
            }
        }

        public override string GetChat()
        {
            int merchant = NPC.FindFirstNPC(NPCID.Merchant);
            int tMerchant = NPC.FindFirstNPC(NPCID.TravellingMerchant);
            if (merchant >= 0 && Main.rand.NextBool(10) || tMerchant >= 0 && Main.rand.NextBool(10))
            {
                return "This merchant guy around here... He tried to offer me some sort of angel statue... Someone told me it does nothing apparently.";
            }
            int wizard = NPC.FindFirstNPC(NPCID.Wizard);
            if (wizard >= 0 && Main.rand.NextBool(10))
            {
                return "Have I ever told you, that I was into dark arts... Probably not. I would perform some twisted necromancy, but I would probably cause chaos...";
            }
            int emperor = NPC.FindFirstNPC(ModContent.NPCType<Emperor>());
            if (emperor >= 0 && Main.rand.NextBool(10))
            {
                return "I prase our glorious emperor of Gallahard, I gaze upon his power, I respect him well!";
            }
            switch (Main.rand.Next(10))
            {
                case 0:
                    return "Some people seem be frightened by my appearence, I don't know why?";
                case 1:
                    return "Hello, I am error!";
                case 2:
                    if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneBeneath)
                        return "I am pretty sure, you should stay out of here, unless you have some sort of lantern that can help you navigate this kind of darkness, torches will not work, they just simply can't provide enough light here!";
                    else
                        return "Dang, I have never seen the sun in years!";
                case 3:
                    return "I know something that can really spice this place up a little bit, but I just won't tell you!";
                case 4:
                    return "If you compare me to a Wraith, then I don't know what's wrong with you...";
                case 5:
                    return "As an apollyon from the dark side, I can confirm that I know the name of a shadow monster, however I don't think you want to know who the shadow monster is... He looks terrifying";
                default:
                    return "I may look evil, but I'm actually quite nice!";
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<SpiritTablet>());
            shop.item[nextSlot].shopCustomPrice = new int?(12);
            shop.item[nextSlot].shopSpecialCurrency = Eternal.ApollyonCoin;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<GrimstoneTorch>());
            shop.item[nextSlot].shopCustomPrice = new int?(2);
            shop.item[nextSlot].shopSpecialCurrency = Eternal.ApollyonCoin;
            nextSlot++;
            if (EternalWorld.downedArkOfImperious)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ArkaniumCompound>());
                shop.item[nextSlot].shopCustomPrice = new int?(24);
                shop.item[nextSlot].shopSpecialCurrency = Eternal.ApollyonCoin;
                nextSlot++;
            }
            if (EternalWorld.downedIncinerius)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ScorchedMetal>());
                shop.item[nextSlot].shopCustomPrice = new int?(16);
                shop.item[nextSlot].shopSpecialCurrency = Eternal.ApollyonCoin;
                nextSlot++;
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
