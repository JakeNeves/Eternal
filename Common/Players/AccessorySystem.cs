using Eternal.Content.Projectiles.Accessories;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Players
{
    public class AccessorySystem : ModPlayer
    {
        public static bool Dreadheart = false;
        public static bool Bloodtooth = false;

        public override void ResetEffects()
        {
            Dreadheart = false;
            Bloodtooth = false;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            var entitySource = Entity.GetSource_None();

            if (Dreadheart)
            {
                Player.HealEffect(Main.rand.Next(6, 12), false);
            }

            if (Bloodtooth)
            {
                SoundEngine.PlaySound(SoundID.DD2_OgreSpit, Player.Center);
                for (int i = 0; i < Main.rand.Next(4, 8); i++)
                    Projectile.NewProjectile(entitySource, Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<BloodtoothProjectile>(), (int)damage, 0f, Main.myPlayer);
            }
        }
    }
}
