using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class UmbralArcanis : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 46;
            Item.damage = 80;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.mana = 5;
            Item.knockBack = 5;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.shoot = ModContent.ProjectileType<UmbralArcanisProjectile>();
            Item.shootSpeed = 4f;
            Item.UseSound = SoundID.Item45;
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
            Item.noMelee = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                var entitySource = player.GetSource_FromThis();

                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.altFunctionUse == 2)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NPCs.Misc.BenightedWisp>() || Main.npc[i].type == ModContent.NPCType<NPCs.Misc.AstralWisp>())
                            {
                                NPC npc = Main.npc[i];
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    npc.StrikeInstantKill();

                                Item.mana = 0;
                                Item.shoot = ProjectileID.None;
                                Item.shootSpeed = 0f;
                            }
                        }
                    }
                    else
                    {
                        Item.mana = 5;
                        Item.shoot = ModContent.ProjectileType<UmbralArcanisProjectile>();
                        Item.shootSpeed = 4f;
                    }
                }
            }

            return true;
        }
    }
}
