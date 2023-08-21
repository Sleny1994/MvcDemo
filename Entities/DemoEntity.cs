namespace MvcDemo.Entities
{
    public class DemoEntity
    {
        /// <summary>
        /// 主键唯一标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 电影名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// 主角
        /// </summary>
        public string LeadingRole { get; set; }

        /// <summary>
        /// 电影类型
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// 票价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 录入人员
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastEditTime { get; set; }

        /// <summary>
        /// 最后编辑人员
        /// </summary>
        public string LastEditUser { get; set; }
    }
}
