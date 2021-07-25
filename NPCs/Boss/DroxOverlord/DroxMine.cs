using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Eternal.NPCs.Boss.DroxOverlord
{
    public class DroxMine : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drox Mine");
        }

        public override void SetDefaults()
        {
            npc.width = 58;
            npc.height = 58;
            npc.damage = 100;
            npc.defense = 25;
            npc.lifeMax = 25;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.DD2_ExplosiveTrapExplode;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.value = 50f;
            npc.aiStyle = -1;
            npc.knockBackResist = -1f;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Electrified] = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width / 2, npc.height / 2, 6, 0f, 0f, 5, default(Color), 0.5f);
                    Main.dust[dustIndex].velocity *= 0.5f;
                }
                for (int i = 0; i < 25; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
            }
        }

    }
}
