using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Dusts
{
    public class OcculticMatter : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= Main.rand.NextFloat(-0.4f, 0.8f);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.scale *= Main.rand.NextFloat(-0.05f, 0.25f);
            dust.noGravity = true;
            dust.velocity.Y = 0.5f;
            dust.alpha++;

            if (dust.scale < -0.25f)
            {
                dust.active = false;
            }

            return false;
        }
    }
}
