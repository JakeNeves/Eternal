using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories;
using Eternal.Content.Items.Misc;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Boss.Igneopede;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Players
{
    public class PassiveEventSystem : ModPlayer
    {
        public int cosmicApparitionPresence = 0;

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (Player.name == "Jake" || Player.name == "JakeTEM")
            {
                return new[]
                {
                    new Item(ModContent.ItemType<AspectofTheJake>())
                };
            }

            return new[] {
                new Item(ModContent.ItemType<DeathCharm>()),
                new Item(ModContent.ItemType<AncientEmblem>())
            };
        }

        public override void OnEnterWorld()
        {
            if (ClientConfig.instance.showWelcomeMessage)
            {
                Main.NewText("Thanks for playing the Eternal " + Eternal.Instance.Version.ToString() + "!" +
                        "\nFor updates, join the Jake's Lounge discord server.", 235, 40, 170);
                Main.NewText("https://discord.gg/HUJ8KUSAjC", 8, 147, 207);
                Main.NewText("Be sure to check out the Eternal mod wiki (WIP) too!", 235, 40, 170);
                Main.NewText("https://terrariamods.wiki.gg/wiki/Eternal", 8, 147, 207);
                Main.NewText("Please note that this mod is a work in progress, so bugs may occur!" + 
                        "\nMake sure you backup your world every now and then, especially when using the mod's experimental features." +
                        "\nThese messages can be disabled in the mod's configs.", 255, 223, 64);
            }
        }

        public override void PreUpdate()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (DifficultySystem.hellMode && ServerConfig.instance.cosmicApparitionNaturalSpawn)
                {
                    if (!DownedBossSystem.downedCosmicApparition && NPC.downedMoonlord && !NPC.AnyNPCs(ModContent.NPCType<CosmicApparition>()))
                    {
                        cosmicApparitionPresence++;
                        switch (cosmicApparitionPresence)
                        {
                            case 4500:
                                Main.NewText("You feel a ghostly figure following you...", 220, 0, 210);
                                break;
                            case 6000:
                                Main.NewText("Shrieks start to echo faintly around you...", 220, 0, 210);
                                break;
                            case 7500:
                                Main.NewText("A chill goes down your spine as something approaches from a vast distance...", 220, 0, 210);
                                break;
                            case 9000:
                                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/CosmicApparitionAnger"));

                                if (!Main.dedServ)
                                    NPC.SpawnOnPlayer(Player.whoAmI, ModContent.NPCType<CosmicApparition>());
                                else
                                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: Player.whoAmI, number2: ModContent.NPCType<CosmicApparition>());

                                // Main.NewText("A Cosmic Apparition has awoken!", 175, 75, 255);
                                cosmicApparitionPresence = 0;
                                break;
                        }
                    }
                    else
                    {
                        cosmicApparitionPresence = 0;
                    }
                }
                else
                {
                    cosmicApparitionPresence = 0;
                }
            }
        }
    }
}
