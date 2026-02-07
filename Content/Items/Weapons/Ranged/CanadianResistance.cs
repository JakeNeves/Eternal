using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class CanadianResistance : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 24;
            Item.damage = 110;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = true;
            Item.shootSpeed = 2f;
            Item.shoot = AmmoID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.rare = ItemRarityID.Yellow;
            Item.knockBack = 2f;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (DownedBossSystem.downedGlare)
                damage += 0.25f;
            if (NPC.downedMoonlord)
                damage += 0.5f;
            if (DownedBossSystem.downedArkofImperious)
                damage += 0.75f;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 5f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int spread = 25;
            float spreadMult = 0.15f;

            for (int i = 0; i < Main.rand.Next(4, 6); i++)
            {
                float vX = velocity.X + Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = velocity.Y + Main.rand.Next(-spread, spread + 1) * spreadMult;

                Projectile.NewProjectile(source, position, new Vector2(vX, vY), type, damage, knockback);
            }

            return true;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-2f, -1.5f);
    }
}
