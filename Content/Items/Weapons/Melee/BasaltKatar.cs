using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class BasaltKatar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.damage = 70;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<BasaltKatarProjectile>();
            Item.shootSpeed = 8f;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2 + Main.rand.Next(2);
            float rotation = MathHelper.ToRadians(30);

            position += Vector2.Normalize(velocity) * 15f;

            if (ArmorSystem.SanguineArmorMeleeBonus)
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item103, player.position);

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
                }
            }

            return false;
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
