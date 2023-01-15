using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Items.Materials;

namespace Eternal.Content.Items.Accessories
{
    public class CosmicEmperorsCombatTassle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Emperor's Combat Tassle");
            Tooltip.SetDefault("40% increased damage" +
                             "\n20% critical strike chance" +
                             "\n[c/008060:Ancient Artifact]" +
                             "\nThis relic of the Cosmic Emperor was weaved with the stardust of comets and moonlight" +
                             "\nWith such power harvested to create such tassle will empower whoever wears this to honor and prase the Cosmic Emperor");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 38;
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
    }
}
