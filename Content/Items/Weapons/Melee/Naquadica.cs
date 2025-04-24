using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Naquadica : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 34;
            Item.height = 30;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 30f;
            Item.knockBack = 9f;
            Item.damage = 240;
            Item.rare = ModContent.RarityType<Magenta>();

            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(platinum: 2);
            Item.shoot = ModContent.ProjectileType<NaquadicaProjectile>();
        }

        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Phantasma>())
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 16)
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>(), 16)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
