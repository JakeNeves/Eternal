using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    public class Grimstone : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			drop = ItemType<Items.Placeable.Grimstone>();
			AddMapEntry(new Color(40, 40, 40));
			minPick = 50;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 4f;
		}
	}
}
