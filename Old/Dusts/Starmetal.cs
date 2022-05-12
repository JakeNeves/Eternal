using Terraria;
using Terraria.ModLoader;

namespace Eternal.Dusts
{
    public class Starmetal : ModDust
    {

        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.4f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale *= 1.5f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.99f;
            float light = 0.35f * dust.scale;
            Lighting.AddLight(dust.position, 1.98f, 0.49f, 2.47f);
            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }
            return false;
        }

        //public override Color? GetAlpha(Dust dust, Color lightColor)
        //	=> new Color(50, 5, 50);


    }
}
