using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class ScorchingBrick : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.Torch;
            // ItemDrop = ModContent.ItemType<Items.Placeable.ScorchingBrick>();
            AddMapEntry(new Color(360, 93, 50));
            MinPick = 100;
            HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/HardBrickBreak")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            MineResist = 3f;
        }
    }
}
