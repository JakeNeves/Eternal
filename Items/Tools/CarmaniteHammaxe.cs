using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;
using Eternal.Items.Materials;

namespace Eternal.Items.Tools
{
    class CarmaniteHammaxe : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carmanite Hamaxe");
            Tooltip.SetDefault("'Bloody chop-chop...'");
        }

        public override void SetDefaults()
        {
            item.damage = 8;
            item.melee = true;
            item.width = 60;
            item.height = 60;
            item.useTime = 10;
            item.useAnimation = 15;
            item.axe = 10;
            item.hammer = 20; 
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2;
            item.value = Item.buyPrice(gold: 1, silver: 10);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(position, 42, 42, DustID.Blood, 0f, 0f, 0, new Color(220, 8, 4), 1f)];
            dust.shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddIngredient(ItemType<Carmanite>(), 80);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
