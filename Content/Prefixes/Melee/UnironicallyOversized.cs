using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Melee;

public class UnironicallyOversized : ModPrefix
{
    public UnironicallyOversized ()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.Melee; } }

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Unironically Oversized");
    }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        scaleMult = 2.5f;
        damageMult = 1.75f;
        knockbackMult = 2.5f;
    }
}