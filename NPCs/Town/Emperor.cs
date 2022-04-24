using Eternal.Items.Potions;
using Eternal.Items.Vanity;
using Eternal.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.NPCs.Town
{
    [AutoloadHead]
    public class Emperor : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            name = "Emperor";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 600;
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
            npc.lifeMax = 20000;
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

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(1))
            {
                default:
                    return "Jake";
            }
        }

        public override string GetChat()
        {
            Player player = Main.player[Main.myPlayer];
            int wizard = NPC.FindFirstNPC(NPCID.Wizard);
            if (wizard >= 0 && Main.rand.NextBool(16))
            {
                return "I like how " + Main.npc[wizard].GivenName + " has some kind of powers, personally I do too... However, I am afraid that my powers are unbareablely destructive on most occations.";
            }
            int nurse = NPC.FindFirstNPC(NPCID.Nurse);
            if (nurse >= 0 && Main.rand.NextBool(16))
            {
                return "I had a friend who tried to become a medic, but it went terribly... He nearly ended up losing his medical licinse! I have no idea what he has done to deserve that...";
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
                    return "This place feel wierd to me sometimes... There are peolpe here that think I look like a truffle.";
                case 7:
                    return "I can only imagine someday, people around here are loyal to me, even though I don't rule this place.";
                case 8:
                    return "What...";
                case 9:
                    return "Me? Powerful? What are you talking about, I'm not that powerful.";
                case 10:
                    return "Can I finish my food after this?";
                case 11:
                    return "Trying to call upon the Cosmic Emperor before you can, What kind of stuff have you been playing with?";
                case 12:
                    return "I could teach you how to harvest godly powers like me, but it's going to take years before you can perfect and control your own source of power.";
                default:
                    return "Do you have an idea where I am right now?";
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
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
        }

        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20000;
            knockback = 6.2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 10;
            randExtraCooldown = 5;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<StarcrescentProjectile>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 6.75f;
            randomOffset = 1.5f;
        }

    }
}
