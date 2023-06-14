using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Melee;

public class Emperors : ModPrefix
{
    public Emperors()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.Melee; } }

    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Emperor's");
    }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.20f;
        knockbackMult = 1.05f;
        critBonus = 20;
    }
}