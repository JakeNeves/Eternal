using Terraria;
using Terraria.ModLoader;

namespace Eternal.Dusts
{
    public class DoomFire : ModDust
    {

        public override void OnSpawn(Dust dust)
        {
            dust.velocity.Y = Main.rand.Next(-8, 4) * 0.1f;
            dust.noGravity = true;
            dust.velocity.X *= 0.2f;
            dust.scale *= 1.4f;
        }

        public override bool MidUpdate(Dust dust)
        {

            if (!dust.noGravity)
            {
                dust.velocity.Y += 0.04f;
            }

            if (dust.noLight)
            {
                return false;
            }

            float strength = dust.scale * 1.2f;
            if (strength > 1f)
            {
                strength = 1f;
            }
            Lighting.AddLight(dust.position, 0.8f * strength, 0.0f * strength, 0.6f * strength);
            return false;

        }

    }
}
