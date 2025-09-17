using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class CometGauntlet : ModItem
    {
        public static readonly int MeleeDamageBonus = 30;
        public static readonly int MeleeSpeedBonus = 20;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBonus, MeleeSpeedBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ModContent.RarityType<Teal>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //EternalGlobalProjectile.cometGauntlet = true;

            player.GetDamage(DamageClass.Melee) += MeleeDamageBonus / 100f;
            player.GetAttackSpeed(DamageClass.Melee) += MeleeSpeedBonus / 100f;
            player.autoReuseGlove = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<Starforge>())
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 20)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 40)
                .AddIngredient(ModContent.ItemType<InterstellarScrapMetal>(), 10)
                .AddIngredient(ItemID.FireGauntlet)
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
        }
    }
}
