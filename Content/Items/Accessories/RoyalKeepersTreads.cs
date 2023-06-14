using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class RoyalKeepersTreads : ModItem
    {
        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("Royal Keeper's Treads");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Counts as Wings" +
                                                                            "\nAllows flight and slow fall" +
                                                                            "\nPress Down to toggle hover" +
                                                                            "\nPress Up to deactivate hover" +
                                                                            "\nAllows the wearer to run at crazy speed!" +
                                                                            "\nProvides mobility on ice" +
                                                                            "\nLava Waders effects" +
                                                                            "\nMaster Ninja Gear effects" +
                                                                            "\nImmunity to lava" +
                                                                            "\n'The Cosmic Emperor's Reward for finding such incredibly rare alloy'");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(720, 18, 4f, true, 1, 1.5f);
        }

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 38;
            Item.value = Item.sellPrice(platinum: 10);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < +maxAccessoryIndex; i++)
                {
                    if (slot != i &&
                        (player.armor[i].type == ItemID.HermesBoots ||
                        player.armor[i].type == ItemID.RocketBoots ||
                        player.armor[i].type == ItemID.FlurryBoots ||
                        player.armor[i].type == ItemID.SailfishBoots ||
                        player.armor[i].type == ItemID.SandBoots ||
                        player.armor[i].type == ItemID.SpectreBoots ||
                        player.armor[i].type == ItemID.LightningBoots ||
                        player.armor[i].type == ItemID.FrostsparkBoots ||
                        player.armor[i].type == ItemID.TerrasparkBoots ||
                        player.armor[i].type == ModContent.ItemType<CosmicStarstryderTreads>()
                        ))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.accRunSpeed = 18f;
            player.rocketBoots = player.vanityRocketBoots = ArmorIDs.RocketBoots.TerrasparkBoots;
            player.moveSpeed += 0.40f;
            player.iceSkate = true;
            player.desertBoots = true;

            player.jumpBoost = true;
            player.noFallDmg = true;

            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;

            player.dash = 1;

            player.blackBelt = true;
            player.spikedBoots = 1;
            player.spikedBoots = 2;

            player.wingTimeMax = 600;
        }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.16f;
            ascentWhenRising = 0.18f;
            maxCanAscendMultiplier = 2.2f;
            maxAscentMultiplier = 3.5f;
            constantAscend = 0.120f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 16.5f;
            acceleration *= 9.2f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmicStarstryderTreads>())
                .AddIngredient(ModContent.ItemType<CosmicEmperorsInterstellarAlloy>())
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
