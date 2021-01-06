﻿namespace LearnMe.Infrastructure.Data
{
    public class MessageParams
    {
        public const int MaxPageSize = 48;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 24;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string Email { get; set; }
        public string MessageContainer { get; set; } = "Nieprzeczytane";
    }
}
