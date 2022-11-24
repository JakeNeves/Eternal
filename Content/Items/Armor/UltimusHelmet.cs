using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class UltimusHelmet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("30% increased minion damage");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(platinum: 6);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 20;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<UltimusPlateMail>() && legs.type == ModContent.ItemType<UltimusLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "42% increased minion damage, 64% decreased mana cost and +24 minion slots" +
                            "\nYou emit a source of light" +
                            "\nStarborn and Arkanium Armor Effects";

            player.GetDamage(DamageClass.Summon) += 0.42f;

            player.statManaMax2 = 300;

            player.manaCost -= 1.64f;
            player.maxMinions += 24;

            Lighting.AddLight(player.Center, 1.14f, 0.22f, 1.43f);

            ArmorSystem.ArkaniumArmor = true;
            ArmorSystem.StarbornArmor = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.30f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientForge>())
                .AddIngredient(ModContent.ItemType<ArkaniumHelmet>())
                .AddIngredient(ModContent.ItemType<StarbornHelmet>())
                .AddIngredient(ModContent.ItemType<CoreofEternal>(), 8)
                .AddIngredient(ModContent.ItemType<StargloomCometiteBar>(), 12)
                .Register();
        }
    }
}
