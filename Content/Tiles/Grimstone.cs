using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class Grimstone : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.Stone;
            ItemDrop = ModContent.ItemType<Items.Placeable.Grimstone>();
            AddMapEntry(new Color(24, 24, 24));
            MinPick = 50;
            HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/GrimstoneBreak")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            MineResist = 6f;
        }
    }
}
