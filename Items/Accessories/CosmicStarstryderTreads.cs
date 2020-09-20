using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class CosmicStarstryderTreads : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Counts as Wings" + 
                               "\nAllows flight and slow fall" + 
                               "\nAllows the wearer to run at Ludicrous Speeds!" + 
                               "\nProvides Mobility on ice" + 
                               "\nLava Waders Effects" +
                               "\nTemporary Immunity to lava" +
                               "\nMaster Ninja Gear Effects" +
                               "\n'A good alternative to the Hell Wheel, I susposed...'");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 30;
            item.value = Item.sellPrice(platinum: 5);
            item.rare = ItemRarityID.Red;
            item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if(slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < + maxAccessoryIndex; i++)
                {
                    if (slot != i && 
                        (player.armor[i].type == ItemID.HermesBoots ||
                        player.armor[i].type == ItemID.SpectreBoots ||
                        player.armor[i].type == ItemID.LightningBoots ||
                        player.armor[i].type == ItemID.FrostsparkBoots
                        //player.armor[i].type == ItemID.TerrasparkBoots
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
            if (!modDashPlayer.DashActive)
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
            }
            #endregion

            player.accRunSpeed = 9.8f;
            player.spikedBoots = 2;
            player.rocketBoots = 3;
            player.wingTimeMax = 250;
            player.maxRunSpeed = 20;

            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 840;
            //player.lavaImmune = true;
            player.noFallDmg = true;
            player.blackBelt = true;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.70f;
            ascentWhenRising = 0.18f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 4f;
            constantAscend = 0.140f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 14f;
            acceleration *= 4f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkTeal;
                }
            }
        }

    }
}
