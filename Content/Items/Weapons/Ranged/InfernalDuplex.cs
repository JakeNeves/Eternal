using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class InfernalDuplex : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 42;
            Item.damage = 80;
            Item.knockBack = 2f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.DD2_PhantomPhoenixShot;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ItemRarityID.Lime;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int spread = 15;
            float spreadMult = 0.2f;

            for (int i = 0; i < 2; i++)
            {
                float vX = velocity.X + Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = velocity.Y + Main.rand.Next(-spread, spread + 1) * spreadMult;

                Projectile.NewProjectile(source, position, new Vector2(vX, vY), type, damage, knockback);
            }

            return false;
        }
    }
}
