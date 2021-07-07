using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Eternal.Items.Materials;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DawnstoneMask  : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 30;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.LightPurple;
            item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<DawnstoneChestplate>() && legs.type == ItemType<DawnstoneGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "16% increased melee damage";
            player.meleeDamage += 0.16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Dawnstone>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
