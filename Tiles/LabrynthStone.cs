using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    class LabrynthStone : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			drop = ItemType<Items.Placeable.LabrynthStone>();
			AddMapEntry(new Color(0, 50, 5));
			minPick = 225;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 5f;
		}
	}
}
