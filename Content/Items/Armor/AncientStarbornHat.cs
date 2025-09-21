using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AncientStarbornHat : ModItem
    {
        public static readonly int MagicDamageBonus = 7;
        public static readonly int MagicDamageSetBonus = 10;
        public static readonly int ManaReductionBonus = 10;
        public static readonly int ManaBonus = 30;

        public static LocalizedText SetBonusText { get; private set; }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDamageBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(MagicDamageSetBonus, ManaBonus, ManaReductionBonus);
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
            player.setBonus = SetBonusText.Value;
            player.GetDamage(DamageClass.Magic) += MagicDamageSetBonus / 100f;

            player.statManaMax2 += ManaBonus;
            player.manaCost -= ManaReductionBonus / 100f;
            ArmorSystem.StarbornArmor = true;
            ArmorSystem.StarbornArmorMagicBonus = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += MagicDamageBonus / 100f;
        }
    }
}
