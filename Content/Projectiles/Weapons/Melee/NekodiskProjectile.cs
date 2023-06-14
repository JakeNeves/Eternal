using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class NekodiskProjectile : ModProjectile
    {
        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nekodisk");
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;

            var entitySource = Projectile.GetSource_FromAI();

            CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
            int index5 = Projectile.NewProjectile(entitySource, Projectile.Center, CircleDirc, ProjectileID.Meowmere, Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            int index6 = Projectile.NewProjectile(entitySource, Projectile.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ProjectileID.Meowmere, Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            Main.projectile[index5].timeLeft = 300;
            Main.projectile[index6].timeLeft = 300;
        }
    }
}
