namespace Diablo3Kit.Service;

[DataContract]
internal abstract class ServiceFile
{
    /// <summary>
    /// 设置项保存的文件夹的路径
    /// </summary>
    internal abstract string DirectoryPath { get; set; }

    /// <summary>
    /// 设置项保存的文件的名字，不包括路径
    /// </summary>
    internal abstract string FileName { get; set; }


    private string FullFile => Path.Combine(DirectoryPath, FileName);

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="isEncrypt">是否加密</param>
    public virtual void Save()
    {
        try
        {
            if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);

            string str = this.JsonSerialize();
 
            File.WriteAllText(FullFile, str);

        }
        catch (Exception ex)
        {
            //Logger.Error($"{FileName} Save: ", ex);
        }

    }

    /// <summary>
    /// 加载 
    /// </summary>
    /// <param name="isDecrypt">是否解密</param>
    public virtual void Load()
    {
        if (!File.Exists(FullFile)) return;

        try
        {
            string str = File.ReadAllText(FullFile);
 
            var instance = str.JsonDerialize<ServiceFile>();

            //复制所有的公共属性
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var p in properties)
            {
                if (p.CanWrite)
                {
                    var value = p.GetValue(instance);

                    p.SetValue(this, value);
                }
            }
        }
        catch (Exception ex)
        {
            File.Delete(FullFile);

            //Logger.Error($"{FileName} load: ", ex);
        }
    }
}