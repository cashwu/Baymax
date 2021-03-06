using System;
using System.Collections.Generic;
using System.Linq;
using Baymax.Util;

namespace Baymax.Model.Enum
{
    public class EnumErrorStatus : Enumeration<EnumErrorStatus>
    {
        /// <summary>
        /// 格式不正確
        /// </summary>
        public static readonly EnumErrorStatus FORMAT_INVALID
                = new EnumErrorStatus(1, "The request format is invalid");

        /// <summary>
        /// 無此資料
        /// </summary>
        public static readonly EnumErrorStatus DATA_NOT_FOUND
                = new EnumErrorStatus(2, "The data is not found in database");

        /// <summary>
        /// 資料重覆
        /// </summary>
        public static readonly EnumErrorStatus DATA_EXISTED
                = new EnumErrorStatus(3, "The data has exist in database");

        /// <summary>
        /// 資料驗證失敗
        /// </summary>
        public static readonly EnumErrorStatus DATA_INVALID
                = new EnumErrorStatus(4, "The data is invalid");

        /// <summary>
        /// 權限不足
        /// </summary>
        public static readonly EnumErrorStatus PERMISSION_DENIED
                = new EnumErrorStatus(5, "You have no permission to operate");

        private EnumErrorStatus(int value, string displayName)
                : base(value, displayName)
        {
        }
    }
}