using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Magic;

public class Unstable : ModPrefix
{
    public Unstable()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.Magic; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = Main.rand.NextFloat(-0.5f, 1.15f);
        manaMult = Main.rand.NextFloat(-1.15f, 0.5f);
    }
}