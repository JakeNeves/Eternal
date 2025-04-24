using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class Psybender : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 20;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 8;
            Item.knockBack = 6f;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.shoot = ModContent.ProjectileType<PsybenderProjectile>();
            Item.shootSpeed = 10f;
            Item.UseSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/Psy")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.channel = true;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ModContent.ItemType<OcculticMatter>(), 20)
                .AddIngredient(ModContent.ItemType<PsychicAshes>(), 10)
                .AddIngredient(ModContent.ItemType<PsyblightEssence>(), 40)
                .AddIngredient(ItemID.SoulofNight, 12)
                .AddIngredient(ItemID.SpellTome)
                .Register();
        }
    }
}
