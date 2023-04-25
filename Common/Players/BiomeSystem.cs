using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Eternal.Common.Systems;

namespace Eternal.Common.Players
{
    public class BiomeSystem : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            Player.ManageSpecialBiomeVisuals("Eternal:Rift", RiftSystem.isRiftOpen);
        }
    }
}
