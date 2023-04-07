using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class BasaltKatar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Basalt Katar");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<EdgeofTheInfernoProjectile>();
            Item.shootSpeed = 30f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 1 * 60 * 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MagmaticAlloy>(), 12)
                .AddIngredient(ModContent.ItemType<InfernalAshes>(), 24)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
