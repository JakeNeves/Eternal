using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eternal.Content.Tiles
{
    public class IesniumOre : ModTile
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
            DustType = DustID.BlueTorch;
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(22, 71, 73), name);
            MinPick = 100;
            HitSound = SoundID.Tink;
            MineResist = 2f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.58f;
            g = 1.59f;
            b = 1.64f;
        }
	}

    public class IesniumOreSystem : ModSystem
    {
        public static LocalizedText IesniumOrePassMessage { get; private set; }
        public static LocalizedText BlessedWithIesniumOreMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            IesniumOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(IesniumOrePassMessage)}");
            BlessedWithIesniumOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithIesniumOreMessage)}");
        }

        public void BlessWorldWithIesnium()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(_ => {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(BlessedWithIesniumOreMessage.Value, 22, 71, 73);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(BlessedWithIesniumOreMessage.ToNetworkText(), new Color(22, 71, 73));
                }

                int splotches = (int)(100 * (Main.maxTilesX / 4200f));
                int highestY = (int)Utils.Lerp(Main.rockLayer, Main.UnderworldLayer, 0.5);
                for (int iteration = 0; iteration < splotches; iteration++)
                {
                    int i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                    int j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);

                    WorldGen.OreRunner(i, j, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(5, 9), (ushort)ModContent.TileType<IesniumOre>());
                }
            });
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new IesniumOrePass("Iesnium Ore", 237.4298f));
            }
        }
    }

    public class IesniumOrePass : GenPass
    {
        public IesniumOrePass(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = IesniumOreSystem.IesniumOrePassMessage.Value;

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 12), WorldGen.genRand.Next(4, 12), ModContent.TileType<IesniumOre>());
            }
        }
    }
}
