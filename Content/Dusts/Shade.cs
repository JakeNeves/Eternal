using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Dusts
{
    public class Shade : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.2f;
            dust.noLight = true;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.10f;
            dust.scale *= 0.99f;
            dust.noGravity = true;

            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }

            return false;
        }
    }
}
