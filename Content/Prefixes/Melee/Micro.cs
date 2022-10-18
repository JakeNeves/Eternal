using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Melee;

public class Micro : ModPrefix
{
    public Micro()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.Melee; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        scaleMult = -0.75f;
    }
}