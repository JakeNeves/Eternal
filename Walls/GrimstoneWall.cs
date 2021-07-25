using Eternal.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Walls
{
	public class GrimstoneWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = DustID.Stone;
			Main.wallHouse[Type] = false;
			drop = ItemType<Items.Placeable.GrimstoneWall>();
			AddMapEntry(new Color(16, 20, 26));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.4f;
			g = 0.4f;
			b = 0.4f;
		}
	}
}