using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories;
using Eternal.Content.Items.Misc;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.NPCs.Boss.CosmicApparition;
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
            Main.NewText("Thanks for playing the Eternal " + Eternal.instance.Version.ToString() + ", this is a public alpha build, expect things to change overtime." +
                    "\nFor updates, join the Jake's Lounge discord server", 125, 45, 60);
            Main.NewText("https://discord.gg/HUJ8KUSAjC", 100, 0, 210);
        }

        public override void PreUpdate()
        {
            var entitySource = Player.GetSource_NaturalSpawn();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (DifficultySystem.hellMode && ModContent.GetInstance<ServerConfig>().cosmicApparitionNaturalSpawn)
                {
                    if (!DownedBossSystem.downedCosmicApparition && NPC.downedMoonlord && !NPC.AnyNPCs(ModContent.NPCType<CosmicApparition>()))
                    {
                        cosmicApparitionPresence++;
                        switch (cosmicApparitionPresence)
                        {
                            case 4000:
                                Main.NewText("You feel a ghostly figure is following you...", 220, 0, 210);
                                break;
                            case 8000:
                                Main.NewText("Shrieks start to echo around you...", 220, 0, 210);
                                break;
                            case 12000:
                                Main.NewText("A chill goes down your spine as something approaches from a vast distance...", 220, 0, 210);
                                break;
                            case 16000:
                                NPC.NewNPC(entitySource, (int)Player.Center.X, (int)Player.Center.Y - 900, ModContent.NPCType<CosmicApparition>());
                                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/CosmicApparitionAnger"));
                                Main.NewText("A Cosmic Apparition has awoken!", 175, 75, 255);
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
