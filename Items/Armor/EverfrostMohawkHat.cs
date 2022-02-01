using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Eternal.Items.Materials;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class EverfrostMohawkHat : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Everfrost Mage Knight's Mask and Hat");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
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
            player.setBonus = "Immunity to Frozen, Chilled, and Frostburn\n20% Increased Magic Damage, Reduced Mana Cost and 90% Increased Mana";
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.magicDamage += 20f;
            player.manaCost -= 20;
            player.statManaMax += 90;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<SydaniteBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
