﻿using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace Eternal.Content
{
    public class EternalMenu : ModMenu
    {
		private const string menuAssetPath = "Eternal/Assets/Textures/Menu";

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/DawnofANewBeginning");

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"{menuAssetPath}/LogoAOE");

        public override string DisplayName => "Eternal";

		public override void OnSelected()
		{
			SoundEngine.PlaySound(SoundID.Research);
		}
	}
}
