using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class UltimusMask : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("30% increased melee damage");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
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
            player.setBonus = "42% increased melee damage and 25% increased melee speed" +
                            "\nSome melee weapons recieve special modifiers" +
                            "\nYou emit a source of light" +
                            "\nStarborn and Arkanium Armor Effects";

            player.GetDamage(DamageClass.Melee) += 1.42f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.25f;

            Lighting.AddLight(player.Center, 1.14f, 0.22f, 1.43f);

            ArmorSystem.ArkaniumArmor = true;
            ArmorSystem.StarbornArmor = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlinesForbidden = true;
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 1.30f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientForge>())
                .AddIngredient(ModContent.ItemType<ArkaniumMask>())
                .AddIngredient(ModContent.ItemType<StarbornMask>())
                .AddIngredient(ModContent.ItemType<CoreofEternal>(), 8)
                .AddIngredient(ModContent.ItemType<StargloomCometiteBar>(), 12)
                .Register();
        }
    }
}
