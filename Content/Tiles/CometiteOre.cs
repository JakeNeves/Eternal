using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class CometiteOre : ModTile
    {

        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 500;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = ModContent.DustType<CosmicSpirit>();
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(30, 20, 40), name);
            MinPick = 225;
            HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/CometiteOreHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.4f),
                MaxInstances = 0,
                Variants = new[] { 1, 2, 3, 4 }
            };
            MineResist = 5.5f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1.20f;
            g = 0.50f;
            b = 1.90f;
        }
    }
}
