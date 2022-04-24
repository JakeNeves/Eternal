using Eternal.NPCs.Boss.BionicBosses;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    public class OrionNeoxCircle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 350;
            projectile.height = 350;

            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EXR-2308 Orion-N30X");
        }

        public override void AI()
        {
            NPC parent = Main.npc[(int)projectile.ai[1]]; //Main.npc[NPC.FindFirstNPC(ModContent.NPCType<OrionNeox>())];

            if (!NPC.AnyNPCs(ModContent.NPCType<OrionNeox>()))
            {
                projectile.Kill();
            }

            projectile.Center = parent.Center;

            projectile.rotation -= parent.velocity.X * 0.1f;

            //projectile.rotation += 0.05f;
        }
    }
}
