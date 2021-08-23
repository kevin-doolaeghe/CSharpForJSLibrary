using System;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace SerialTransmitter {

    class SerialCom {

        private class SerialException : Exception {

            public SerialException() : base() { }

            public SerialException(string message) : base(message) { }

            public SerialException(string message, Exception inner) : base(message, inner) { }

        }

        private class Com {

            private SerialPort port;

            public Com()
            {
                this.port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            }

            public Com(String port, int speed, int parity, int data, int stop)
            {
                this.port = new SerialPort(port, speed, (Parity)parity, data, (StopBits)stop);
            }

            public void Open()
            {
                try
                {
                    port.Open();
                }
                catch (IOException e)
                {
                    throw new SerialException(e.Message);
                }
            }

            public void Close()
            {
                port.Close();
            }

            public String ReadAll()
            {
                return port.ReadExisting();
            }

            public String ReadLine()
            {
                return port.ReadLine();
            }

            public void Write(String data)
            {
                port.Write(data);
            }

            public String GetPortName()
            {
                return port.PortName;
            }

        }

        public string Send(string message, string serialPort, int baudRate, int parity, int dataBits, int StopBits)
        {
            string result = String.Empty;

            Com com = new Com(serialPort, baudRate, parity, dataBits, StopBits);
            try
            {
                com.Open();

                try
                {
                    ASCIIEncoding ascii = new ASCIIEncoding();
                    String decodedString = ascii.GetString(ascii.GetBytes(message));
                    com.Write(decodedString);
                    result = "Data sent: " + decodedString;
                }
                catch (Exception e)
                {
                    result = "Unexpected exception : " + e.ToString();
                }

            }
            catch (SerialException e)
            {
                result = "Can't open port " + com.GetPortName() + "!";
            }
            finally
            {
                com.Close();
            }

            return result;
        }

    }

}