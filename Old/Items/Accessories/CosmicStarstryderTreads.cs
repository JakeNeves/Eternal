﻿using Eternal.Items.Materials;
using Eternal.Items.Materials.Elementalblights;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories
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
                               "\n'Ascendance'");
        }

        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 46;
            item.value = Item.sellPrice(platinum: 5);
            item.rare = ItemRarityID.Red;
            item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
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
                        //player.armor[i].type == ItemID.SandBoots ||
                        player.armor[i].type == ItemID.SpectreBoots ||
                        player.armor[i].type == ItemID.LightningBoots ||
                        player.armor[i].type == ItemID.FrostsparkBoots ||
                        //player.armor[i].type == ItemID.TerrasparkBoots ||
                        player.armor[i].type == ModContent.ItemType<BootsofBlindingSpeed>()
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
            EternalPlayer modPlayer = player.GetModPlayer<EternalPlayer>();
            EternalDashPlayer modDashPlayer = player.GetModPlayer<EternalDashPlayer>();

            #region Dash Effect
            /*if (!modDashPlayer.DashActive)
                return;

            player.eocDash = modDashPlayer.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            if (modDashPlayer.DashTimer == EternalDashPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if ((modDashPlayer.DashDir == EternalDashPlayer.DashLeft && player.velocity.X > -modDashPlayer.DashVelocity) || (modDashPlayer.DashDir == EternalDashPlayer.DashRight && player.velocity.X < modDashPlayer.DashVelocity))
                {
                    int dashDirection = modDashPlayer.DashDir == EternalDashPlayer.DashRight ? 1 : -1;
                    newVelocity.X = dashDirection * modDashPlayer.DashVelocity;
                }

                player.velocity = newVelocity;
            }

            modDashPlayer.DashTimer--;
            modDashPlayer.DashDelay--;

            if (modDashPlayer.DashDelay == 0)
            {
                modDashPlayer.DashDelay = EternalDashPlayer.MAX_DASH_DELAY;
                modDashPlayer.DashTimer = EternalDashPlayer.MAX_DASH_TIMER;
                modDashPlayer.DashActive = false;
            }*/
            #endregion

            player.accRunSpeed = 9.0f;
            player.rocketBoots = 4;
            player.moveSpeed += 0.20f;
            player.iceSkate = true;
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<CometiteCrystal>(), 12);
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 40);
            recipe.AddIngredient(ModContent.ItemType<DuskblightCrystal>(), 16);
            recipe.AddIngredient(ItemID.FrostsparkBoots);
            recipe.AddIngredient(ItemID.LavaWaders);
            recipe.AddIngredient(ItemID.MasterNinjaGear);
            recipe.AddIngredient(ItemID.LunarBar, 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}