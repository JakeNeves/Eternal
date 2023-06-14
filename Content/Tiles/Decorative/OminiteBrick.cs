using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class OminiteBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.PinkTorch;
            // ItemDrop = ModContent.ItemType<Content.Items.Placeable.Decorative.OminiteBrick>();
            AddMapEntry(new Color(241, 126, 197));
            HitSound = SoundID.Tink;
            MineResist = 2f;
        }
    }
}
