using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class CometiteBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.PurpleTorch;
            ItemDrop = ModContent.ItemType<Content.Items.Placeable.Decorative.CometiteBrick>();
            AddMapEntry(new Color(60, 40, 80));
            HitSound = SoundID.Tink;
            MineResist = 2f;
        }
    }
}
