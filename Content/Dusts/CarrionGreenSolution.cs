using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Dusts
{
    public class CarrionGreenSolution : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.2f;
            dust.noLight = false;
        }

        public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.99f;

			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}

			return false;
		}
	}
}
