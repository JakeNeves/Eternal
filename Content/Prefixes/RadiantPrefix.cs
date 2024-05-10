using Eternal.Content.DamageClasses;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes
{
    public class RadiantPrefix : ModPrefix
    {
        internal float dmgMul = 1f;
        internal float useTimeMul = 1f;
        internal int critBonus;
        internal float shootSpeedMul = 1f;

        public override PrefixCategory Category => PrefixCategory.AnyWeapon;

        public RadiantPrefix() { }

        public RadiantPrefix(float dmgMul = 1f, float useTimeMul = 1f, int critBonus = 0, float shootSpeedMul = 1f)
        { 
            this.dmgMul = dmgMul;
            this.useTimeMul = useTimeMul;
            this.critBonus = critBonus;
            this.shootSpeedMul = shootSpeedMul;
        }

        public override bool CanRoll(Item item)
        {
            if (item.CountsAsClass<Content.DamageClasses.Radiant>() && (item.stack == 1 || item.AllowReforgeForStackableItem))
                return GetType() != typeof(RadiantPrefix);

            return false;
        }
    }
}
