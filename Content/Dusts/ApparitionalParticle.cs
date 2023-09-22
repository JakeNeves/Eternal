using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Dusts
{
    public class ApparitionalParticle : ModDust
    {
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.4f;
			dust.scale = 0.25f;
			dust.noLight = true;
            dust.frame = new Rectangle(0, 0, 32, 32);
        }

        public override bool Update(Dust dust)
		{
			dust.velocity.Y -= 1.5f;
			dust.alpha += 25;
			dust.scale -= 0.5f / 60f;

			return false;
		}
	}
}
