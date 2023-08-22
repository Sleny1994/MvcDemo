using ProtoBuf;

namespace MvcDemo.Models
{
    /// 个人信息
    [ProtoContract]
    [Serializable]
    public class Person
    {
        /// 唯一标识
        [ProtoMember(1)]
        public int Id { get; set; }

        /// 姓名
        [ProtoMember(2)]
        public string Name { get; set; }

        /// 生日
        [ProtoMember(3)]
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return $"Id={Id},Name={Name},Birthday={Birthday.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
        }
    }
}
