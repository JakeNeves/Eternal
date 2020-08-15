using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    class CometiteOre : ModTile
    {

		public override void SetDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			drop = ItemType<Items.Placeable.CometiteOre>();
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Cometite");
			AddMapEntry(new Color(30, 20, 40), name);
			minPick = 225;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 5f;
		}

	}
}
