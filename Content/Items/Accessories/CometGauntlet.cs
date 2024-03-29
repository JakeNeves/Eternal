﻿using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class CometGauntlet : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("35% increased melee damage" +
                                                                            "\n17% increased melee speed" +
                                                                            "\nEnables auto swing for melee weapons" +
                                                                            "\n'The comets are now in the palm of your hand'");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ModContent.RarityType<Teal>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //EternalGlobalProjectile.cometGauntlet = true;

            player.GetDamage(DamageClass.Melee) += 1.35f;
            player.GetAttackSpeed(DamageClass.Melee) += 1.17f;
            player.autoReuseGlove = true;
        }
    }
}
