using UnityEngine;

namespace EditorEx
{
    /// <summary>
    /// 添加了这个特性的字段显示在Editor中的Label的内容将会被替换成参数中本地化的字符串。
    /// </summary>
    public class LocalizedFieldAttribute : PropertyAttribute
    {
        string _name;
        public string name
        {
            get { return _name; }
        }
        public LocalizedFieldAttribute(string name)
        {
            _name = name;
        }
    }
}