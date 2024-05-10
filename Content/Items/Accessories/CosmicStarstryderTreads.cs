using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class CosmicStarstryderTreads : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(320, 8, 4f);
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 46;
            Item.value = Item.sellPrice(platinum: 5);
            Item.rare = ModContent.RarityType<Teal>();
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
                        player.armor[i].type == ModContent.ItemType<RoyalKeepersTreads>()
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
            player.accRunSpeed = 9f;
            player.rocketBoots = player.vanityRocketBoots = ArmorIDs.RocketBoots.SpectreBoots;
            player.moveSpeed += 0.20f;
            player.iceSkate = true;
            player.desertBoots = true;

            player.jumpBoost = true;
            player.noFallDmg = true;

            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;

            player.blackBelt = true;
            player.spikedBoots = 1;
            player.spikedBoots = 2;

            player.wingTimeMax = 300;

            player.GetModPlayer<DashSystem>().DashAccessoryEquipped = true;

        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.16f;
            ascentWhenRising = 0.16f;
            maxCanAscendMultiplier = 2.2f;
            maxAscentMultiplier = 2.2f;
            constantAscend = 0.112f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 9.5f;
            acceleration *= 2.3f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TerrasparkBoots)
                .AddIngredient(ItemID.LunarBar, 6)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 20)
                .AddIngredient(ModContent.ItemType<StarpowerCrystal>(), 12)
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 40)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
