using Terraria;
using Terraria.ModLoader;

namespace Eternal.Dusts
{
    public class ArkEnergy : ModDust
    {

        public override void OnSpawn(Dust dust)
        {
            dust.velocity.Y = Main.rand.Next(-4, 2) * 0.1f;
            dust.velocity.X *= 0.2f;
            dust.scale *= 1.4f;
            dust.noGravity = false;
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
            Lighting.AddLight(dust.position, 0.26f * strength, 2.19f * strength, 0.84f * strength);
            return false;

        }

    }
}
