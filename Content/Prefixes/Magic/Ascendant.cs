using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Magic;

public class Ascendant : ModPrefix
{
    public Ascendant()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.Magic; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.25f;
        manaMult -= 1.05f;
    }
}