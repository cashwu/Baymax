using System;
using System.Collections.Generic;
using System.Linq;
using Baymax.Util;

namespace Baymax.Model.Enum
{
    public class EnumHttpStatus : Enumeration<EnumHttpStatus>
    {
        /// <summary>
        /// 400
        /// 參數不正確、服務無法理解
        /// </summary>
        public static readonly EnumHttpStatus BAD_REQUEST
            = new EnumHttpStatus(400, "Bad Request");
        
        /// <summary>
        /// 401
        /// 驗證錯誤
        /// </summary>
        public static readonly EnumHttpStatus UNAUTHORIZED
                = new EnumHttpStatus(401, "Unauthorized");

        /// <summary>
        ///403
        /// 拒絕存取
        /// </summary>
        public static readonly EnumHttpStatus FORBIDDEN
                = new EnumHttpStatus(403, "Forbidden");

        /// <summary>
        /// 404
        /// 找不到資源
        /// </summary>
        public static readonly EnumHttpStatus NOT_FOUND
                = new EnumHttpStatus(404, "Not Found");

        /// <summary>
        /// 500
        /// 伺服器發生錯誤
        /// </summary>
        public static readonly EnumHttpStatus INTERNAL_SERVER_ERROR
                = new EnumHttpStatus(500, "Internal Server Error");

        /// <summary>
        /// 503
        /// 暫時無法服務
        /// </summary>
        public static readonly EnumHttpStatus SERVICE_UNAVAILABLE
                = new EnumHttpStatus(503, "Service unavailable");

        /// <summary>
        /// 200
        /// 正確無誤
        /// </summary>
        public static readonly EnumHttpStatus OK
                = new EnumHttpStatus(200, "OK");

        /// <summary>
        /// 201
        /// 請求已被實現
        /// </summary>
        public static readonly EnumHttpStatus CREATED
                = new EnumHttpStatus(201, "Created");

        /// <summary>
        /// 204
        /// 成功處理請求
        /// </summary>
        public static readonly EnumHttpStatus NO_CONTENT
                = new EnumHttpStatus(204, "No Content");

        private EnumHttpStatus(int value, string displayName)
                : base(value, displayName)
        {
        }
    }
}