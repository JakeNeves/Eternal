using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Hell
{
    public class Dreadheart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Dreadheart");
            Tooltip.SetDefault("Increased melee damage By 45%" +
                             "\nIncreased life by 100" +
                             "\nTaking damage can heal you by 6 to 12 HP" +
                             "\nHell Mode upgrade");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 70;
            Item.value = 0;
            Item.rare = ModContent.RarityType<HellMode>();
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += 1.45f;
            player.GetAttackSpeed(DamageClass.Melee) += 1.15f;
            player.statLifeMax2 += 100;
            AccessorySystem.Dreadheart = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CarminiteHeart>())
                .AddIngredient(ModContent.ItemType<FierceDeityEmblem>())
                .AddIngredient(ItemID.LunarBar, 12)
                .AddIngredient(ItemID.HallowedBar, 20)
                .AddIngredient(ItemID.BrokenHeroSword)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
