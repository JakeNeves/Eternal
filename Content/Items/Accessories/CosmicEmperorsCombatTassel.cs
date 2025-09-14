using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Items.Materials;
using Terraria.Localization;
using Terraria.ID;

namespace Eternal.Content.Items.Accessories
{
    public class CosmicEmperorsCombatTassel : ModItem
    {
        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("Cosmic Emperor's Combat Tassle");

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("40% increased damage" +
                                                                            "\n20% critical strike chance" +
                                                                            "\n[c/008060:Ancient Artifact]" +
                                                                            "\nThis relic of the Cosmic Emperor was weaved with the stardust of comets and moonlight" +
                                                                            "\nWith such power harvested to create such tassle will empower whoever wears this to honor and prase the Cosmic Emperor");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 30;
            Item.value = Item.sellPrice(gold: 13, silver: 70);
            Item.rare = ModContent.RarityType<Teal>();
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.50f;
            player.GetCritChance(DamageClass.Generic) += 1.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<Starforge>())
                .AddIngredient(ModContent.ItemType<FierceDeityEmblem>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 16)
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
        }
    }
}
