using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Magic;
using Eternal.Content.Projectiles.Weapons.Hell;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Hell
{
    public class NiadesRevenge : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LastPrism);
            Item.width = 30;
            Item.height = 34;
            Item.UseSound = SoundID.Item46;
            Item.damage = 150;
            Item.mana = 4;
            Item.shoot = ModContent.ProjectileType<NiadesRevengeHoldout>();
            Item.shootSpeed = 30f;
            Item.rare = ModContent.RarityType<HellMode>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<NiadesRevengeHoldout>()] <= 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Prystaltine>())
                .AddIngredient(ModContent.ItemType<SpiritRites>())
                .Register();
        }
    }
}
