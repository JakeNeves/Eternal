using Eternal.Common.Players;
using Eternal.Common.Systems;
using Eternal.Content.Buffs;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class RodofDistortion : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.buffType = ModContent.BuffType<UnstableState>();
        }

        public override bool? UseItem(Player player)
        {
            if (ArmorSystem.NaquadahArmor && RiftSystem.isRiftOpen)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/RodofDistortion"), player.position);
                player.Teleport(Main.MouseWorld, 2, 0);

                Dust.NewDust(player.position, player.width, player.height, DustID.PinkTorch, 0.5f, 0.5f, 0, Color.White, Main.rand.NextFloat(0.25f, 1f));
            }
            else
            {
                if (!player.HasBuff(ModContent.BuffType<UnstableState>()) || !Main.zenithWorld)
                {
                    player.AddBuff(Item.buffType, 1 * 30 * 30);
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/RodofDistortion"), player.position);
                    player.Teleport(Main.MouseWorld, 2, 0);

                    Dust.NewDust(player.position, player.width, player.height, DustID.PinkTorch, 0.5f, 0.5f, 0, Color.White, Main.rand.NextFloat(0.25f, 1f));
                }
                else
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "'s body was heavily disfigured by the Rod of Distortion's unstable power"), 10000, 1, false);
                }
            }

            return true;
        }
    }
}
