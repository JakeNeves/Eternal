using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class ShrineBrick : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.Stone;
            ItemDrop = ModContent.ItemType<Content.Items.Placeable.ShrineBrick>();
            AddMapEntry(new Color(21, 26, 35));
            MinPick = 225;
            HitSound = SoundID.Tink;
            MineResist = 4f;
        }
    }
}
