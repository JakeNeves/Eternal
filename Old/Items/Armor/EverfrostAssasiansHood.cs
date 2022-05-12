using Eternal.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class EverfrostAssasiansHood : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Everfrost Assasian's Hood");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Red;
            item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<EverfrostChestplate>() && legs.type == ItemType<EverfrostBoots>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Immunity to Frozen, Chilled, and Frostburn\n20% Increased Ranged Damage";
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.rangedDamage += 20f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<SydaniteBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
