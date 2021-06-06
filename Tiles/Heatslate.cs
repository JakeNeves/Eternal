using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    public class Heatslate : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMerge[Type][TileType<HeatslateGrowth>()] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			dustType = DustID.Stone;
			drop = ItemType<Items.Placeable.Heatslate>();
			AddMapEntry(new Color(61, 68, 86));
			minPick = 100;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 4.4f;
		}
	}
}
