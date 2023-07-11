using System;
using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    START_GAME = 3,
    MAKE_MOVE = 4,
    REMATCH = 5,
    GAME_READY = 6,
    LAUNCH_GAME = 7,
    QUIT_GAME = 8
}

public static class NetUtility
{
    public static void OnData(DataStreamReader stream, NetworkConnection cnn, Server server = null)
    {
        NetMessage msg = null;
        var opCode = (OpCode)stream.ReadByte();
        switch(opCode)
        {
            case OpCode.KEEP_ALIVE: msg = new NetKeepAlive(stream); break;
            case OpCode.WELCOME: msg = new NetWelcome(stream); break;
            case OpCode.START_GAME: msg = new NetStartGame(stream); break;
            case OpCode.MAKE_MOVE: msg = new NetMakeMove(stream); break;
            //case OpCode.REMATCH:msg = new NetRematch(stream); break;
            case OpCode.GAME_READY: msg = new NetGameReady(stream); break;
            case OpCode.LAUNCH_GAME: msg = new NetLaunchGame(stream); break;
            case OpCode.QUIT_GAME: msg = new NetQuitGame(stream); break;
            default:
                Debug.LogError("Message received had no OpCode");
                break;
        }

        if(server != null)
        {
            msg.ReceivedOnServer(cnn);
        }
        else
        {
            msg.ReceivedOnClient();
        }
    }

    //Net Messages
    public static Action<NetMessage> C_KEEP_ALIVE;
    public static Action<NetMessage> C_WELCOME;
    public static Action<NetMessage> C_START_GAME;
    public static Action<NetMessage> C_MAKE_MOVE;
    public static Action<NetMessage> C_REMATCH;
    public static Action<NetMessage> C_GAME_READY;
    public static Action<NetMessage> C_LAUNCH_GAME;
    public static Action<NetMessage> C_QUIT_GAME;
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME;
    public static Action<NetMessage, NetworkConnection> S_START_GAME;
    public static Action<NetMessage, NetworkConnection> S_MAKE_MOVE;
    public static Action<NetMessage, NetworkConnection> S_REMATCH;
    public static Action<NetMessage, NetworkConnection> S_GAME_READY;
    public static Action<NetMessage, NetworkConnection> S_LAUNCH_GAME;
    public static Action<NetMessage, NetworkConnection> S_QUIT_GAME;

}
