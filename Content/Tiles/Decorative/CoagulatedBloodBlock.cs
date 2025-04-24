using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class CoagulatedBloodBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.Blood;
            AddMapEntry(new Color(200, 25, 10));
            HitSound = SoundID.NPCDeath1;
        }
    }
}
