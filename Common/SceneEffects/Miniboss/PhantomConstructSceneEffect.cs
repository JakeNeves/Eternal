using Terraria;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Miniboss;

namespace Eternal.Common.SceneEffects.Miniboss
{
    public class PhantomConstructSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/ConstructedByUnstability");

        public override bool IsSceneEffectActive(Player player) => NPC.AnyNPCs(ModContent.NPCType<PhantomConstruct>()) && !Main.zenithWorld;

        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
    }
}