using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.DamageClasses
{
    public class Radiant : DamageClass
    {
		internal static Radiant Instance;

        public override void Load()
        {
			Instance = this;
        }

        public override void Unload()
        {
			Instance = null;
        }

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			if (damageClass == DamageClass.Magic || damageClass == DamageClass.Generic)
				return StatInheritanceData.Full;

			return StatInheritanceData.None;
		}

		public override bool GetEffectInheritance(DamageClass damageClass)
		{
			return damageClass == DamageClass.Magic;
		}

		public override void SetDefaultStats(Player player)
		{
			player.GetCritChance<Radiant>() += 4;
			player.GetArmorPenetration<Radiant>() += 10;
		}
		public override bool UseStandardCritCalcs => true;

		public override bool ShowStatTooltipLine(Player player, string lineName)
		{
			if (lineName == "Speed")
				return false;

			return true;
		}
	}
}
