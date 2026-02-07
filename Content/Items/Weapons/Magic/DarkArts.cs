using Eternal.Common.Players;
using Eternal.Content.Projectiles.Weapons.Magic;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class DarkArts : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 150;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 8;
            Item.knockBack = 0f;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.shoot = ModContent.ProjectileType<DarkArtsProjectile>();
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item45;
            Item.rare = ModContent.RarityType<Maroon>();
            Item.autoReuse = true;
            Item.noMelee = true;
        }

        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            if (AccessorySystem.ShadeLocket)
                mult *= 0f;
        }
    }
}
