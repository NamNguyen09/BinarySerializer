using System;
using System.Runtime.Serialization;

namespace cx.BinarySerializer.EFCache.Tables
{
    /// <summary>
    ///     TableColumn's Info
    /// </summary>
    [Serializable]
    [DataContract]
    public class EFCoreTableColumnInfo
    {
        /// <summary>
        ///     The column's ordinal.
        /// </summary>
        [DataMember]
        public int Ordinal { get; set; }

        /// <summary>
        ///     The column's name.
        /// </summary>
        [DataMember]
        public string Name { get; set; } = "";

        /// <summary>
        ///     The column's DbType Name.
        /// </summary>
        [DataMember]
        public string DbTypeName { get; set; } = "";

        /// <summary>
        ///     The column's Type.
        /// </summary>
        [DataMember]
        public string TypeName { get; set; } = "";

        /// <summary>
        ///     ToString
        /// </summary>
        public override string ToString() =>
            $"Ordinal: {Ordinal}, Name: {Name}, DbTypeName: {DbTypeName}, TypeName= {TypeName}.";
    }
}