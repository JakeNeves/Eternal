using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class FierceDeityEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased damage" +
                             "\n10% critical strike chance" +
                             "\n[c/008060:Ancient Artifact]" +
                             "\nAn emblem empowered with a godly radiance" +
                             "\nLegends say this was used to empower the guardians of the dunes, aiding them in combat and allowing them to become reststant to any weakness they had");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(gold: 13, silver: 70);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 1.20f;
            player.GetCritChance(DamageClass.Generic) += 1.10f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ItemID.AvengerEmblem)
                .AddIngredient(ModContent.ItemType<MalachiteSheets>(), 4)
                .AddIngredient(ModContent.ItemType<IesniumBar>(), 16)
                .Register();
        }
    }
}
