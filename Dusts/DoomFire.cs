using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Dusts
{
    public class DoomFire : ModDust
    {

		public override void OnSpawn(Dust dust)
		{
			dust.velocity.Y = Main.rand.Next(-8, 4) * 0.1f;
			dust.velocity.X *= 0.2f;
			dust.scale *= 0.4f;
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

            float strength = dust.scale * 1.4f;
            if (strength > 1f)
            {
                strength = 1f;
            }
            Lighting.AddLight(dust.position, 0.8f * strength, 0.4f * strength, 0.2f * strength);
            return false;

        }

    }
}
