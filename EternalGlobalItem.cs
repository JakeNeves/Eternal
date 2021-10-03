using Eternal.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
    public class EternalGlobalItem : GlobalItem
    {
        public override void MeleeEffects(Item item, Player player, Rectangle hitbox)
        {
            if (EternalGlobalProjectile.emperorsGift)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<EmperorFire>());
                }
            }
        }
    }
}
