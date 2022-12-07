using System;
using System.Linq;

namespace Cromatix.MP4Reader
{
    public class GPMFStream : IDisposable
    {
        public byte[] Content;
        public int Position;
        public int NestLevel;
        public int[] NestSize;
        public int DeviceCount;
        public int DeviceId;
        public string DeviceName;

        public readonly int NESTLIMIT = 16;

        public int Length
        {
            get
            {
                if (Content != null)
                    return Content.Length;
                else
                    return 0;
            }
        }

        public string FourCC
        {
            get
            {
                if (Content != null)
                {
                    return ByteUtil.FourCCFomBytes(Content, Position);
                }

                return string.Empty;
            }
        }

        public SampleType Type
        {
            get
            {
                if (Content != null)
                {
                    SampleType type = (SampleType)(Content[Position + 4] & 0xff);

                    if (type == SampleType.COMPRESSED && Content[Position + 8] < Length)
                    {
                        return (SampleType)(Content[Position + 8] & 0xff);
                    }

                    return type;
                }

                return SampleType.ERROR;
            }
        }

        public int Repeat
        {
            get
            {
                if (Content != null)
                {
                    uint bytes32 = ByteUtil.BytesToInt(Content, Position + 4);
                    int repeat = Samples(bytes32);
                    SampleType type = (SampleType)(Content[Position + 4] & 0xff);

                    if (type == SampleType.COMPRESSED && Content[Position + 4] < Length)
                    {
                        repeat = Samples(Content[Position + 8]);
                    }
                    return repeat;
                }

                return 0;
            }
        }

        public int StructSize
        {
            get
            {
                if (Content != null)
                {
                    uint bytes32 = ByteUtil.BytesToInt(Content, Position + 4);
                    int ssize = SampleSize(bytes32);

                    SampleType type = (SampleType)(Content[Position + 4] & 0xff);

                    if (type == SampleType.COMPRESSED && Position + 8 < Length)
                    {
                        ssize = SampleSize(Content[Position + 8]);
                    }

                    return ssize;
                }

                return 0;
            }
        }

        public int DataSize(uint num)
        {
            return (SampleSize(num) * Samples(num) + 3) & ~0x3;
        }

        public byte[] GetRawData(int size)
        {
            if (Content != null)
            {
                var byteSpan = new ReadOnlySpan<byte>(Content, Position + 8, size);
                return byteSpan.ToArray();
            }

            return null;
        }

        public GPMFStream(byte[] _buffer)
        {
            Content = _buffer;
            NestSize = new int[NESTLIMIT];
        }

        public int SampleSize(uint num)
        {
            return ((int)(num >> 8)) & 0xff;
        }

        private int Samples(uint num)
        {
            return (((int)(num >> 24)) & 0xff) | (((int)(num >> 16) & 0xff) << 8);
        }

        private void Reset()
        {
            Position = 0;
            NestLevel = 0;
            Content = null;
            NestSize.ToList().ForEach(x => x = 0);

            GC.Collect();
        }

        public void Dispose()
        {
            Reset();
        }
    }
}
