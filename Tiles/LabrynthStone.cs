using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    public class LabrynthStone : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			dustType = DustID.Stone;
			drop = ItemType<Items.Placeable.LabrynthStone>();
			AddMapEntry(new Color(21, 26, 35));
			minPick = 225;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 1.2f;
		}
	}
}
