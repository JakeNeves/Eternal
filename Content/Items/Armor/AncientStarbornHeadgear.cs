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
    public class AncientStarbornHeadgear : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("7% increased ranged damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
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
            player.setBonus = "10% increased ranged damage" +
                            "\nSome weapons receive special abilities" +
                            "\nWeapon projectiles heal the player by 15 HP when below half healt upon hitting any enemy" +
                            "\n15% increased damage when below half health" +
                            "\n[c/FCA5033:Starborn Headgear Bonus]" +
                            "\nGrants the stealth effect of the Shroomite Armor";
            player.GetDamage(DamageClass.Ranged) += 0.10f;
            ArmorSystem.StarbornArmor = true;

            player.shroomiteStealth = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.07f;
        }
    }
}
