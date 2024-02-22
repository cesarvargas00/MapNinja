﻿using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace MapNinja.Connection
{
    class OBSWebSocket : IDisposable
    {
        public ClientWebSocket ws { get; set; }
        public bool isConnected { get; set; }

        public OBSWebSocket()
        {
            isConnected = false;
            ws = new ClientWebSocket();
        }

        public async Task ConnectToWebSocket(string ipAddress)
        {
            ws = new ClientWebSocket();
            Task t;
            await ws.ConnectAsync(new Uri("ws://" + ipAddress + ":4455"), new System.Threading.CancellationToken());
            Console.WriteLine("Connected to OBS WebSocket.");
            Console.WriteLine("Identifying...");
            await ws.SendAsync(new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes("{\"op\": 1,\"d\": {\"rpcVersion\": 1}}")), WebSocketMessageType.Text, true, new System.Threading.CancellationToken());
            isConnected = true;
        }

        public async void Dispose()
        {
            if (ws != null && ws.State == WebSocketState.Open)
            {
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "User requested close", new System.Threading.CancellationToken());
                isConnected = false;
                ws.Dispose();
            }
        }
    }
}

