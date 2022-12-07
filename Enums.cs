using System;

namespace Cromatix.MP4Reader
{
    public enum SampleType
    {
        STRING_ASCII = 'c', //single byte 'c' style character string
        SIGNED_BYTE = 'b',//single byte signed number
        UNSIGNED_BYTE = 'B', //single byte unsigned number
        SIGNED_SHORT = 's',//16-bit integer
        UNSIGNED_SHORT = 'S',//16-bit integer
        FLOAT = 'f', //32-bit single precision float (IEEE 754)
        FOURCC = 'F', //32-bit four character tag 
        SIGNED_LONG = 'l',//32-bit integer
        UNSIGNED_LONG = 'L', //32-bit integer
        Q15_16_FIXED_POINT = 'q', // Q number Q15.16 - 16-bit signed integer (A) with 16-bit fixed point (B) for A.B value (range -32768.0 to 32767.99998). 
        Q31_32_FIXED_POINT = 'Q', // Q number Q31.32 - 32-bit signed integer (A) with 32-bit fixed point (B) for A.B value. 
        SIGNED_64BIT_INT = 'j', //64 bit signed long
        UNSIGNED_64BIT_INT = 'J', //64 bit unsigned long	
        DOUBLE = 'd', //64 bit double precision float (IEEE 754)
        STRING_UTF8 = 'u', //UTF-8 formatted text string.  As the character storage size varies, the size is in bytes, not UTF characters.
        UTC_DATE_TIME = 'U', //128-bit ASCII Date + UTC Time format yymmddhhmmss.sss - 16 bytes ASCII (years 20xx covered)
        GUID = 'G', //128-bit ID (like UUID)

        COMPLEX = '?', //for sample with complex data structures, base size in bytes.  Data is either opaque, or the stream has a TYPE structure field for the sample.
        COMPRESSED = '#', //Huffman compression STRM payloads.  4-CC <type><size><rpt> <data ...> is compressed as 4-CC '#'<new size/rpt> <type><size><rpt> <compressed data ...>

        NEST = 0, // used to nest more GPMF formatted metadata 

        /* ------------- Internal usage only ------------- */
        EMPTY = 0xfe, // used to distinguish between grouped metadata (like FACE) with no data (no faces detected) and an empty payload (FACE device reported no samples.)
        ERROR = 0xff // used to report an error
    }

    public enum ExportFormat
    { 
        GPX,
        VIRB,
        CSV
    }
}
