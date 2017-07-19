using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialForms.Demo.Models
{
    public class DataTypes
    {
        private string s;

        public string String
        {
            get { return s; }
            set
            {
                s = value;
            }
        }

        public DateTime DateTime { get; set; }

        public bool Bool { get; set; }

        public char Char { get; set; }

        public short ShortInt { get; set; }

        public int Int { get; set; }

        public long LongInt { get; set; }

        public ushort UnsignedShortInt { get; set; }

        public uint UnsignedInt { get; set; }

        public ulong UnsignedLongInt { get; set; }

        public byte Byte { get; set; }

        public sbyte SignedByte { get; set; }

        public float Float { get; set; }

        public double Double { get; set; }

        public decimal Decimal { get; set; }

        public DateTime? NullableDateTime { get; set; }

        public bool? NullableBool { get; set; }

        public char? NullableChar { get; set; }

        public short? NullableShortInt { get; set; }

        public int? NullableInt { get; set; }

        public long? NullableLongInt { get; set; }

        public ushort? NullableUnsignedShortInt { get; set; }

        public uint? NullableUnsignedInt { get; set; }

        public ulong? NullableUnsignedLongInt { get; set; }

        public byte? NullableByte { get; set; }

        public sbyte? NullableSignedByte { get; set; }

        public float? NullableFloat { get; set; }

        public double? NullableDouble { get; set; }

        public decimal? NullableDecimal { get; set; }
    }
}
