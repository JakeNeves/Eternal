using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class PsybenderPlus : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 80;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 8;
            Item.knockBack = 6f;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.shoot = ModContent.ProjectileType<PsybenderProjectile>();
            Item.shootSpeed = 30f;
            Item.UseSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/Psy")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            Item.rare = ItemRarityID.Lime;
            Item.noMelee = true;
            Item.channel = true;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ModContent.ItemType<CursedAshes>(), 20)
                .AddIngredient(ModContent.ItemType<Psybender>())
                .Register();
        }
    }
}
