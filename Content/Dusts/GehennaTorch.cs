using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Dusts
{
    public class GehennaTorch : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.2f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.30f;
            dust.scale *= 0.9f;
            dust.noGravity = true;

            if (dust.scale < 0.25f)
            {
                dust.active = false;
            }

            return false;
        }
    }
}
