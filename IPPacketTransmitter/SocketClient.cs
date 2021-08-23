using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace IPPacketTransmitter {

    public class SocketClient {

        public string SendAndWaitResponse(string message, string ip, int port)
        {
            string result = String.Empty;

            try
            {
                // IPHostEntry host = Dns.GetHostEntry(ip);
                // IPAddress ipAddress = host.AddressList[0];
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, port);

                Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEndpoint);

                    byte[] msg = Encoding.ASCII.GetBytes(message + "\r\n");
                    int bytesSent = socket.Send(msg);

                    byte[] bytes = new byte[1024];
                    int bytesRec = socket.Receive(bytes);

                    result = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (ArgumentNullException e)
                {
                    result = "ArgumentNullException : " + e.ToString();
                }
                catch (SocketException e)
                {
                    result = "SocketException : " + e.ToString();
                }
                catch (Exception e)
                {
                    result = "Unexpected exception : " + e.ToString();
                }
            }
            catch (Exception e)
            {
                result = "Unexpected exception : " + e.ToString();
            }

            return result;
        }

        public string Send(string message, string ip, int port)
        {
            string result = String.Empty;

            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, port);

                Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEndpoint);

                    byte[] msg = Encoding.ASCII.GetBytes(message + "\r\n");
                    int bytesSent = socket.Send(msg);

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (ArgumentNullException e)
                {
                    result = "ArgumentNullException : " + e.ToString();
                }
                catch (SocketException e)
                {
                    result = "SocketException : " + e.ToString();
                }
                catch (Exception e)
                {
                    result = "Unexpected exception : " + e.ToString();
                }
            }
            catch (Exception e)
            {
                result = "Unexpected exception : " + e.ToString();
            }

            return result;
        }

    }

}
