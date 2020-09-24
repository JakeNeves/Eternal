using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Eternal.Dusts
{
    public class Tritalodium : ModDust
    {

        public override void OnSpawn(Dust dust)
        {
            dust.scale *= 0.25f;
            dust.noGravity = true;
        }

        public override bool Update(Dust dust)
        {
            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}
