﻿using System;

namespace cx.BinarySerializer.EFCache.Tables
{
    /// <summary>
    ///     A Table's EntityInfo
    /// </summary>
    public class TableEntityInfo
    {
        /// <summary>
        ///     Gets the CLR class that is used to represent instances of this type.
        ///     Returns null if the type does not have a corresponding CLR class (known as a shadow type).
        /// </summary>
        public Type ClrType { set; get; } = null;

        /// <summary>
        ///     The Corresponding table's name.
        /// </summary>
        public string TableName { set; get; } = "";

        /// <summary>
        ///     Debug info.
        /// </summary>
        public override string ToString() => $"{ClrType}::{TableName}";
    }
}