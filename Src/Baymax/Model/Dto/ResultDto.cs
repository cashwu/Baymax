using System;
using System.Collections.Generic;
using Baymax.Model.Enum;

namespace Baymax.Model.Dto
{
    public class ResultDto<T> where T : class
    {
        public ResultDto()
        {
        }

        public ResultDto(T data)
        {
            Result = data;
        }

        /// <summary>
        /// 錯誤內容
        /// </summary>
        public IList<ErrorDto> Error { set; get; } = new List<ErrorDto>();

        /// <summary>
        /// 回應代碼：預設400
        /// </summary>
        public int Code { set; get; } = EnumHttpStatus.OK.Value;

        /// <summary>
        /// 回傳物件：可以是訊息，物件
        /// </summary>
        public T Result { set; get; }

        /// <summary>
        /// 美東時間
        /// </summary>
        public DateTime ReplyTime { get; } = DateTime.Now;
    }
}