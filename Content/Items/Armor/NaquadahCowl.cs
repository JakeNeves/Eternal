using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class NaquadahCowl : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = Item.sellPrice(platinum: 15);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.defense = 40;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<NaquadahChestplate>() && legs.type == ModContent.ItemType<NaquadahGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "30% increased minion damage and +20 minion slots" +
                            "\nYou emit a source of light" +
                            "\nStarborn and Arkanium Armor Effects" +
                            "\n[c/FCA5033:Rift Bonus]" +
                            "\nImmunity to Rift Withering" +
                            "\nProtection against the Rod of Distortion's unstability";

            player.GetDamage(DamageClass.Summon) += 0.30f;
            player.maxMinions += 20;

            Lighting.AddLight(player.Center, 0.75f, 0f, 0.75f);

            ArmorSystem.ArkaniumArmor = true;
            ArmorSystem.StarbornArmor = true;
            ArmorSystem.UltimusArmor = true;
            ArmorSystem.NaquadahArmor = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlinesForbidden = true;
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.20f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 5)
                .AddIngredient(ModContent.ItemType<CrystalizedOminite>())
                .AddIngredient(ModContent.ItemType<StarbornMask>())
                .AddIngredient(ModContent.ItemType<UltimusMask>())
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
