using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class BoonOfTheArks : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 600;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<BOTAProjectile>();
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.autoReuse = true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Item.GetSource_None();

            for (int i = 0; i < 6; i++)
            {
                Projectile.NewProjectile(entitySource, target.Center, new Vector2(Main.rand.Next(-8, 8), Main.rand.Next(-8, 8)), ModContent.ProjectileType<BOTAProjectileAOE>(), Item.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }

            SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/BOTAHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(1f, 1.25f),
                MaxInstances = 0,
            }, target.position);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ProjectileID.None;
                Item.shootSpeed = 0f;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<BOTAProjectile>();
                Item.shootSpeed = 6f;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkiumQuartzPlating>(), 48)
                .AddIngredient(ModContent.ItemType<ArkaniumAlloy>(), 30)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 4)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
