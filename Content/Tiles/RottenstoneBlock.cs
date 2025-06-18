using Eternal.Common.Configurations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class RottenstoneBlock : ModTile
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.instance.update15;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.GreenBlood;
            AddMapEntry(new Color(41, 51, 26));
            MinPick = 100;
            HitSound = SoundID.Tink;
            MineResist = 4.25f;
        }
    }
}
