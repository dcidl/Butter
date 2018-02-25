using System.IO;

using UnityEngine;
using UnityEngine.UI;

using DAFramework;

namespace Butter.StartMenu
{
    public class UGUIAVGBackground : AVGBackground
    {
        [SerializeField]
        Image _image;
        public override Sprite sprite
        {
            get
            {
                return _image.sprite;
            }
        }
        public override void setSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
        [SerializeField]
        string _spriteExternal;
        public override void setSpriteExternal(string streamingPath)
        {
            string url = Application.streamingAssetsPath + "/" + streamingPath;
            FileInfo fileInfo = new FileInfo(url);
            if (fileInfo.Exists)
            {
                using (FileStream fileStream = new FileStream(url, FileMode.Open))
                {
                    Texture2D texture = new Texture2D(100, 100);
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        texture.LoadImage(reader.ReadBytes((int)fileStream.Length));
                    }
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
                    sprite.name = fileInfo.Name;
                    _image.sprite = sprite;
                    _spriteExternal = streamingPath;
                }
            }
            else
            {
                throw new FileNotFoundException("文件" + url + "不存在！");
            }
        }

        public override AVGBackgroundSave save()
        {
            return new AVGBackgroundSave()
            {
                spriteExternal = _spriteExternal
            };
        }

        public override void load(AVGBackgroundSave save)
        {
            if (save != null)
            {
                if (!string.IsNullOrEmpty(save.spriteExternal))
                {
                    setSpriteExternal(save.spriteExternal);
                }
            }
        }
    }
}
