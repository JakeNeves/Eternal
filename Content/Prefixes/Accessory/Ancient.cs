using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Accessory;

public class Ancient : ModPrefix
{
    public Ancient()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.2f;
        critBonus = 75;
    }
}