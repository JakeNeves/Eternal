using Eternal.Content.NPCs.Boss.NeoxMechs;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Boss
{
    public class OrionNeoxCircle : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 216;
            Projectile.height = 216;

            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orion N30X");
        }

        public override void AI()
        {
            NPC parent = Main.npc[(int)Projectile.ai[1]]; //Main.npc[NPC.FindFirstNPC(ModContent.NPCType<OrionNeox>())];

            if (!NPC.AnyNPCs(ModContent.NPCType<Orion>()))
            {
                Projectile.Kill();
            }

            Projectile.Center = parent.Center;

            Projectile.spriteDirection = Projectile.direction;

            Projectile.rotation -= parent.velocity.X * 0.1f;

            //projectile.rotation += 0.05f;
        }
    }
}
