using Eternal.Content.NPCs.Boss.AoI;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Boss
{
    public class AoICircle : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 418;
            Projectile.height = 418;

            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.alpha = 80;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
        }

        public override void AI()
        {
            NPC parent = Main.npc[(int)Projectile.ai[1]]; //Main.npc[NPC.FindFirstNPC(ModContent.NPCType<ArkofImperious>())];

            if (!NPC.AnyNPCs(ModContent.NPCType<ArkofImperious>()))
            {
                Projectile.Kill();
            }

            Projectile.Center = parent.Center;

            Projectile.rotation += 0.15f;
        }
    }
}
