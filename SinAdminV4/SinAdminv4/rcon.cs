using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityScript;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace SinAdminv4
{
    class rcon : BaseScript
    {

        private Thread start;
        private bool _init = false;
        private const int listenPort = 27068;
        private UdpClient listener = new UdpClient(listenPort);
        private IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        private string received_data = "NOTHING";
        private Boolean done = false;
        private Boolean exception_thrown = false;
        private Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
        ProtocolType.Udp);
        private IPEndPoint sending_end_point;
        private IPAddress send_to_address = IPAddress.Parse("127.0.0.1");

        public rcon()
            : base()
        {
            Log.Write(LogLevel.All, "Rcon Initialized");
            sending_end_point = new IPEndPoint(send_to_address, 27069);
            PlayerConnected += new Action<Entity>(pConnect);
            PlayerDisconnected += new Action<Entity>(dConnect);
        }

        public void pConnect(Entity player)
        {

            byte[] send_buffer = Encoding.ASCII.GetBytes(player.Name + " " + "Connected");

            try
            {
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception send_exception)
            {
                Log.Write(LogLevel.All, send_exception.ToString());
            }


            Thread start = new Thread(
                 new ThreadStart(ThreadStart));
            start.Start();
        }

        public override BaseScript.EventEat OnSay2(Entity player, string name, string message)
        {
            byte[] send_buffer = Encoding.ASCII.GetBytes(player.Name + " " + message);

            try
            {
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception send_exception)
            {
                Log.Write(LogLevel.All, send_exception.ToString());
            }
            return base.OnSay2(player, name, message);
        }

        public void dConnect(Entity player)
        {

            byte[] send_buffer = Encoding.ASCII.GetBytes(player.Name + " " + "Connected");

            try
            {
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception send_exception)
            {
                Log.Write(LogLevel.All, send_exception.ToString());
            }
        }
        public string overRide = "";
        public void udp()
        {
            byte[] receive_byte_array;
            try
            {
                Log.Write(LogLevel.All, "Waiting for message");
                receive_byte_array = listener.Receive(ref groupEP);
                Log.Write(LogLevel.All, "Received a message from {0}", groupEP.ToString());
                received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                Log.Write(LogLevel.All, received_data);
                overRide = received_data;
            }
            catch (Exception e)
            {
                Log.Write(LogLevel.All, e.ToString());

            }
        }

        public override void OnPlayerKilled(Entity player, Entity inflictor, Entity attacker, int damage, string mod, string weapon, Vector3 dir, string hitLoc)
        {
            byte[] send_buffer = Encoding.ASCII.GetBytes(player.Name + " " + attacker.Name + " " + damage + " " + mod + " " + weapon);

            try
            {
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception send_exception)
            {
                Log.Write(LogLevel.All, send_exception.ToString());
            }

            base.OnPlayerKilled(player, inflictor, attacker, damage, mod, weapon, dir, hitLoc);
        }

        public override void OnPlayerDamage(Entity player, Entity inflictor, Entity attacker, int damage, int dFlags, string mod, string weapon, Vector3 point, Vector3 dir, string hitLoc)
        {
            byte[] send_buffer = Encoding.ASCII.GetBytes(player.Name + " " + attacker.Name + " " + damage + " " + mod + " " + weapon);

            try
            {
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception send_exception)
            {

            }

            base.OnPlayerDamage(player, inflictor, attacker, damage, dFlags, mod, weapon, point, dir, hitLoc);
        }

        public override void OnSay(Entity player, string name, string message)
        {
            byte[] send_buffer = Encoding.ASCII.GetBytes(message);

            try
            {
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception send_exception)
            {
                Log.Write(LogLevel.All, send_exception.ToString());
            }

            base.OnSay(player, name, message);
        }

        public void ThreadStart()
        {
            udp();
        }



        public override void OnExitLevel()
        {
            listener.Close();
            base.OnExitLevel();
        }

    }
}

