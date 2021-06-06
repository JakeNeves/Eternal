using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    public class ScorchedBrick : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			dustType = DustID.Fire;
			drop = ItemType<Items.Placeable.ScorchedBrick>();
			AddMapEntry(new Color(209, 90, 4));
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 1.2f;
		}
	}
}
