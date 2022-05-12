using Eternal.NPCs.Boss.AoI;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    public class AoICircle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 300;
            projectile.height = 300;

            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.alpha = 80;
            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ark of Imperious");
        }

        public override void AI()
        {
            NPC parent = Main.npc[(int)projectile.ai[1]]; //Main.npc[NPC.FindFirstNPC(ModContent.NPCType<ArkofImperious>())];

            if (!NPC.AnyNPCs(ModContent.NPCType<ArkofImperious>()))
            {
                projectile.Kill();
            }

            projectile.Center = parent.Center;

            projectile.rotation += 0.05f;
        }
    }
}
