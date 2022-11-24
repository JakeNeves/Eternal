using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Generic;

public class Heroic : ModPrefix
{
    public Heroic()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.AnyWeapon; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.10f;
    }
}