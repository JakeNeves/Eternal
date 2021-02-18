using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories
{
    public class BlackLantern : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants Immunity to Obstruction while in The Beneath");
        }
        
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneBeneath)
            {
                player.buffImmune[BuffID.Obstructed] = true;
            }
            else
            {
                player.buffImmune[BuffID.Obstructed] = false;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.IronBar, 4);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Torch);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe altrecipe = new ModRecipe(mod);
            altrecipe.AddTile(TileID.Anvils);
            altrecipe.AddIngredient(ItemID.LeadBar, 4);
            altrecipe.AddIngredient(ItemID.Chain, 2);
            altrecipe.AddIngredient(ItemID.Torch);
            altrecipe.SetResult(this);
            altrecipe.AddRecipe();
        }
    }
}
