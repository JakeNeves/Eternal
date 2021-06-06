using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Eternal.Tiles
{
    public class CoraliumSludge : ModTile
    {

		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = false;
			Main.tileLighted[Type] = false;
			drop = ItemType<Items.Placeable.CoraliumSludge>();
			AddMapEntry(new Color(6, 150, 109));
			soundType = SoundID.Dig;
			mineResist = 0.5f;
			dustType = DustID.Grass;
		}

	}
}
