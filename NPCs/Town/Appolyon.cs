using System;
using Eternal.Items;
using Eternal.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.NPCs.Town
{
    [AutoloadHead]
    public class Appolyon : ModNPC
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
                if (!player.active || Main.hardMode)
                {
                    continue;
                }
            }
            return false;
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            shop = firstButton;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(8))
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
                default:
                    return "Azel";
            }
        }

        public override string GetChat()
        {
            int merchant = NPC.FindFirstNPC(NPCID.Merchant);
            int tMerchant = NPC.FindFirstNPC(NPCID.TravellingMerchant);
            if (merchant >= 0 && Main.rand.NextBool(8) || tMerchant >= 0 && Main.rand.NextBool(8))
            {
                return "This merchant guy around here... He tried to offer me some sort of angel statue... Someone told me it does nothing apparently.";
            }
            int wizard = NPC.FindFirstNPC(NPCID.Wizard);
            if (wizard >= 0 && Main.rand.NextBool(8))
            {
                return "Have I ever told you, that I was into dark arts... Probably not. I would perform some twisted necromancy, but I would probably cause chaos...";
            }
            switch (Main.rand.Next(8))
            {
                case 0:
                    return "Some people seem be frightened by my appearence, I don't know why?";
                case 1:
                    return "Hello, I am error!";
                case 2:
                    return "It's 5:00, Somewhere...";
                case 3:
                    return "I know something that can really spice this place up a little bit, but I just won't tell you!";
                case 4:
                    return "If you compare me to a Wraith, then I don't know what's wrong with you...";
                default:
                    return "I may look evil, but I'm actually quite nice!";
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemID.BreakerBlade);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<ScorchedMetal>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.DemonTorch);
            nextSlot++;
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
