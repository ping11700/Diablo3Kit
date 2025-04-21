namespace Diablo3Kit.Data;


[DataContract]
internal class Config : ServiceFile
{
    internal override string DirectoryPath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");

    internal override string FileName { get; set; } = "Config";

    /// <summary>
    /// Synergies
    /// </summary>

    [DataMember] public IEnumerable<SkillData> SkillDatas { get; set; } = [new SkillData(), new SkillData()];

}


/// <summary>
/// 技能
/// </summary>
[DataContract]
public sealed class SkillData
{
    [DataMember] public bool IsEnable { get; set; }

    [DataMember] public string Key { get; set; } = "12312321";

    [DataMember] public int Time { get; set; } = 123124532;
}