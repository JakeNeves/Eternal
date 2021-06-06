using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Eternal.Items.Materials;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class TritalodiumHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("For both melee and summon...");
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
            player.setBonus = "Increased Melee, Minion Damage and Max Minions";
            player.meleeDamage += 1.5f;
            player.minionDamage += 1.5f;
            player.maxMinions += 4;
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
