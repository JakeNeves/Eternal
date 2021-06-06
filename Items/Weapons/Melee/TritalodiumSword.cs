using Eternal.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Eternal.Items.Weapons.Melee
{
    class TritalodiumSword : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.melee = true;
            item.height = 48;
            item.damage = 32;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 5;
            item.value = Item.buyPrice(gold: 1, silver: 3);
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.rare = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemType<TritalodiumBar>(), 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
