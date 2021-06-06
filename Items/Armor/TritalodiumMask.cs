using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Eternal.Items.Materials;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class TritalodiumMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("For both ranged and magic...");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Green;
            item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<TritalodiumPlateMail>() && legs.type == ItemType<TritalodiumLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased Ranged, Magic Damage and Mana";
            player.rangedDamage += 1.5f;
            player.magicDamage += 1.5f;
            player.statManaMax2 += 40;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<TritalodiumBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
