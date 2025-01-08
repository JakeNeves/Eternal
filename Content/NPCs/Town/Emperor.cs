using Eternal.Common.Players;
using Eternal.Common.Systems;
using Eternal.Content.Items.Misc;
using Eternal.Content.Items.Placeable.Paintings;
using Eternal.Content.Items.Potions;
using Eternal.Content.Items.Summon;
using Eternal.Content.Items.Vanity;
using Eternal.Content.Items.Weapons.Throwing;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Town
{
    [AutoloadHead]
    public class Emperor : ModNPC
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
                .SetNPCAffection(ModContent.NPCType<Emissary>(), AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate)
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
            NPC.defense = 60;
            NPC.lifeMax = 60000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.DD2_GhastlyGlaivePierce;
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
                if (NPC.downedMoonlord || SeedSystem.emperorSeed || Main.zenithWorld)
                    return true;
            }
            return false;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<CosmicTablet>()))
            {
                button2 = "The Cosmic Emperor";
            }
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<LetterofRecommendation>()))
            {
                button2 = "Letter of Recommendation";
            }
            if (EventSystem.isRiftOpen)
            {
                button2 = "Help";
            }
        }

        public override bool CheckDead()
        {
            int num = NPC.life > 0 ? 1 : 10;
            int emperor = NPC.FindFirstNPC(ModContent.NPCType<Emperor>());
            
            Main.NewText(Main.npc[emperor].GivenName + " the Emperor has returned to his world...", 255, 0, 0);
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BlueTorch);
            }
            NPC.life = 1;
            NPC.active = false;

            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);

            return false;
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Player player = Main.player[Main.myPlayer];
            if (firstButton)
                shopName = ShopName;
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<CosmicTablet>()))
            {
                if (!DownedBossSystem.downedArkofImperious)
                    Main.npcChatText = "Talk to me later for information about the Cosmic Emperor";
                else
                {
                    if (!DownedBossSystem.downedCosmicEmperor)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 0:
                                Main.npcChatText = "You're here to talk about the Cosmic Emperor? Great, he needs to be overthrown, however it won't be easy fighting against someone as powerful as him. Get yourself some better armor and such before we call to action.";
                                break;
                            case 1:
                                if (NPC.AnyNPCs(ModContent.NPCType<Emissary>()))
                                    Main.npcChatText = "We must confront the Cosmic Emperor for his doings, he can't go any further. " + player.name + ", I want you to lead us into battle, together we can overthrow and defeat him.";
                                else
                                    Main.npcChatText = "We must confront the Cosmic Emperor for his doings, he can't go any further. " + player.name + ", I want you to lead me into battle, together we can overthrow and defeat him.";
                                break;
                            case 2:
                                Main.npcChatText = player.name + ", I am counting on you to lead me into battle to help overthrow and defeat the Cosmic Emperor, once in for all. It won't be easy trying to fight him, you will need a stronger arsenal to fight him!";
                                break;
                            case 3:
                                Main.npcChatText = "The Cosmic Emperor needs to be overthrown, however you can't fight him alone, join me and together we can defeat him!";
                                break;
                        }
                    }
                    else
                    {
                        Main.npcChatText = player.name + ", I honor you for all of your hard work, therefore you shall be titled 'Minister' for successfully defeating him and leading us to victory.";
                    }
                }
            }
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<LetterofRecommendation>()))
            {
                Main.npcChatText = "A Letter of Recommendation? I must say that you my friend, should happily have this gift to honor your saviour rank as well as great reputation and other great things you have done since I have first arrived here in your world, although I wish to return to my world however, I think I'll stay here for the time being!";
            }
            else if (EventSystem.isRiftOpen)
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        Main.npcChatText = "Whatever situation this is, I can't guide you through this otherworldly reign of chaos!";
                        break;
                    case 1:
                        Main.npcChatText = "What you used to turn this world into an apocalypse, is highly dangerous! I suggest you turn back or face the consinquences within the altered reality...";
                        break;
                    case 2:
                        Main.npcChatText = "Are you sure you're prepared for this kind of mess? Let me tell you right now, you're on your own! No help, no nonthing... I am very concerned about this shifted reign of terror!";
                        break;
                    case 3:
                        Main.npcChatText = "From my point of view, It's best to take as much action as possible to insure everyone is safe within their household, it's simply too dangerous for everyone! Let's hope you have some sort of wits to survive this nightmare...";
                        break;
                }
            }
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Jake"
            };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Hier to the throne of Gallahard, he is the highest of the Gallahard Empire's monarchy. With his trust, he will call upon one of his emissaries to settle the very land."),
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
            if (EventSystem.isRiftOpen)
            {
                switch (Main.rand.Next(8))
                {
                    case 0:
                        return "I don't know what kind of item you have in your pocket that turned the world upside down!";
                    case 1:
                        return "Talk to me later when the world goes back to normal.";
                    case 2:
                        return "I am starting to see some distorted figures around here...";
                    case 3:
                        return "Do I look like I have a general idea on what you've done here? I don't want to have to see this world shift into a state of pure chaos!";
                    case 4:
                        return "Please tell me you're going end all of this...";
                    case 5:
                        return "Welp... If anything strange and destructive approaches here, we're dead!";
                    case 6:
                        return "I don't know if it's just me, but I am starting to see some warping within the fabric of reality...";
                    default:
                        return "You opened a rift? YOU, opened a RIFT!?";
                }
            }
            else
            {
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
                    return "I like parties and all, but some people like me, perfer to have there party like a late-night sit back and relax kinda situation...";
                }
                int merchant = NPC.FindFirstNPC(NPCID.Merchant);
                if (merchant >= 0 && Main.rand.NextBool(18))
                {
                    return "That " + Main.npc[merchant].GivenName + " guy, I don't know about you, but he likes to sell angel statues... Everyone knows they do nothing!";
                }
                switch (Main.rand.Next(16))
                {
                    case 0:
                        if (player.name == "Jake")
                            return "So you must be that " + player.name + " guy everyone in Gallahard is talking about! You should come to a Jake meetup one day...";
                        else
                            return "What am I even doing here?";
                    case 1:
                        return "I suppose this place feel really wacky to me.";
                    case 2:
                        return "I rule an empire, that's all I do...";
                    case 3:
                        return "In case if your curious, how I like to throw parties sometimes, is with some fresh bread, baked by one of my favourite emissaries and some fine red wine.";
                    case 4:
                        if (NPC.IsShimmerVariant)
                            return "How do you like my new uniform? I made it so I can really stand out in front of everyone and prove that I am an emperor from another world!";
                        else
                            return "I have a another uniform I wear, this is what I normally wear wherever I go.";
                    case 5:
                        if (player.ZoneSnow)
                            return "Wow, this winter wonderland is just like the same winter feeling I get over in the Gallahard Empire!";
                        else
                            return "Honestly, I feel that this place could use some winter wonders...";
                    case 6:
                        if (player.ZoneUnderworldHeight || player.ZoneDesert)
                            return "Why is it so hot here, I wasn't built for places like this!";
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
        }

        public override void AddShops()
        {
            Player player = Main.player[Main.myPlayer];

            var emperorShop = new NPCShop(Type, ShopName)
                .Add<RoyalGaladianBread>()
                .Add<FineRedWine>()
                .Add<TheCherishedKinofGallahard>()
                .Add<TheSonofTheArsGaladia>(Condition.IsNpcShimmered)
                .Add<EmperorsTrust>(new Condition("Mods.Eternal.Conditions.CosmicApparitionDefeated", () => DownedBossSystem.downedCosmicApparition))
                .Add<PocketJake>(new Condition("Mods.Eternal.Conditions.IsNamedJake", () => player.name == "Jake" || player.name == "JakeTEM"))
                .Add<JakesHat>()
                .Add<JakesTunic>()
                .Add<JakesPants>();

            emperorShop.Register();
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
            CombatText.NewText(NPC.Hitbox, new Color(24, 96, 210), "Woah, what happened?", dramatic: true);
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 10;
            randExtraCooldown = 5;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<PocketJakeProjectile>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 6.75f;
            randomOffset = 1.5f;
        }
    }
}
