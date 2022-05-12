using Eternal.NPCs.Boss.BionicBosses;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    public class OrionCircle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 246;
            projectile.height = 246;

            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XR-2008 Orion-X5");
        }

        public override void AI()
        {
            NPC parent = Main.npc[(int)projectile.ai[1]]; //Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Orion>())];

            if (!NPC.AnyNPCs(ModContent.NPCType<Orion>()))
            {
                projectile.Kill();
            }

            projectile.Center = parent.Center;

            projectile.rotation -= parent.velocity.X * 0.1f;

            //projectile.rotation += 0.05f;
        }
    }
}
