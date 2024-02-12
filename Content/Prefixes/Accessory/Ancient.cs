using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Prefixes.Accessory;

public class Ancient : ModPrefix
{
    public Ancient()
    {

    }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0.75f;
    }

    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void ApplyAccessoryEffects(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.05f;
        player.GetCritChance(DamageClass.Generic) += 15;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
    }
}