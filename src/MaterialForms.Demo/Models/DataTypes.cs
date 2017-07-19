using System;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo.Models
{
    [Form(Grid = new[] { 1d, 1d, 1d })]
    public class DataTypes
    {
        [Field(Row = "1")]
        public string String { get; set; }

        [Field(Row = "1")]
        public DateTime DateTime { get; set; }

        [Field(Row = "1")]
        public bool Bool { get; set; }

        [Field(Row = "2")]
        public char Char { get; set; }

        [Field(Row = "2")]
        public short ShortInt { get; set; }

        [Field(Row = "2")]
        public int Int { get; set; }

        [Field(Row = "3")]
        public long LongInt { get; set; }

        [Field(Row = "3")]
        public ushort UnsignedShortInt { get; set; }

        [Field(Row = "3")]
        public uint UnsignedInt { get; set; }

        [Field(Row = "4")]
        public ulong UnsignedLongInt { get; set; }

        [Field(Row = "4")]
        public byte Byte { get; set; }

        [Field(Row = "4")]
        public sbyte SignedByte { get; set; }

        [Field(Row = "5")]
        public float Float { get; set; }

        [Field(Row = "5")]
        public double Double { get; set; }

        [Field(Row = "5")]
        public decimal Decimal { get; set; }

        [Field(Row = "6")]
        public DateTime? NullableDateTime { get; set; }

        [Field(Row = "6")]
        public bool? NullableBool { get; set; }

        [Field(Row = "6")]
        public char? NullableChar { get; set; }

        [Field(Row = "7")]
        public short? NullableShortInt { get; set; }

        [Field(Row = "7")]
        public int? NullableInt { get; set; }

        [Field(Row = "7")]
        public long? NullableLongInt { get; set; }

        [Field(Row = "8")]
        public ushort? NullableUnsignedShortInt { get; set; }

        [Field(Row = "8")]
        public uint? NullableUnsignedInt { get; set; }

        [Field(Row = "8")]
        public ulong? NullableUnsignedLongInt { get; set; }

        [Field(Row = "9")]
        public byte? NullableByte { get; set; }

        [Field(Row = "9")]
        public sbyte? NullableSignedByte { get; set; }

        [Field(Row = "9")]
        public float? NullableFloat { get; set; }

        [Field(Row = "10")]
        public double? NullableDouble { get; set; }

        [Field(Row = "10")]
        public decimal? NullableDecimal { get; set; }
    }
}
