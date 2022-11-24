using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Accessory;

public class Stalwart : ModPrefix
{
    public Stalwart()
    {

    }

    public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void Apply(Item item)
    {
        Main.player[Main.myPlayer].statDefense += 10;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.10f;
        critBonus = 75;
        knockbackMult = 1.5f;
    }
}