using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;
using Eternal.Tiles;
using System.Linq;
using Eternal.Items.Weapons.Ranged;

namespace Eternal.NPCs.Boss.Incinerius
{
    public class RollingFire : ModNPC
    {

        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Incineration Stone");
        }

        public override void SetDefaults()
        {
            npc.width = 58;
            npc.height = 58;
            npc.damage = 100;
            npc.lifeMax = 200;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCHit3;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.value = 50f;
            npc.aiStyle = 5;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Electrified] = true;
        }

        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            npc.rotation += npc.velocity.X * 0.1f;
        }

    }
}
