using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AncientStarbornHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("7% increased magic damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 8;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AncientStarbornScalePlate>() && legs.type == ModContent.ItemType<AncientStarbornGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+30 mana, 10% decreased mana cost and 10% increased magic damage" +
                            "\nSome weapons receive special abilities" +
                            "\nWeapon projectiles heal the player by 15 HP when below half health upon hitting any enemy" +
                            "\n15% increased damage when below half health" +
                            "\n[c/FCA5033:Starborn Hat Bonus]" +
                            "\nYou sometimes fire a projectile that homes in on enemies, damage is based on how much mana you have in total";
            player.GetDamage(DamageClass.Magic) += 0.20f;

            player.statManaMax2 += 30;
            player.manaCost -= 0.1f;
            ArmorSystem.StarbornArmor = true;
            ArmorSystem.StarbornArmorMagicBonus = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.07f;
        }
    }
}
