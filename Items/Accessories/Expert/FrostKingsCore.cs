using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Eternal.Projectiles.Accessories;

namespace Eternal.Items.Accessories.Expert
{
    public class FrostKingsCore : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Frost King's Core");
            Tooltip.SetDefault("Creates a crystal ice barrier around the player"
                                           + "\n17% extra melee and ranged damage"
                                           + "\nDash effects");
        }

        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 26;
            item.height = 46;
            item.value = Item.buyPrice(gold: 15);
            item.rare = ItemRarityID.Expert;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.17f;
            player.rangedDamage += 0.17f;

            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;

            EternalPlayer.frostKingsCore = true;

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

        }

    }
}
