using System;
using System.Linq;
using System.Text;

namespace Cromatix.MP4Reader
{
    internal static class ByteUtil
    {
        internal static string FourCCFomBytes(byte[] buffer, int pos)
        {
            try
            {
                uint number = BytesToInt(buffer, pos);
                return IntToString(number, false);
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static string FourCCFromInt(uint number)
        {
            string res = IntToString(number, false);

            if (res != null)
                res = res.Trim();

            return res;
        }

        internal static bool IsValidFourCC(string fourCC)
        {
            if (string.IsNullOrEmpty(fourCC) || string.IsNullOrWhiteSpace(fourCC))
                return false;

            if (fourCC.Length != 4)
                return false;

            for (int i = 0; i < fourCC.Length; i++)
            {
                if (char.IsLetterOrDigit(fourCC[i]))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        internal static string IntToString(uint value, bool revese = true)
        {
            var bytes = BitConverter.GetBytes(value);
            return BytesToString(bytes, revese);
        }

        internal static string BytesToString(byte[] array, bool revese = true)
        {
            if (revese)
                array = array.Reverse().ToArray();

#if NETSTANDARD1_3
                return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
#else
            return Encoding.ASCII.GetString(array);
#endif
        }

        internal static uint BytesToInt(byte[] array, int startPos)
        {
            var byteSpan = new ReadOnlySpan<byte>(array, startPos, 4);
            return BitConverter.ToUInt32(byteSpan);
        }

        internal static ushort BytesToShort(byte[] array, int startPos)
        {
            var byteSpan = new ReadOnlySpan<byte>(array, startPos, 2);
            return BitConverter.ToUInt16(byteSpan);
        }

        internal static int HexToInt(int num)
        {
            return ((num & 0xff) << 24) | ((num & 0xff00) << 8) | ((num >> 8) & 0xff00) | ((num >> 24) & 0xff);
        }

        internal static short GetShort(GPMFStream gpmf)
        {
            var bytes = new ReadOnlySpan<byte>(gpmf.Content, gpmf.Position + 8, gpmf.StructSize).ToArray();
            return ByteUtil.IntToShort(ByteUtil.BytesToShort(bytes, 0));
        }

        internal static int GetInt(GPMFStream gpmf)
        {
            var bytes = new ReadOnlySpan<byte>(gpmf.Content, gpmf.Position + 8, gpmf.StructSize).ToArray();

            if (BitConverter.IsLittleEndian)
                bytes = bytes.Reverse().ToArray();

            return BitConverter.ToInt32(bytes, 0);
        }

        private static short IntToShort(int num)
        {
            return (short)((((num) >> 8) & 0xff) | (((num) << 8) & 0xff00));
        }
    }
}
