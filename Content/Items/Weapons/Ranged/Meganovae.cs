using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class Meganovae : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 30;
            Item.damage = 220;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/MeganovaeFire");
            Item.autoReuse = true;
            Item.shootSpeed = 5f;
            Item.shoot = AmmoID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.rare = ItemRarityID.Red;
            Item.knockBack = 2f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(source, player.position.X - 100, player.position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, player.position.X + 120, player.position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, -1);
        }
    }
}
