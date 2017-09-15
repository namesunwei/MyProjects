using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chris.Blog.Services.DomainModels
{
    public class ArticleDomain
    {
        public int CategoryId { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ModifyAt { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }
    }
}
