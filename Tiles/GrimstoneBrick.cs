using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    public class GrimstoneBrick : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			drop = ItemType<Items.Placeable.GrimstoneBrick>();
			AddMapEntry(new Color(50, 50, 50));
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 1.2f;
		}
	}
}
