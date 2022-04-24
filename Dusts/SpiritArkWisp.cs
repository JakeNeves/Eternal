using Terraria;
using Terraria.ModLoader;

namespace Eternal.Dusts
{
    public class SpiritArkWisp : ModDust
    {

        public override void OnSpawn(Dust dust)
        {
            dust.velocity.Y = Main.rand.Next(-4, 2) * 0.1f;
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
            Lighting.AddLight(dust.position, 0.87f * strength, 2.55f * strength, 1.62f * strength);
            return false;

        }

    }
}
