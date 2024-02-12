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

    public override void ApplyAccessoryEffects(Player player)
    {
        player.statDefense += 10;
        player.GetDamage(DamageClass.Generic) += 1.10f;
        player.GetCritChance(DamageClass.Generic) += 30;
        player.GetKnockback(DamageClass.Generic) += 1.5f;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
    }
}