using Eternal.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class EthershiftWood : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.PinkTorch;
            AddMapEntry(new Color(100, 100, 100));
            HitSound = SoundID.Dig;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Main.tileColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DeepPink, Color.MediumPurple, Color.DeepPink);
        }
    }
}
