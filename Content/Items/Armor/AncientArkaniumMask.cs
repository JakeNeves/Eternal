﻿using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AncientArkaniumMask : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("20% increased melee damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.value = Item.sellPrice(platinum: 6);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AncientArkaniumChestplate>() && legs.type == ModContent.ItemType<AncientArkaniumGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "15% increased melee damage and 10% increased melee speed" +
                            "\nSwords rain down on you upon getting hit";

            player.GetDamage(DamageClass.Melee) += 1.15f;
            player.GetAttackSpeed(DamageClass.Melee) += 1.10f;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.JungleTorch, 0f, 0f, 0, new Color(255, 255, 255), 0.5f)];
            dust.fadeIn = 0.4f;
            dust.noGravity = true;

            ArmorSystem.ArkaniumArmor = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlinesForbidden = true;
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 1.10f;
        }
    }
}
