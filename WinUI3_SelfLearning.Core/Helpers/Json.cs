using Newtonsoft.Json;

namespace WinUI3_SelfLearning.Core.Helpers;

public static class Json
{
    /// <summary>
    /// 反序列化JSON字符串为对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static async Task<T> ToObjectAsync<T>(string value)
    {
        return await Task.Run<T>(() =>
        {
            return JsonConvert.DeserializeObject<T>(value);
        });
    }

    /// <summary>
    /// 序列化对象为JSON字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static async Task<string> StringifyAsync(object value)
    {
        return await Task.Run<string>(() =>
        {
            return JsonConvert.SerializeObject(value);
        });
    }
}
