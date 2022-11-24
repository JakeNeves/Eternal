using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Ranged;

public class Headstrong : ModPrefix
{
    public Headstrong()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.Ranged; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.15f;
        shootSpeedMult = 3.15f;
    }
}