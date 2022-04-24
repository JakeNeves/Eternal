using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Eternal.Tiles
{
    class Dunesand : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            drop = ItemType<Items.Placeable.Dunesand>();
            AddMapEntry(new Color(245, 235, 170));
            soundType = SoundID.Dig;
            mineResist = 5f;
        }

    }
}
