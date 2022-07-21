using Eternal.Content.Projectiles.Weapons.Melee.Shortsword;
using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class SerpentKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Teal>();
            Item.damage = 400;
            Item.useAnimation = 6;
            Item.useTime = 6;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 3f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<SerpentKillerProjectile>();
            Item.shootSpeed = 2.1f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
    }
}
