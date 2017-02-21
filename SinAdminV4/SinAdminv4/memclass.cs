
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemHax
{
    class memoryHax
    {

        public IntPtr pHandel;

        public bool Process_Handle(string ProcessName)
        {
            try
            {
                Process[] processesByName = Process.GetProcessesByName(ProcessName);
                if (processesByName.Length == 0)
                {
                    return false;
                }
                pHandel = processesByName[0].Handle;
                return true;
            }
            catch (Exception exception)
            {
                Console.Beep();
                Console.WriteLine("Process_Handle - " + exception.Message);
                return false;
            }
        }

        public byte[] Read(int Address, int Length)
        {
            byte[] buffer = new byte[Length];
            IntPtr zero = IntPtr.Zero;
            ReadProcessMemory(pHandel, (IntPtr)Address, buffer, (uint)buffer.Length, out zero);
            return buffer;
        }

        public byte[] ReadBytes(int Address, int Length)
        {
            return Read(Address, Length);
        }

        public int ReadInteger(int Address, [Optional, DefaultParameterValue(4)] int Length)
        {
            return BitConverter.ToInt32(Read(Address, Length), 0);
        }

        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesWritten);
        public string ReadString(int Address, [Optional, DefaultParameterValue(4)] int Length)
        {
            return new ASCIIEncoding().GetString(Read(Address, Length));
        }

        public void Write(int Address, int Value)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            IntPtr zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, bytes, (uint)bytes.Length, out zero);
        }

        public void Writel(int Address, long Value)
        {



            byte[] bytes = BitConverter.GetBytes(Value);
            IntPtr zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, bytes, (uint)bytes.Length, out zero);
        }


        public void WriteBytes(int Address, byte[] Bytes)
        {
            IntPtr zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Bytes, (uint)Bytes.Length, out zero);
        }

        public void WriteInteger(int Address, int Value)
        {
            Write(Address, Value);
        }

        public void WriteLong(int Address, long Value)
        {
            Writel(Address, Value);
        }

        public void WriteNOP(int Address)
        {
            byte[] buffer = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 };
            IntPtr zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, buffer, (uint)buffer.Length, out zero);
        }

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesWritten);
        public void WriteString(int Address, string Text)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(Text);
            IntPtr zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, bytes, (uint)bytes.Length, out zero);
        }

    }
}
