using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class BladeofEternalDivinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.damage = 200;
            Item.knockBack = 2f;
            Item.width = 56;
            Item.height = 56;
            Item.scale = 1f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Azure>();
            Item.value = Item.buyPrice(platinum: 2, gold: 90);
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<BladeofEternalDivinityProjectile2>();
            Item.shootSpeed = 6f;
            Item.noMelee = true;
            Item.shootsEveryUse = true;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ProviditeBar>(), 16)
                .AddIngredient(ModContent.ItemType<CoreofExodus>(), 6)
                .AddIngredient(ModContent.ItemType<EternalQuartz>(), 12)
                .AddIngredient(ModContent.ItemType<MindCrystalCluster>())
                .AddIngredient(ModContent.ItemType<BodyCrystalCluster>())
                .AddIngredient(ModContent.ItemType<SoulCrystalCluster>())
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int spread = 25;
            float spreadMult = 0.15f;

            for (int i = 0; i < Main.rand.Next(3, 9); i++)
            {
                float vX = velocity.X + Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = velocity.Y + Main.rand.Next(-spread, spread + 1) * spreadMult;

                Projectile.NewProjectile(source, position, new Vector2(vX, vY), type, damage, knockback);
            }

            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<BladeofEternalDivinityProjectile>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
