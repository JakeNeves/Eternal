using Eternal.Content.Projectiles.Misc;
using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.DuneGolem;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Summon
{
    public class IesniumBeacon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a possessed desert idol" +
                               "\n'Throw it in the air, watch it attract an idol'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<IesniumBeaconProjectile>();
            Item.shootSpeed = 2.75f;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<DuneGolem>()) && player.ownedProjectileCounts[Item.shoot] < 1 && player.ZoneDesert;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<IesniumBar>(), 6)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}
