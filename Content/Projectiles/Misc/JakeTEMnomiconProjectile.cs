using Eternal.Content.NPCs.Boss.AoI;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Boss.DuneGolem;
using Eternal.Content.NPCs.Boss.Niades;
using Eternal.Content.NPCs.Boss.Trinity;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public class JakeTEMnomiconProjectile : ModProjectile
    {
        private static readonly int[] PossibleBosses = {
            ModContent.NPCType<CarminiteAmalgamation>(),
            ModContent.NPCType<DuneGolem>(),
            ModContent.NPCType<Niades>(),
            ModContent.NPCType<CosmicApparition>(),
            ModContent.NPCType<ArkofImperious>(),
            ModContent.NPCType<TrinityCore>()
        };

        private int BossIndex;

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 40;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.timeLeft = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            BossIndex = (byte)Main.rand.Next(PossibleBosses.Length);
        }

        public override void OnKill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            if (!Main.dedServ)
            {
                for (int i = 0; i < Main.rand.Next(1, 7); i++)
                {
                    int boss = NPC.NewNPC(entitySource, (int)Projectile.Center.X + Main.rand.Next(-100, 100), (int)Projectile.Center.Y + Main.rand.Next(-100, 100), PossibleBosses[BossIndex]);
                    Main.npc[boss].boss = false;
                }
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)BossIndex);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            BossIndex = reader.ReadByte();
        }
    }
}
