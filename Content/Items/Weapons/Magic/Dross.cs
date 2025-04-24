using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class Dross : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 80;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 12;
            Item.knockBack = 2f;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.shoot = ModContent.ProjectileType<BallofSewage>();
            Item.shootSpeed = 10f;
            Item.UseSound = SoundID.Item111;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ItemID.ChlorophyteBar, 20)
                .AddIngredient(ItemID.MudBlock, 16)
                .AddIngredient(ItemID.Vine, 12)
                .Register();
        }
    }
}
