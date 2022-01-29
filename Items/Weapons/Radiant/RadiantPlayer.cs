using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace Eternal.Items.Weapons.Radiant
{
    public class RadiantPlayer : ModPlayer
    {
        public static RadiantPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<RadiantPlayer>();
        }

        public float radiantDamageAdd;
        public float radiantDamageMultiply = 1f;
        public float radiantKnockback;
        public int radiantCrit;

        public int etherealPowerCurrent;
        public const int DefaultEtherealPowerMax = 100;
        public int etherealPowerMax;
        public int etherealPowerMax2;
        public float etherealPowerRegenerationRate;
        internal int etherealPowerRegenerationTimer = 0;
        public static readonly Color HealEtherealPower = new Color(74, 190, 247);

        public override void Initialize()
        {
            etherealPowerMax = DefaultEtherealPowerMax;
        }

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            radiantDamageAdd = 0f;
            radiantDamageMultiply = 1f;
            radiantKnockback = 0f;
            radiantCrit = 0;
            etherealPowerRegenerationRate = 1f;
            etherealPowerMax2 = etherealPowerMax;
        }

        public override void PostUpdateMiscEffects()
        {
            UpdateEtherealPower();
        }

        private void UpdateEtherealPower()
        {
            etherealPowerRegenerationTimer++;

            if (etherealPowerRegenerationTimer > 180 * etherealPowerRegenerationRate)
            {
                etherealPowerCurrent += 1;
                etherealPowerRegenerationTimer = 0;
            }

            etherealPowerCurrent = Utils.Clamp(etherealPowerCurrent, 0, etherealPowerMax2);
        }
    }
}
