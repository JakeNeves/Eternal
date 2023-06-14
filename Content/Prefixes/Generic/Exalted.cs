using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Generic;

public class Exalted : ModPrefix
{
    public Exalted()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.AnyWeapon; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.15f;
        critBonus = 25;
    }
}