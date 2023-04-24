using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;
using Websockets;
using Xamarin.Essentials;
using System;
using CecilsCall.Services;
using CecilsCall.Droid.Services;
using Xamarin.Forms;
using Android.App;
using Newtonsoft.Json;

[assembly: Dependency(typeof(AndroidCommWithServer))]
namespace CecilsCall.Droid.Services
{
    public class AndroidCommWithServer : ICommWithServer
    {
        public static readonly string WSECHOD_URL = "wss://70.32.30.125:2096";
        private IWebSocketConnection connection;
        private CancellationTokenSource token;
        private bool Failed;
        private bool Echo;
        private string SendInfoType;

        // My properties
        private bool isConnectedToServer = false;
        private bool isConnectedToInternet = false;

        public AndroidCommWithServer()
        {

        }
        public async Task<bool> ConnectToInternet()
        {
            isConnectedToInternet = Connectivity.NetworkAccess == NetworkAccess.Internet;
            if (!isConnectedToInternet)
            {
                await Debugger.Prompt("Connection:", "No Internet connection. Set phone options.", "OK");
            }
            return isConnectedToInternet;
        }
        public async Task<bool> ConnectToServer()
        {
            if (!isConnectedToInternet)
            {
                return false;
            }

            // 1) Set up
            Setup();

            // 2) Call factory from your PCL code.
            // This is the same as new   Websockets.Droid.WebsocketConnection();
            // Except that the Factory is in a PCL and accessible anywhere
            connection = Websockets.WebSocketFactory.Create();

            connection.SetIsAllTrusted();
            connection.OnLog += Connection_OnLog;
            connection.OnError += Connection_OnError;
            connection.OnMessage += Connection_OnMessage;
            connection.OnOpened += Connection_OnOpened;

            //Timeout / Setup
            Echo = Failed = false;
            token = new CancellationTokenSource();
            Timeout(token.Token);

            connection.Open(WSECHOD_URL);
            while (!connection.IsOpen && !Failed)
            {
                await Task.Delay(10);
            }

            if (!connection.IsOpen)
            {
                token.Cancel();
                isConnectedToServer = false;
                await Debugger.Prompt("Connection:", "Sorry, server is down. Retry after some time.", "OK");

                return false;
            }

            Debugger.Msg("ConnectToServer: Connected to server");
            isConnectedToServer = true;
            return true;
        }
        private async Task<bool> CheckInternetServerConnectioins()
        {
            Debugger.Msg("Inside CheckInternetServerConnectioins");

            // If not connected make a connection then connect to server
            if (!isConnectedToInternet)
            {
                await ConnectToInternet();
                if (isConnectedToInternet)
                {
                    await ConnectToServer();
                    if (!isConnectedToServer) { return false; }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!isConnectedToServer)
                {
                    await ConnectToServer();
                    if (!isConnectedToServer) { return false; }
                }
            }

            return true;
        }
        public async Task<bool> SendMessage(string msg)
        {
            // If connections fail, return
            bool connectionStatus = await CheckInternetServerConnectioins();

            Debugger.Msg("SendMessage connectionStatus: " + connectionStatus);
            if (!connectionStatus) { return false; }
            connection.Send(msg);
            while (!Echo && !Failed)
            {
                await Task.Delay(10);
            }

            if (!Echo)
            {
                try
                {
                    Assert.True(Echo);
                }
                catch (Exception err)
                {
                    Debugger.Msg("SendMessage Error: " + err.Message);
                }
                return false;
            }

            token.Cancel();
            Assert.True(true);

            return true;

        }
        public async Task<bool> SendByDependency(string msg, string givenType)
        {
            MainActivity.commWithInternetServer.SetReferenceType(givenType);
            return await MainActivity.commWithInternetServer.SendMessage(msg);
        }
        public Task<bool> CloseConnectionsByDependency()
        {
            MainActivity.commWithInternetServer.CloseConnections();

            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }
        public async void CloseConnections()
        {
            // Send message to SERVER to close connection.
            await SendCloseMessage();

            // Set CLIENT parameters to close
            isConnectedToServer = false;
            isConnectedToInternet = false;
        }
        private async Task<bool> SendCloseMessage()
        {
            string type = "CloseConnection";
            string message = JsonConvert.SerializeObject(new { Type = type});
            return await SendMessage(message);

        }
        public void SetReferenceType(string givenType)
        {
            SendInfoType = givenType;
        }

        //============================ Codes below are from GitHub ============================//

        public void Setup()
        {
            // 1) Link in your main activity
            Websockets.Droid.WebsocketConnection.Link();
        }
        public async void TestForDebugging()
        {
            Debugger.Msg("Inside DoTest");

            // 2) Call factory from your PCL code.
            // This is the same as new   Websockets.Droid.WebsocketConnection();
            // Except that the Factory is in a PCL and accessible anywhere
            connection = Websockets.WebSocketFactory.Create();
            connection.SetIsAllTrusted();
            connection.OnLog += Connection_OnLog;
            connection.OnError += Connection_OnError;
            connection.OnMessage += Connection_OnMessage;
            connection.OnOpened += Connection_OnOpened;

            //Timeout / Setup
            Echo = Failed = false;
            var token = new CancellationTokenSource();
            Timeout(token.Token);

            //Do test

            Debugger.Msg("Connecting...");

            connection.Open(WSECHOD_URL);

            while (!connection.IsOpen && !Failed)
            {
                await Task.Delay(10);
            }

            if (!connection.IsOpen)
            {
                token.Cancel();
                Assert.True(false);
                return;
            }
            Debugger.Msg("Connected !");

            Debugger.Msg("Sending...");

            // 3) Sending
            connection.Send("Hello World");

            Debugger.Msg("Sent !");

            while (!Echo && !Failed)
            {
                await Task.Delay(10);
            }

            if (!Echo)
            {
                token.Cancel();
                Assert.True(Echo);
                return;
            }

            token.Cancel();

            Debugger.Msg("Received !");

            Debugger.Msg("Passed !");
            Debugger.Msg("Passed");
            Assert.True(true);
        }
        private void Connection_OnOpened()
        {
            Debugger.Msg("Connection_OnOpened: Opened !");
        }
        async void Timeout(CancellationToken token)
        {
            try
            {
                var t = Task.Delay(30000, token);
                await t;
                if (!t.IsCanceled)
                {
                    Debugger.Msg("Timeout");
                    Failed = true;
                }
            }
            catch (TaskCanceledException) { }
        }
        private void Connection_OnMessage(string obj)
        {
            Debugger.Msg("Connection_OnMessage Type: " + obj);
            Echo = obj.Contains(SendInfoType);
        }
        private void Connection_OnError(System.Exception ex)
        {
            Debugger.Msg("ERROR " + ex.ToString());
            Failed = true;
        }
        private void Connection_OnLog(string obj)
        {
            Debugger.Msg(obj);
        }
        public void Tear()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }
    }
}