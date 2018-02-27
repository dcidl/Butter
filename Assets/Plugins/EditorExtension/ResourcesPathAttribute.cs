using UnityEngine;

namespace EditorEx
{
    /// <summary>
    /// 适用于字符串字段，会将这个字段赋值为目标字段所引用的UnityObject的Resources地址。
    /// </summary>
    public class ResourcesPathAttribute : PropertyAttribute
    {
        private string _field;
        public string fieldName
        {
            get { return _field; }
        }
        /// <summary>
        /// 目标字段必须是Unity.Object。
        /// </summary>
        /// <param name="relativeFieldName">目标字段的名字</param>
        public ResourcesPathAttribute(string relativeFieldName)
        {
            _field = relativeFieldName;
        }
    }
}