using Eternal.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class SactothsConquest : ModItem
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
            Item.damage = 100;
            Item.knockBack = 2f;
            Item.width = 52;
            Item.height = 52;
            Item.scale = 1f;
            Item.UseSound = SoundID.Item71;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.buyPrice(gold: 60);
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<SactothsConquestProjectile>();
            Item.noMelee = true;
            Item.shootsEveryUse = true;
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
