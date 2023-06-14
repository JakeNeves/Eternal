using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Terraria.DataStructures;
using Eternal.Content.Buffs;

namespace Eternal.Common.Players
{
    public class BrutalHellModeSystem : ModPlayer
    {
        public bool hyperthermia = false;
        public bool hypothermia = false;

        public override void ResetEffects()
        {
            hyperthermia = false;
            hypothermia = false;
        }

        public override void PostUpdate()
        {
            if (DifficultySystem.hellMode && ModContent.GetInstance<ServerConfig>().brutalHellMode)
            {
                if (Player.ZoneDesert && Main.dayTime)
                {
                    Player.AddBuff(ModContent.BuffType<Hyperthermia>(), 1, false);
                }
                else if (Player.ZoneDesert && !Main.dayTime)
                {
                    Player.AddBuff(ModContent.BuffType<Hypothermia>(), 1, false);
                }

                if (Player.ZoneUnderworldHeight)
                {
                    Player.AddBuff(ModContent.BuffType<Hyperthermia>(), 1, false);
                }

                if (Player.ZoneSnow)
                {
                    Player.AddBuff(ModContent.BuffType<Hypothermia>(), 1, false);
                }

                if (Player.ZoneNormalSpace)
                {
                    Player.AddBuff(ModContent.BuffType<Hypothermia>(), 1, false);
                }
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (hyperthermia)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 10;

                if (Player.statLifeMax < 1)
                {
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " was dehydrated"), 10000, 1, false);
                }
            }

            if (hypothermia)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 5;

                if (Player.statLifeMax < 1)
                {
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " froze to death"), 10000, 1, false);
                }
            }
        }
    }
}
