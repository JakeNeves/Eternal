using Eternal.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DawnstoneHelmet : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
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
            player.setBonus = "16% increased magic damage\n25% decreased mana cost";
            player.magicDamage += 0.16f;
            player.manaCost -= 0.25f;
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
