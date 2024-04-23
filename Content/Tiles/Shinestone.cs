using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class Shinestone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.GemDiamond;
            AddMapEntry(new Color(119, 115, 129));
            MinPick = 100;
            HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/ShinestoneHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            MineResist = 4.25f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 2.05f;
            g = 2.08f;
            b = 2.24f;
        }
    }
}
