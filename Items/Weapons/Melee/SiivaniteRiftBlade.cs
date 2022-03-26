using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class SiivaniteRiftBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a Siiva Spark\n<right> for True Melee\n'Pierce the rift, and all of it's glory' \n[c/008060:Ancient Artifact] \nSomething about this rift blade seems to be some sort of rift prevention artifact and rift seal...\nI was guessing that it was a sword that sealed any rift.");
        }

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 56;
            item.damage = 1000;
            item.knockBack = 30;
            item.value = Item.buyPrice(platinum: 1, gold: 3);
            item.useTime = 15;
            item.useAnimation = 15;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.shoot = ProjectileType<SiivaSpark>();
            item.autoReuse = true;
            item.melee = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                
                //item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/RiftBlade");
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.shoot = ProjectileID.None;
                item.shootSpeed = 0f;
                item.autoReuse = false;
            }
            else
            {
                Item.staff[item.type] = false;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.shoot = ProjectileType<SiivaSpark>();
                item.shootSpeed = 12f;
                item.autoReuse = true;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<SiivaniteAlloy>(), 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
