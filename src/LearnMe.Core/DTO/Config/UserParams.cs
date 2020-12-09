using System;
using System.Collections.Generic;
using System.Text;

namespace LearnMe.Core.DTO.Config
{
    public class UserParams
    {
        
        public const int MaxPageSize = 48;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 24;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        
    }
}
