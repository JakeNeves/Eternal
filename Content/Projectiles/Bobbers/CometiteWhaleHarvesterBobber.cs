using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Bobbers
{
    public class CometiteWhaleHarvesterBobber : ModProjectile
    {
		public static readonly Color[] PossibleLineColors = new Color[] {
			new Color(96, 33, 99),
			new Color(110, 33, 183),
			new Color(212, 44, 96)
		};

		private int fishingLineColorIndex;

		private Color FishingLineColor => PossibleLineColors[fishingLineColorIndex];

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Whale Harvester Bobber");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BobberGolden);

			DrawOriginOffsetY = -8;
		}

		public override void OnSpawn(IEntitySource source)
		{
			fishingLineColorIndex = (byte)Main.rand.Next(PossibleLineColors.Length);
		}

		public override void AI()
		{
			if (!Main.dedServ)
			{
				Lighting.AddLight(Projectile.Center, FishingLineColor.ToVector3());
			}
		}

		public override void ModifyFishingLine(ref Vector2 lineOriginOffset, ref Color lineColor)
		{
			lineOriginOffset = new Vector2(47, -31);
			lineColor = FishingLineColor;
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
