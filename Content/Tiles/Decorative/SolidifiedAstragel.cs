using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class SolidifiedAstragel : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.DemonTorch;
            ItemDrop = ModContent.ItemType<Content.Items.Placeable.Decorative.SolidifiedAstragel>();
            AddMapEntry(new Color(246, 43, 152));
            HitSound = SoundID.DD2_SkeletonHurt;
        }
    }
}
