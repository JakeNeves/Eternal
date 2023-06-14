using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Accessories;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Players
{
    public class BiomePlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneBeneath)
            {
                Player.AddBuff(BuffID.Obstructed, 1, true);

                if (DifficultySystem.hellMode)
                {
                    Player.AddBuff(BuffID.Darkness, 1, true);
                    Player.AddBuff(BuffID.Blackout, 1, true);
                }
            }
        }
    }
}
