using Eternal.Content.Items.Materials;
using Eternal.Content.NPCs.Boss.AoI;
using Eternal.Content.Projectiles.Misc;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class RoyalShrineSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Swing this sword to summon the Ark of Imperious upon swinging, the god of living swords");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.rare = ModContent.RarityType<Teal>();
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<AoISpark>();
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item71;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<ArkofImperious>()) && player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<Starforge>())
                .AddIngredient(ModContent.ItemType<MachaliteSheets>(), 16)
                .AddIngredient(ModContent.ItemType<IesniumBar>(), 12)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 3)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 10)
                .Register();
        }
    }
}
