using System.Text;

using Newtonsoft.Json;

using WinUI3_SelfLearning.Core.Contracts.Services;

namespace WinUI3_SelfLearning.Core.Services;

public class FileService : IFileService
{
    /// <summary>
    /// 读取JSON文件，返回反序列化后的对象。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="folderPath"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public T Read<T>(string folderPath, string fileName)
    {
        var path = Path.Combine(folderPath, fileName);
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        return default;
    }

    /// <summary>
    /// 把对象序列化写入到JSON文件中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="folderPath">写入文件所在的文件夹</param>
    /// <param name="fileName">文件名称，要指定扩展名</param>
    /// <param name="content">泛型对象，也就是要写入的内容</param>
    public void Save<T>(string folderPath, string fileName, T content)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fileContent = JsonConvert.SerializeObject(content);
        File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="folderPath"></param>
    /// <param name="fileName"></param>
    public void Delete(string folderPath, string fileName)
    {
        if (fileName != null && File.Exists(Path.Combine(folderPath, fileName)))
        {
            File.Delete(Path.Combine(folderPath, fileName));
        }
    }
}
