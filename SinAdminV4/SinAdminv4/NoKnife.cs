using System;
using System.Runtime.InteropServices;
using InfinityScript;
using System.IO;

//Noknife from http://www.itsmods.com/forum/Thread-Release-NoKnife-Plugin.html
//credits to:
//base.ServerPrint("\n NoKnife Plugin loaded \n Author: zxz0O0 \n Thanks to Nukem, Jariz, Pozzuh and Makavel\n");
//base.ServerPrint(" www.youtube.com/zxz0O0 \n www.itsmods.com\n");

//Adapted by DaMacc for IW5M

namespace noKnife
{
    internal unsafe class NoKnife
    {
        public readonly int DefaultKnifeAddress;
        public readonly int* KnifeRange;
        public readonly int* ZeroAddress;

        public NoKnife()
        {
            Log.Write(LogLevel.Info,
                      "NoKnife Plugin loaded \n Author: zxz0O0 Thanks to Nukem, Jariz, Pozzuh and Makavel\n Modified for IW5M by DaMaccn||SinAdmin Fix by Bio");



            try
            {
                var search = new byte?[]
                                 {
                                     0x8b, null, null, null, 0x83, null, 4, null, 0x83, null, 12, 0xd9, null, null, null
                                     , 0x8b,
                                     null, 0xd9, null, null, null, 0xd9, 5
                                 };
                KnifeRange = (int*)(FindMem(search, 1, 0x400000, 0x500000) + search.Length);

                //   Log.Write(LogLevel.Info, "KnifeRange ptr: " + string.Format("{0:X}", (int) KnifeRange));

                if ((int)KnifeRange == search.Length)
                {
                    var nullableArray2 = new byte?[]
                                             {
                                                 0x8b, null, null, null, 0x83, null, 0x18, null, 0x83, null, 12, 0xd9,
                                                 null, null, null, 0x8d,
                                                 null, null, null, 0xd9, null, null, null, 0xd9, 5
                                             };
                    KnifeRange = (int*)(FindMem(nullableArray2, 1, 0x400000, 0x500000) + nullableArray2.Length);

                    //Log.Write(LogLevel.Info, "KnifeRange ptr: " + string.Format("{0:X}", (int) KnifeRange));

                    if ((int)KnifeRange == nullableArray2.Length)
                    {
                        KnifeRange = (int*)0;
                    }
                }
                DefaultKnifeAddress = *KnifeRange;

                var nullableArray3 = new byte?[]
                                         {
                                             0xd9, 0x5c, null, null, 0xd8, null, null, 0xd8, null, null, 0xd9, 0x5c,
                                             null, null, 0x83, null,
                                             1, 15, 0x86, null, 0, 0, 0, 0xd9
                                         };
                ZeroAddress = (int*)(FindMem(nullableArray3, 1, 0x400000, 0x500000) + nullableArray3.Length + 2);

                //   Log.Write(LogLevel.Info, "ZeroAddress ptr: " + string.Format("{0:X}", (int) ZeroAddress));

                if ((((int)KnifeRange == 0) || (DefaultKnifeAddress == 0)) || ((int)ZeroAddress == 0))
                {
                    Log.Write(LogLevel.Error, "Error finding address: NoKnife Plugin will not work");
                }
                else
                {
                    uint num;
                    VirtualProtect((IntPtr)KnifeRange, (IntPtr)4, 0x40, out num);
                }
            }
            catch (Exception exception)
            {
                Log.Write(LogLevel.Error, "Error in NoKnife Plugin. Plugin will not work.");
                Log.Write(LogLevel.Error, exception.ToString());
            }
        }

        public void DisableKnife()
        {
            try
            {
                int x = (int)ZeroAddress;
                if (!(DefaultKnifeAddress.ToString() == x.ToString()))
                {
                    File.WriteAllText(@"scripts\\SinAdmin\\KnifeAddress.txt", String.Empty);
                    StreamWriter write = new StreamWriter(@"scripts\\SinAdmin\\KnifeAddress.txt");
                    write.WriteLine(DefaultKnifeAddress.ToString(), true);
                    Utilities.RawSayAll(DefaultKnifeAddress.ToString());
                    write.Close();
                    *KnifeRange = (int)ZeroAddress;
                    File.WriteAllText(@"scripts\\SinAdmin\\ZeroAddress.txt", String.Empty);
                    StreamWriter write2 = new StreamWriter(@"scripts\\SinAdmin\\KnifeAddress.txt");
                    write2.WriteLine(DefaultKnifeAddress.ToString(), true);
                    Utilities.RawSayAll(x.ToString());
                    write2.Close();
                }
            }
            catch (Exception exception) { Log.Write(LogLevel.Error, exception.ToString()); }
        }

        public void EnableKnife()
        {
            try
            {
                StreamReader read = new StreamReader(@"scripts\\SinAdmin\\KnifeAddress.txt");
                string def = read.ReadLine();
                StreamReader read2 = new StreamReader(@"scripts\\SinAdmin\\ZeroAddress.txt");
                string fed = read2.ReadLine();
                read2.Close();
                read.Close();
                if (!(def == fed))
                {
                    *KnifeRange = Convert.ToInt32(def);
                }
                //fs.Close();
                Utilities.RawSayAll(def);
            }
            catch (Exception exception) { Log.Write(LogLevel.Error, exception.ToString()); }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtect(IntPtr lpAddress, IntPtr dwSize, uint flNewProtect,
                                                  out uint lpflOldProtect);

        public int FindMem(byte?[] search, int num = 1, int start = 0x1000000, int end = 0x3d00000)
        {
            var num2 = (byte*)0;
            try
            {
                int num3 = 0;
                for (int i = start; i < end; i++)
                {
                    num2 = (byte*)i;
                    bool flag = false;
                    for (int j = 0; j < search.Length; j++)
                    {
                        if (search[j].HasValue)
                        {
                            byte num7 = *num2;
                            if (num7 != search[j])
                            {
                                break;
                            }
                        }
                        if (j == (search.Length - 1))
                        {
                            if (num == 1)
                            {
                                flag = true;
                            }
                            else
                            {
                                num3++;
                                if (num3 == num)
                                {
                                    flag = true;
                                }
                            }
                        }
                        else
                        {
                            num2++;
                        }
                    }
                    if (flag)
                    {
                        return i;
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Write(LogLevel.Error, "FindMem: " + exception.Message + "\nAddress: " + (int)num2);
            }
            return 0;
        }
    }
}

