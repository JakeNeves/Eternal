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
        public static readonly int DamageBonus = 40;
        public static readonly int CritBonus = 50;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBonus, CritBonus);

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
            player.GetDamage(DamageClass.Generic) += DamageBonus / 100f;
            player.GetCritChance(DamageClass.Generic) += CritBonus / 100f;
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
