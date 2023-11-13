﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;

namespace cx.BinarySerializer.EFCache.Tables
{
    /// <summary>
    ///     TableRows structure
    /// </summary>
    [Serializable]
    [DataContract]
    public class EFCoreTableRows
    {
        /// <summary>
        ///     TableRows structure
        /// </summary>
        public EFCoreTableRows(DbDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            ColumnsInfo = new Dictionary<int, EFCoreTableColumnInfo>(reader.FieldCount);
            for (var i = 0; i < reader.FieldCount; i++)
            {
                ColumnsInfo.Add(i, new EFCoreTableColumnInfo
                {
                    Ordinal = i,
                    Name = reader.GetName(i),
                    DbTypeName = reader.GetDataTypeName(i) ?? typeof(string).ToString(),
                    TypeName = reader.GetFieldType(i)?.ToString() ?? typeof(string).ToString(),
                });
            }
        }

        /// <summary>
        ///     TableRows structure
        /// </summary>
        public EFCoreTableRows() => ColumnsInfo = new Dictionary<int, EFCoreTableColumnInfo>();

        /// <summary>
        ///     Rows of the table
        /// </summary>
        [DataMember]
        public IList<EFCoreTableRow> Rows { get; set; } = new List<EFCoreTableRow>();

        /// <summary>
        ///     TableColumn's Info
        /// </summary>
        [DataMember]
        public IDictionary<int, EFCoreTableColumnInfo> ColumnsInfo { get; set; }

        /// <summary>
        ///     Gets the number of columns in the current row.
        /// </summary>
        [DataMember]
        public int FieldCount { get; set; }

        /// <summary>
        ///     EFTableRows's unique ID
        /// </summary>
        [DataMember]
        public string TableName { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        ///     Gets a value that indicates whether the SqlDataReader contains one or more rows.
        /// </summary>
        [DataMember]
        public bool HasRows => Rows?.Count > 0;

        /// <summary>
        ///     Gets the number of rows changed, inserted, or deleted by execution of the Transact-SQL statement.
        /// </summary>
        [DataMember]
        public int RecordsAffected { get; } = -1;

        /// <summary>
        ///     Gets the number of fields in the SqlDataReader that are not hidden.
        /// </summary>
        [DataMember]
        public int VisibleFieldCount { get; set; }

        /// <summary>
        ///     Number of Db rows.
        /// </summary>
        [DataMember]
        public int RowsCount => Rows?.Count ?? 0;

        /// <summary>
        ///     Gets or sets the Get(index)
        /// </summary>
        public EFCoreTableRow this[int index]
        {
            get => Get(index);
            set => Rows[index] = value;
        }

        /// <summary>
        ///     Adds an item to the EFTableRows
        /// </summary>
        public void Add(EFCoreTableRow item)
        {
            if (item != null)
            {
                Rows.Add(item);
            }
        }

        /// <summary>
        ///     returns the value of the given index.
        /// </summary>
        public EFCoreTableRow Get(int index) => Rows[index];

        /// <summary>
        ///     Gets the column ordinal, given the name of the column.
        /// </summary>
        public int GetOrdinal(string name)
        {
            var keyValuePair =
                ColumnsInfo.FirstOrDefault(pair => pair.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (keyValuePair.Value != null)
            {
                return keyValuePair.Value.Ordinal;
            }

            throw new ArgumentOutOfRangeException(nameof(name), name);
        }

        /// <summary>
        ///     Gets the name of the specified column.
        /// </summary>
        public string GetName(int ordinal) => GetColumnInfo(ordinal).Name;

        /// <summary>
        ///     Gets a string representing the data type of the specified column.
        /// </summary>
        public string GetDataTypeName(int ordinal) => GetColumnInfo(ordinal).DbTypeName;

        /// <summary>
        ///     Gets the Type that is the data type of the object.
        /// </summary>
        public Type GetFieldType(int ordinal) => Type.GetType(GetColumnInfo(ordinal).TypeName) ?? typeof(string);

        /// <summary>
        ///     Gets the Type that is the data type of the object.
        /// </summary>
        public string GetFieldTypeName(int ordinal) => GetColumnInfo(ordinal).TypeName;

        private EFCoreTableColumnInfo GetColumnInfo(int ordinal)
        {
            var dbColumnInfo = ColumnsInfo[ordinal];
            if (dbColumnInfo != null)
            {
                return dbColumnInfo;
            }

            throw new ArgumentOutOfRangeException(nameof(ordinal), $"Index[{ordinal}] was outside of array's bounds.");
        }
    }
}