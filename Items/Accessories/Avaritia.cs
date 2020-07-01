using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace Eternal.Items.Accessories
{
    class Avaritia : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants immunity to a majority of debuffs \nA mere fragment of The Cosmic Tyriant, JakeTEM. It was given to him as a gift from the cosmic gods \n'It was an honor serving my empire, which no longer exsists, but however... I have transcended the cosmis plain and reveived the power of Avaritia...' \nThis divine artifact was proven to make the tyriant a god in any way, shape or form... \n'We had a fair battle between you and I...' \nMaybe Jake knows about this artifact, but we may never know, once in for all...");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 14));
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = -12;
            item.accessory = true;
            item.value = Item.buyPrice(platinum: 5, gold: 15);
            item.lifeRegen = 100;
            item.expert = true;
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //Speed
            player.moveSpeed = 50.5f;

            //Damage Boost
            player.meleeDamage *= 75f;
            player.rangedDamage *= 75f;
            player.magicDamage *= 75f;
            player.minionDamage *= 75f;

            //Buff Immunity
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Midas] = true;
            player.buffImmune[BuffID.Wet] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Horrified] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.MoonLeech] = true;

            //Dash
            EternalDashPlayer mp = player.GetModPlayer<EternalDashPlayer>();

            if (!mp.DashActive)
                return;

            player.eocDash = mp.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            if (mp.DashTimer == EternalDashPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if ((mp.DashDir == EternalDashPlayer.DashUp && player.velocity.Y > -mp.DashVelocity) || (mp.DashDir == EternalDashPlayer.DashDown && player.velocity.Y < mp.DashVelocity))
                {
                    float dashDirection = mp.DashDir == EternalDashPlayer.DashDown ? 1 : -1.3f;
                    newVelocity.Y = dashDirection * mp.DashVelocity;
                }
                else if ((mp.DashDir == EternalDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity) || (mp.DashDir == EternalDashPlayer.DashRight && player.velocity.X < mp.DashVelocity))
                {
                    int dashDirection = mp.DashDir == EternalDashPlayer.DashRight ? 1 : -1;
                    newVelocity.X = dashDirection * mp.DashVelocity;
                }

                player.velocity = newVelocity;
            }

            mp.DashTimer--;
            mp.DashDelay--;

            if (mp.DashDelay == 0)
            {
                mp.DashDelay = EternalDashPlayer.MAX_DASH_DELAY;
                mp.DashTimer = EternalDashPlayer.MAX_DASH_TIMER;
                mp.DashActive = false;
            }
        }
    }

}