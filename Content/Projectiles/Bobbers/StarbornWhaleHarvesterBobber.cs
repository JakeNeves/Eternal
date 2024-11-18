using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Bobbers
{
    public class StarbornWhaleHarvesterBobber : ModProjectile
    {
		public static readonly Color[] PossibleLineColors = new Color[] {
			new Color(96, 33, 99),
			new Color(110, 33, 183),
			new Color(212, 44, 96)
		};

		private int fishingLineColorIndex;

		public Color FishingLineColor => PossibleLineColors[fishingLineColorIndex];

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BobberGolden);
			Projectile.width = 10;
			Projectile.height = 18;
		}

		public override void OnSpawn(IEntitySource source)
		{
			fishingLineColorIndex = (byte)Main.rand.Next(PossibleLineColors.Length);
		}

		public override void AI()
		{
			if (!Main.dedServ)
				Lighting.AddLight(Projectile.Center, FishingLineColor.ToVector3());
		}

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)fishingLineColorIndex);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            fishingLineColorIndex = reader.ReadByte();
        }
    }
}
