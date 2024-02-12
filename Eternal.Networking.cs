using Eternal.Common.Players;
using System.IO;
using Terraria;
using Terraria.ID;

namespace Eternal
{
	partial class Eternal
	{
		internal enum MessageType
		{
			StatIncreaseSync,
			TeleportToStatue
		}

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.StatIncreaseSync:
                    byte playerNumber = reader.ReadByte();
                    LifeMotePlayer lifeMotePlayer = Main.player[playerNumber].GetModPlayer<LifeMotePlayer>();
                    lifeMotePlayer.ReceivePlayerSync(reader);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        lifeMotePlayer.SyncPlayer(-1, whoAmI, false);
                    }
                    break;
                default:
                    Logger.WarnFormat("Eternal: Unknown Message type: {0}", msgType);
                    break;
            }
        }
    }
}