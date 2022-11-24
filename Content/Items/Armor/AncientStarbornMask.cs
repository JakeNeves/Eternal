using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AncientStarbornMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("7% increased melee damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 16;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AncientStarbornScalePlate>() && legs.type == ModContent.ItemType<AncientStarbornGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased melee damage and 7% increased melee speed" +
                            "\nSome weapons receive special abilities" +
                            "\nWeapon projectiles heal the player by 15 HP when below half healt upon hitting any enemy" +
                            "\n15% increased damage when below half health" +
                            "\n[c/FCA5033:Starborn Mask Bonus]" +
                            "\nSome melee weapons recieve special modifiers";
            player.GetDamage(DamageClass.Melee) += 0.10f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.07f;
            ArmorSystem.StarbornArmor = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.07f;
        }
    }
}
