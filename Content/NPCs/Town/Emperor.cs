using Eternal.Content.Projectiles.Weapons.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Town
{
    [AutoloadHead]
    public class Emperor : ModNPC
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
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Mechanic, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate)
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
            NPC.lifeMax = 60000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.DD2_GhastlyGlaivePierce;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
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
                if (NPC.downedMoonlord)
                    return true;
            }
            return false;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
                shop = true;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Jake"
            };
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life < 0)
            {
                int num = NPC.life > 0 ? 1 : 10;
                for (int k = 0; k < num; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BlueTorch);
                }
            }
            else {
                int num = NPC.life > 0 ? 1 : 5;
                for (int k = 0; k < num; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
                }
            }

            if (NPC.life < 1)
            {
                int emperor = NPC.FindFirstNPC(ModContent.NPCType<Emperor>());
                NPC.life = 1;
                NPC.active = false;
                Main.NewText(Main.npc[emperor].GivenName + " the Emperor has returned to his world", 255, 0, 0);
            }
        }

        public override string GetChat()
        {
            Player player = Main.player[Main.myPlayer];
            int wizard = NPC.FindFirstNPC(NPCID.Wizard);
            if (wizard >= 0 && Main.rand.NextBool(18))
            {
                return "I like how " + Main.npc[wizard].GivenName + " has some kind of powers, personally I do too... However, I am afraid that my powers are unbareablely destructive on most occations.";
            }
            int nurse = NPC.FindFirstNPC(NPCID.Nurse);
            if (nurse >= 0 && Main.rand.NextBool(18))
            {
                return "I had a friend who was a medic, but things went terribly... He nearly ended up losing his medical licinse! I have no idea what he has done to deserve that...";
            }
            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            if (partyGirl >= 0 && Main.rand.NextBool(18))
            {
                return "I like partys and all, but some people like me, perfer to have there party like a late-night sit back and relax kinda situation...";
            }
            int merchant = NPC.FindFirstNPC(NPCID.Merchant);
            if (merchant >= 0 && Main.rand.NextBool(18))
            {
                return "That " + Main.npc[merchant].GivenName + " guy, I don't know about you, but he likes to sell angel statues... Everyone knows they do nothing!";
            }
            switch (Main.rand.Next(16))
            {
                case 0:
                    return "What am I even doing here?";
                case 1:
                    return "I suppose this place feel really wacky to me.";
                case 2:
                    return "I rule an empire, that's all I do...";
                case 3:
                    return "Dr. Sebaston Kox does lead a really good space entity resarch team, am I right?";
                case 4:
                    return "I know what you're thinking, do I look like I am ready for a russian winter? Yes, I do!";
                case 5:
                    if (player.ZoneSnow)
                        return "Wow, this winter wonderland is just like the same winter feeling I get over in the Gallahard Empire!";
                    else
                        return "Honestly, I feel that this place could use some winter wonders...";
                case 6:
                    if (player.ZoneSnow || player.ZoneDesert)
                        return "Why is it so hot down here, I wasn't built for places like this!";
                    else if (player.ZoneDesert && !Main.dayTime || player.ZoneSnow)
                        return "Okay to be honest, it's quite cold and I like it...";
                    else
                        return "Mmmm... Oh, sorry this place isn't good enough.";
                case 7:
                    return "I can only imagine someday, people around here are loyal to me, even though I don't rule this place.";
                case 8:
                    return "What...";
                case 9:
                    return "Me? Powerful? What are you talking about, I'm not that powerful.";
                case 10:
                    return "Can I finish my food after this?";
                case 11:
                    return "Trying to fight some big colossal enemies right before you should, what kind of sourcery are you playing with?";
                case 12:
                    return "I could teach you how to harvest godly powers like me, but it's going to take years before you can perfect and control your own source of power.";
                default:
                    return "Do you have an idea where I am right now?";
            }
        }

        /*public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<JakesHat>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<JakesCoat>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<JakesKicks>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<JakesCosmicWings>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<PristineHealingPotion>());
            nextSlot++;
        }*/

        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 2000;
            knockback = 6.2f;
        }

        public void StatueTeleport()
        {
            Main.NewText("How did I even get here?", 24, 96, 210);
        }


        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 10;
            randExtraCooldown = 5;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<StarspearProjectileThrown>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 6.75f;
            randomOffset = 1.5f;
        }

    }
}
