using Eternal.Content.Buffs.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class MrFishboneProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
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
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            var entitySource = Projectile.GetSource_FromAI();

            if (Main.rand.NextBool(8) && player.statLifeMax >= player.statLifeMax2 / 2)
                player.AddBuff(ModContent.BuffType<MrFishbonesBoon>(), 960);

            if (!Main.dedServ) {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/MrFishboneHit")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(1f, 1.25f),
                    MaxInstances = 0,
                }, target.position);

                for (int i = 0; i < 1 + Main.rand.Next(5); i++) {
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4)), ModContent.ProjectileType<MrFishboneBone>(), Projectile.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }
}
