using Eternal.Common.Configurations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class RottingFleshBlock : ModTile
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.instance.update15;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.GreenBlood;
            AddMapEntry(new Color(20, 25, 13));
            HitSound = SoundID.NPCDeath1;
            MineResist = 1f;
        }
    }
}
