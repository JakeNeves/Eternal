using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class GehennaSpike : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.TouchDamageBleeding[Type] = true;
            TileID.Sets.TouchDamageImmediate[Type] = 20;
            DustType = DustID.Stone;
            AddMapEntry(new Color(25, 5, 5));
            HitSound = SoundID.Tink;
            MineResist = 1.5f;
        }

        public override bool IsTileDangerous(int i, int j, Player player) => true;
    }
}
