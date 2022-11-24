using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Eternal.Common.Players
{
    public class BuffSystem : ModPlayer
    {
        public bool holyMantle = false;
        public bool chaosCardCooldown = false;

        public bool apparitionalWither = false;

        public override void ResetEffects()
        {
            holyMantle = false;
            chaosCardCooldown = false;
        }

        public override void UpdateDead()
        {
            apparitionalWither = false;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (holyMantle)
            {
                Player.endurance += 10f;

                SoundEngine.PlaySound(SoundID.NPCDeath14, Player.position);

                for (int i = 0; i < 25; i++)
                {
                    Vector2 position = Player.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(Player.position, DustID.BlueTorch);
                    dust.noGravity = false;
                    dust.velocity = Vector2.Normalize(position - Player.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                Player.ClearBuff(ModContent.BuffType<HolyMantle>());
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (apparitionalWither)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 25;

                if (Player.statLifeMax < 0)
                {
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "'s body withered away"), 10000, 1, false);
                }
            }
        }
    }
}
