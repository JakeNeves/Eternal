using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class EverfrostFortressHelmet : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
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
            player.setBonus = "Immunity to Frozen, Chilled, and Frostburn\n20% Increased Melee Damage";
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.meleeDamage += 20f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<SydaniteBar>(), 10);
            recipe.AddIngredient(ItemType<TritalodiumHelmet>());
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
