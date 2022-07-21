using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
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
            Tooltip.SetDefault("Counts as Wings" +
                               "\nAllows flight and slow fall" +
                               "\nAllows the wearer to run at ludicrous speed!" +
                               "\nProvides mobility on ice" +
                               "\nLava Waders effects" +
                               "\nMaster Ninja Gear effects" +
                               "\nImmunity to lava" +
                               "\nDouble tap a direction to dash, even vertically" +
                               "\n'Ascendance beyond your comprehension!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(360, 16, 4.6f);
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
                        player.armor[i].type == ItemID.TerrasparkBoots
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
            player.accRunSpeed = 9.75f;
            player.rocketBoots = player.vanityRocketBoots = ArmorIDs.RocketBoots.SpectreBoots;
            player.moveSpeed += 0.20f;
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

            player.wingTimeMax = 300;

            CosmicStarstryderTreadsPlayer modDashPlayer = player.GetModPlayer<CosmicStarstryderTreadsPlayer>();

            #region Dash Effect
            if (!modDashPlayer.DashActive)
                return;

            player.eocDash = modDashPlayer.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            if (modDashPlayer.DashTimer == CosmicStarstryderTreadsPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if ((modDashPlayer.DashDir == CosmicStarstryderTreadsPlayer.DashUp && player.velocity.Y > -modDashPlayer.DashVelocity) || (modDashPlayer.DashDir == CosmicStarstryderTreadsPlayer.DashDown && player.velocity.Y < modDashPlayer.DashVelocity))
                {
                    float dashDirection = modDashPlayer.DashDir == CosmicStarstryderTreadsPlayer.DashDown ? 1 : -1.3f;
                    newVelocity.Y = dashDirection * modDashPlayer.DashVelocity;
                }
                else if ((modDashPlayer.DashDir == CosmicStarstryderTreadsPlayer.DashLeft && player.velocity.X > -modDashPlayer.DashVelocity) || (modDashPlayer.DashDir == CosmicStarstryderTreadsPlayer.DashRight && player.velocity.X < modDashPlayer.DashVelocity))
                {
                    int dashDirection = modDashPlayer.DashDir == CosmicStarstryderTreadsPlayer.DashRight ? 1 : -1;
                    newVelocity.X = dashDirection * modDashPlayer.DashVelocity;
                }

                player.velocity = newVelocity;
            }

            modDashPlayer.DashTimer--;
            modDashPlayer.DashDelay--;

            if (modDashPlayer.DashDelay == 0)
            {
                modDashPlayer.DashDelay = CosmicStarstryderTreadsPlayer.MAX_DASH_DELAY;
                modDashPlayer.DashTimer = CosmicStarstryderTreadsPlayer.MAX_DASH_TIMER;
                modDashPlayer.DashActive = false;
            }
            modDashPlayer.DashActive = false;
            #endregion
        }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.32f;
            ascentWhenRising = 0.32f;
            maxCanAscendMultiplier = 2.2f;
            maxAscentMultiplier = 4.2f;
            constantAscend = 0.136f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 18.5f;
            acceleration *= 4.6f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TerrasparkBoots)
                .AddIngredient(ItemID.MasterNinjaGear)
                .AddIngredient(ItemID.LunarBar, 6)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 20)
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 12)
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 40)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
