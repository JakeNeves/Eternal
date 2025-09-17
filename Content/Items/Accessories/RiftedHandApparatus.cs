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
    public class RiftedHandApparatus : ModItem
    {
        public static readonly int MeleeDamageBonus = 45;
        public static readonly int MeleeSpeedBonus = 25;

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
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ModContent.RarityType<Turquoise>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) +=  MeleeDamageBonus / 100f;
            player.GetAttackSpeed(DamageClass.Melee) += MeleeSpeedBonus / 100f;
            player.autoReuseGlove = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .AddIngredient(ModContent.ItemType<CometGauntlet>())
                .AddIngredient(ModContent.ItemType<InertHandApparatus>())
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 20)
                .AddIngredient(ModContent.ItemType<ShiftblightAmethyst>(), 10)
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
        }
    }
}
