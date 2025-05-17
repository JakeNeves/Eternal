using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Common.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Eternal.Content.Items.Misc;
using Eternal.Content.Items.Potions;
using Eternal.Common.Players;
using Terraria.GameContent;

namespace Eternal.Content.NPCs.Town
{
    [AutoloadHead]
    public class Emissary : ModNPC
    {
        public string ShopName = "Shop";

        private static int ShimmerHeadIndex;

        private static Profiles.StackedNPCProfile NPCProfile;

        public override void Load()
        {
            ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmered_Head");
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 600;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.ShimmerTownTransform[NPC.type] = true;
            NPCID.Sets.ShimmerTownTransform[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f,
                Direction = 1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
            ;

            NPCProfile = new Profiles.StackedNPCProfile(
                new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture + "_Alt"),
                new Profiles.DefaultNPCProfile(Texture + "_Shimmered", ShimmerHeadIndex, Texture + "_Shimmered_Alt")
            );
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return NPCProfile;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 35;
            NPC.height = 55;
            NPC.aiStyle = 7;
            NPC.damage = 20;
            NPC.defense = 40;
            NPC.lifeMax = 3000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
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
            button = Language.GetTextValue("LegacyInterface.28");
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<EmperorsTrust>()))
            {
                button2 = "Reputation";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = ShopName;
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<EmperorsTrust>()))
            {
                Player player = Main.player[Main.myPlayer];
                if (ReputationSystem.ReputationPoints > 100)
                {
                    Main.npcChatText = $"Based on your current reputation, our highness has trusted you in our eyes {player.name}.";
                }
                else if (ReputationSystem.ReputationPoints > 500)
                {
                    Main.npcChatText = $"Based on your current reputation, our highness would like to honor you based on how you have left an impact in our hearts {player.name}.";
                }
                else if (ReputationSystem.ReputationPoints > 1000)
                {
                    Main.npcChatText = $"Based on your current reputation, our highness would see you as a hero to us and to our people, please do one day, visit our empire {player.name}.";
                }
                else if (ReputationSystem.ReputationPoints >= 5000)
                {
                    Main.npcChatText = $"Based on your current reputation, our highness shall declare you a saviour to our people. Congratulations {player.name}, you've shown your excellence before our very eyes!";
                }
            }
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Sirus",
                "Alastor",
                "Rupert",
                "Maddox",
                "Corleone",
                "Ivor",
                "Samual",
                "Drew",
                "Dale",
                "Emmanuel",
                "Julian",
                "Roslyn"
            };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				new FlavorTextBestiaryInfoElement("The Emperor's highest representatives of the Gallahard Empire, ordered as the Emperor's messanger he spreads the mighty words of the Emperor's voice."),
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
            }
        }

        public override string GetChat()
        {
            Player player = Main.player[Main.myPlayer];
            int emissary = NPC.FindFirstNPC(ModContent.NPCType<Emissary>());
            int dryad = NPC.FindFirstNPC(NPCID.Dryad);
            if (dryad >= 0 && Main.rand.NextBool(12))
            {
                return "The blessings of " + Main.npc[dryad].GivenName + " almost reminds me of some druids I know of, I am not sure if I remember there names though...";
            }
            switch (Main.rand.Next(11))
            {
                case 0:
                    return "I come from the empire of Gallahard, and I hail to our emperor!";
                case 1:
                    return "I am true to our emperor, we stand tall across the Gallahard Empire...";
                case 2:
                    return "I am " + Main.npc[emissary].GivenName + ", of Galadian blood and to the emperor, I am his Emissary, therefore, I am true to the Gallahard Empire!";
                case 3:
                    return "What do you seek my friend...";
                case 4:
                    return "You would have to know alot of stuff, if you want to be one of our emperor's Emissaries...";
                case 5:
                    return "This nature you have is very otherworldly in my taste!";
                case 6:
                    if (DownedBossSystem.downedCosmicEmperor)
                        return player.name + ", I knew you would be famous one day...";
                    else
                        return player.name + "... I believe you would be famous one day, famous for slaying such great enemies of this world";
                case 7:
                    return "If there was one thing I know that how our emperor likes throw parties sometimes, is with some fresh bread, baked by one of our emperor's favourite emissaries and some fine red wine.";
                case 8:
                    return "Oh... Don't mind me, I am just writing some entries in favour of our emperor.";
                case 9:
                    return "Trying to face great colossal enemies right before you should, what kind of tomfoolery do you even deal with...";
                case 10:
                    if (ReputationSystem.ReputationPoints > 25)
                        return "How can I help you at this time my friend.";
                    else
                        return "Make this quick, I have some erands from our emperor to finish...";
                default:
                    if (ReputationSystem.ReputationPoints > 100)
                        return $"Hello {player.name}, how is business going?";
                    else
                        return "I am a little busy right now, but I don't mind visitors.";
            }
        }

        public override void AddShops()
        {
            Player player = Main.player[Main.myPlayer];

            var emissaryShop = new NPCShop(Type, ShopName);

            emissaryShop.Add<PristineHealingPotion>(new Condition("Mods.Eternal.Conditions.50ReputationPoints", () => ReputationSystem.ReputationPoints == 50));

            emissaryShop.Add<PerfectHealingPotion>(new Condition("Mods.Eternal.Conditions.100ReputationPoints", () => ReputationSystem.ReputationPoints == 100));

            emissaryShop.Register();
        }

        public override void ModifyActiveShop(string shopName, Item[] items)
        {
            foreach (Item item in items)
            {
                if (item == null || item.type == ItemID.None)
                {
                    continue;
                }

                if (NPC.IsShimmerVariant)
                {
                    int value = item.shopCustomPrice ?? item.value;
                    item.shopCustomPrice = value / 2;
                }
            }
        }

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
            Main.NewText("What happened?", 12, 48, 105);
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 10;
            randExtraCooldown = 5;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<StartridentProjectileThrown>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 6.75f;
            randomOffset = 1.5f;
        }

    }
}
