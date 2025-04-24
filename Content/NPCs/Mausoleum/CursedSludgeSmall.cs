using Eternal.Common.Misc;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Mausoleum
{
    public class CursedSludgeSmall : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 16;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.BlueSlime;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.lifeMax = 1000;
            AnimationType = NPCID.BlueSlime;
            NPC.value = Item.sellPrice(platinum: 1);
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Suffocation] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.BetsysCurse] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Mausoleum>().Type ];
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.TargetClosest();
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.dedServ)
                return;

            var entitySource = NPC.GetSource_Death();

            for (int k = 0; k < 15.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.MausoleumTorch>(), 0, 0, 0, default(Color), 1f);
            }
        }
    }
}
