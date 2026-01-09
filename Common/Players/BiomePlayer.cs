using Eternal.Common.Systems;
using Eternal.Content.Biomes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Players
{
    public class BiomePlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (Player.InModBiome<Beneath>() && !Player.ZoneUnderworldHeight)
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
