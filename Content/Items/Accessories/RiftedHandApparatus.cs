using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class RiftedHandApparatus : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ModContent.RarityType<Turquoise>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += 0.40f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.20f;
            player.autoReuseGlove = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .AddIngredient(ModContent.ItemType<CometGauntlet>())
                .AddIngredient(ModContent.ItemType<InertHandApparatus>())
                .AddIngredient(ModContent.ItemType<WeaponGradeNaquadahAlloy>(), 20)
                .AddIngredient(ModContent.ItemType<ShiftblightAmethyst>(), 10)
                .Register();
        }
    }
}
