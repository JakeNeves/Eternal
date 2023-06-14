using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class NaquadahSlate : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.Wraith;
            // ItemDrop = ModContent.ItemType<Content.Items.Placeable.Decorative.CometiteBrick>();
            AddMapEntry(new Color(20, 20, 20));
            HitSound = SoundID.Tink;
            MineResist = 2f;
        }
    }
}
