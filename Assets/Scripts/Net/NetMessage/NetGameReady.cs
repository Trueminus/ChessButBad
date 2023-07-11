using Unity.Networking.Transport;
using UnityEngine;

public class NetGameReady : NetMessage
{
    public int gameSize { set; get; }
    public int gameId { set; get; }
    public int isReady { set; get; }
    public NetGameReady()
    {
        Code = OpCode.GAME_READY;
    }
    public NetGameReady(DataStreamReader reader)
    {
        Code = OpCode.GAME_READY;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(gameSize);
        writer.WriteInt(gameId);
        writer.WriteInt(isReady);
    }
    public override void Deserialize(DataStreamReader reader)
    {
        gameSize = reader.ReadInt();
        gameId = reader.ReadInt();
        isReady = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_GAME_READY?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_GAME_READY?.Invoke(this, cnn);
    }
}
