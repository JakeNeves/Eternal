using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace Eternal.Items.Accessories.Expert
{
    public class FrostKingsCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost King's Core");
            Tooltip.SetDefault("Grants Immunity to Chilled, Frostburn, and Frozen Debuffs"
                                           + "\n17% Extra Melee and Ranged"
                                           + "\nDash Effects"
                                           + "\nEffects of Frost Armor");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
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

            player.meleeDamage += 0.17f;
            player.rangedDamage += 0.17f;

            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;

            player.frostArmor = true;
        }

        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 36;
            item.height = 30;
            item.value = Item.buyPrice(gold: 15);
            item.rare = ItemRarityID.Expert;
            item.expert = true;
        }
    }
}
