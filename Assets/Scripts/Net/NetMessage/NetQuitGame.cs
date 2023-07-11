using Unity.Networking.Transport;
using UnityEngine;

public class NetQuitGame : NetMessage
{
    public NetQuitGame()
    {
        Code = OpCode.QUIT_GAME;
    }
    public NetQuitGame(DataStreamReader reader)
    {
        Code = OpCode.QUIT_GAME;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }
    public override void Deserialize(DataStreamReader reader)
    {

    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_QUIT_GAME?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_QUIT_GAME?.Invoke(this, cnn);
    }
}
