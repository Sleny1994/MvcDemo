namespace MvcDemo.Interfaces
{
    public interface ISerializeHelper
    {
        /// 序列化
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="path">序列化后保存路径</param>
        void Serialize<T>(T t, string path) where T : class;

        /// 反序列化
        /// <typeparam name="T"></typeparam>
        /// <param name="path">反序列化文件路径</param>
        /// <returns></returns>
        T Deserialize<T>(string path) where T : class;
    }
}
