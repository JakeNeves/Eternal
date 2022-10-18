using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class UltimusHood : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("30% increased ranged damage");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
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
            player.setBonus = "42% increased ranged damage" +
                            "\nYou emit a source of light" +
                            "\nStarborn weapons cost 0 mana" +
                            "\nStarborn and Arkanium Armor Effects";

            player.GetDamage(DamageClass.Ranged) += 0.42f;

            Lighting.AddLight(player.Center, 1.14f, 0.22f, 1.43f);

            ArmorSystem.ArkaniumArmor = true;
            ArmorSystem.StarbornArmor = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.30f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientForge>())
                .AddIngredient(ModContent.ItemType<ArkaniumHeadgear>())
                .AddIngredient(ModContent.ItemType<StarbornHeadgear>())
                .AddIngredient(ModContent.ItemType<CoreofEternal>(), 8)
                .Register();
        }
    }
}
