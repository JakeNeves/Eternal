using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Eternal.Content.Dusts;

namespace Eternal.Common.Players
{
    public class BuffSystem : ModPlayer
    {
        public bool holyMantle = false;

        public bool apparitionalWither = false;
        public bool unstableState = false;

        public override void ResetEffects()
        {
            holyMantle = false;
            unstableState = false;
        }

        public override void UpdateDead()
        {
            apparitionalWither = false;
            unstableState = false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (holyMantle)
            {
                if (!Main.dedServ)
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

        public override void PostUpdateBuffs()
        {
            if (holyMantle)
            {
                Player.endurance += 15f;
            }

            if (unstableState)
            {
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.PurpleCrystalShard, 0.5f, 0.5f, 0, Color.White, Main.rand.NextFloat(0.25f, 1f));
            }

            if (apparitionalWither)
            {
                Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<CosmicSpirit>(), 0.5f, 0.5f, 0, Color.White, Main.rand.NextFloat(0.5f, 1.5f));
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (apparitionalWither)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 15;

                if (Player.statLifeMax < 2)
                {
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "'s body withered away"), 10000, 1, false);
                }
            }
        }
    }
}
