using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Armor;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Magic;
using Eternal.Content.Projectiles.Weapons.Ranged;
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
    public class Heretic : ModNPC
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
            NPCID.Sets.DangerDetectRange[NPC.type] = 400;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 30;
            NPCID.Sets.AttackAverageChance[NPC.type] = 50;
            NPCID.Sets.ShimmerTownTransform[NPC.type] = true;
            NPCID.Sets.ShimmerTownTransform[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f,
                Direction = 1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<Biomes.Mausoleum>(AffectionLevel.Like)
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
                if (DownedBossSystem.downedGlare)
                    return true;
            }
            return false;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");

            if (EventSystem.isRiftOpen)
                button2 = "Help";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Player player = Main.player[Main.myPlayer];
            if (firstButton)
                shopName = ShopName;
            
            if (EventSystem.darkMoon)
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        Main.npcChatText = "Beware, there could be settlers from the Mausoleum that are hunting you down at this time of night...";
                        break;
                    case 1:
                        Main.npcChatText = "My advice is that you should try your best to stay alive as much as possible, especially when the Dark Moon's curse gets stronger later on!";
                        break;
                    case 2:
                        Main.npcChatText = "When the glow of the Dark Moon's curse shines upon the land, the arcane spirits of the Mausoleum and beyond, make their debut upon the cursed night...";
                        break;
                    case 3:
                        if (!DownedBossSystem.downedTrinity)
                            Main.npcChatText = "This is advice I once told my sons Sodor and Grobittom, without the proper potential, the occult can strike you down no matter what! When you've harness what you believe is the true potential, challenge those above and strengthen the Dark Moons curse. If done correctly, the curse will be strengthened, however only you can ward the Dark Moon's curse away...";
                        else 
                            Main.npcChatText = "The Darks Moon's curse appears to be strengthened. Now, only you can ward the Dark Moon's curse away...";
                        break;
                }
            }
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Judas",
                "Slade",
                "Lambada",
                "Etehyn",
                "Abbos",
                "Sactoth",
                "Ozim",
                "Hennomit",
                "Onan",
                "Zamott"
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
            int wizard = NPC.FindFirstNPC(NPCID.Wizard);
            if (wizard >= 0 && Main.rand.NextBool(18))
            {
                return "I like how " + Main.npc[wizard].GivenName + " has some kind of powers, personally I do too... However, I am afraid that my powers are unbareablely destructive on most occations.";
            }
            int emperor = NPC.FindFirstNPC(ModContent.NPCType<Emperor>());
            if (emperor >= 0 && Main.rand.NextBool(18))
            {
                return "I tried to place a slight curse on that Jake guy as a little joke one time, but it immeadiately wore off thanks to his unpredictable clairvoyance!";
            }
            switch (Main.rand.Next(16))
            {
                case 0:
                    if (player.name == "Sodor" || player.name == "Grobittom")
                        return "Ahh so you're alive and well " + player.name + "... I knew you and I would meet again one day, unless your not really the " + player.name + " I remember, are you?";
                    else
                        return "If only I saw my sons, Sodor and Grobittom alive and well... I guess I'll never see them again, I really wish they never went missing in the first place.";
                case 1:
                    return "Trying to invoke the Dark Moon's cuse, right before you're prepared? Nonsense... I've had people do that a couple of times, and they all died!";
                case 2:
                    return "Now, now... I mustn't be bothered right now, I must preserve the magic of the occult for hundreds of years to come...";
                case 3:
                    return "";
                case 4:
                    if (NPC.IsShimmerVariant)
                        return "Have you ever been over to the Gehenna yet?";
                    else
                        return "Have you ever been over to the Mausoleum yet?";
                case 5:
                    if (ModContent.GetInstance<ZoneSystem>().zoneMausoleum)
                        return "Oh the Mausoleum, almost just like how I remembered it...";
                    else if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
                        return "Ahh, the Gehenna, A place I normally don't visit often! It does tend to carry the heavy torture I've witnessed...";
                    else
                        return "Curses? Torture? I'll show you some places where you can visit for either or... One day...";
                case 6:
                    return "Niades... That dammed construct, I just wish it was ridden from this world for good... However, that might only be just a dream...";
                case 7:
                    return "Me? Dark? Heh heh heh, I'm not even trying to hide it! I could be anyone's dark side if I so choose to be...";
                case 8:
                    return "...";
                case 9:
                    return "Have you ever stumbled upon a giant floating mask with a heart chained to it? I know I have...";
                case 10:
                    return "If it's about clarevoyance, I can't help you with that...";
                case 11:
                    return "If it's about black magic, I'm all in for it!";
                case 12:
                    return "Just a heads up, I could potentally do mostly harm than I can do good...";
                default:
                    return "You got something to say? Spill it!";
            }
        }

        public override void AddShops()
        {
            Player player = Main.player[Main.myPlayer];

            var hereticShop = new NPCShop(Type, ShopName)
                .Add<SpiritRites>([new Condition("Mods.Eternal.Conditions.CosmicApparitionDefeated", () => DownedBossSystem.downedCosmicApparition), new Condition("Mods.Eternal.Conditions.HellMode", () => DifficultySystem.hellMode)])
                .Add<Brimstone>(Condition.IsNpcShimmered)
                .Add<SanguineMask>()
                .Add<SanguineChestplate>()
                .Add<SanguineGreaves>();

            hereticShop.Register();
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
            damage = 80;
            knockback = 2f;
        }

        public void StatueTeleport()
        {
            for (int k = 0; k < 4 + Main.rand.Next(2); k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<MausoleumTorch>());
            }
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 5;
            randExtraCooldown = 5;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<SwiftShotStarbuster>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 2f;
            randomOffset = 1.25f;
        }
    }
}
