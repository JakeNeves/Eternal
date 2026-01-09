using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MorbusHelmet : ModItem
    {
        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            SetBonusText = this.GetLocalization("SetBonus");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<MorbusRibPlate>() && legs.type == ModContent.ItemType<MorbusLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;

            ArmorSystem.MorbusArmor = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BlightedFarrago>(), 16)
                .AddIngredient(ModContent.ItemType<NecroticTissue>(), 12)
                .AddIngredient(ModContent.ItemType<PolypChunk>(), 12)
                .AddIngredient(ModContent.ItemType<SoulofBlight>(), 6)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
