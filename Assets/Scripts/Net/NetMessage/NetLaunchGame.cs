using Unity.Networking.Transport;
using UnityEngine;

public class NetLaunchGame : NetMessage
{


    public NetLaunchGame()
    {
        Code = OpCode.LAUNCH_GAME;
    }
    public NetLaunchGame(DataStreamReader reader)
    {
        Code = OpCode.LAUNCH_GAME;
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
        NetUtility.C_LAUNCH_GAME?.Invoke(this);
    }
    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_LAUNCH_GAME?.Invoke(this, cnn);
    }
}
