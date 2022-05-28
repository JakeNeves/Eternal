using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;

namespace Eternal.Content.BossBars
{
    public class Notched2BossBar : ModBossBar
    {
		private int bossHeadIndex = -1;

		public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
		{
			if (bossHeadIndex != -1)
			{
				return TextureAssets.NpcHeadBoss[bossHeadIndex];
			}
			return null;
		}

        public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float lifePercent, ref float shieldPercent)
        {
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active)
				return false;

			bossHeadIndex = npc.GetBossHeadTextureIndex();

			lifePercent = Utils.Clamp(npc.life / (float)npc.lifeMax, 0f, 1f);

			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, NPC npc, ref BossBarDrawParams drawParams)
		{
			float shakeIntensity = Utils.Clamp(1f - drawParams.LifePercentToShow - 0.2f, 0f, 1f);
			drawParams.BarCenter.Y -= 20f;
			drawParams.BarCenter += new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)) * shakeIntensity * 5f;

			return true;
		}
	}
}
