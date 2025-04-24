using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ModLoader;

namespace Eternal.Content.BossBars
{
    public class Cryota : ModBossBar
    {
        public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
        {
            return TextureAssets.NpcHeadBoss[ModContent.NPCType<NPCs.Boss.Trinity.Cryota>()];
        }
    }
}
