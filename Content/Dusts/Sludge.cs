using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Dusts
{
    public class Sludge : ModDust
    {
		public override void OnSpawn(Dust dust)
		{
			dust.noLight = true; // Makes the dust emit no light.
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
