using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 70;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 12;
            Item.knockBack = 2f;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.shoot = ModContent.ProjectileType<BallofSewage>();
            Item.shootSpeed = 10f;
            Item.UseSound = SoundID.Item111;
            Item.rare = ItemRarityID.Yellow;
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
                .AddIngredient(ModContent.ItemType<SoulofBlight>(), 6)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int spread = 20;
            float spreadMult = 0.2f;

            for (int i = 0; i < Main.rand.Next(3, 5); i++)
            {
                float vX = velocity.X + Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = velocity.Y + Main.rand.Next(-spread, spread + 1) * spreadMult;

                Projectile.NewProjectile(source, position, new Vector2(vX, vY), type, damage, knockback);
            }

            return true;
        }
    }
}
