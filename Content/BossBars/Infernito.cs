using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Eternal.Content.BossBars
{
    public class Infernito : ModBossBar
    {
        public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
        {
            return TextureAssets.NpcHeadBoss[ModContent.NPCType<NPCs.Boss.Trinity.Infernito>()];
        }
    }
}
