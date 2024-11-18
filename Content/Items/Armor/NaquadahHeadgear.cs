using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class NaquadahHeadgear : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 22;
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
            player.setBonus = "30% increased ranged damage" +
                            "\nYou emit a source of light" +
                            "\nYou release spike bombs upon getting hit" +
                            "\n[c/FCA5033:Rift Bonus]" +
                            "\nImmunity to Rift Withering" +
                            "\nProtection against the Rod of Distortion's unstability";

            player.GetDamage(DamageClass.Ranged) += 0.30f;

            Lighting.AddLight(player.Center, 0.75f, 0f, 0.75f);

            ArmorSystem.NaquadahArmor = true;
            ArmorSystem.NaquadahArmorRangedBonus = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlinesForbidden = true;
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.20f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 5)
                .AddIngredient(ModContent.ItemType<CrystalizedOminite>())
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 6)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
