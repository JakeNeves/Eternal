using Eternal.Content.Projectiles.Misc;
using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Summon
{
    public class SuspiciousLookingDroplet : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("Summons the Carminite Amalgamation" +
                               "\n'Looks rather tasty...'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<Droplet>();
            Item.shootSpeed = 2.75f;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<CarminiteAmalgamation>()) && player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Carminite>(), 10)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

    }
}
