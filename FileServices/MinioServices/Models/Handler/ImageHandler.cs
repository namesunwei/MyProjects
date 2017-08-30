using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;

namespace MinioServices.Models.Handler
{
    public static class ImageHandler
    {
        /// <summary>
        /// 判断图片是否有效，以及是否损坏
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Tuple<bool, string> Invalid(MemoryStream stream)
        {
            try
            {
                using (new MagickImage(stream))
                {

                }
            }
            catch (MagickException)
            {
                return new Tuple<bool, string>(false, "上传失败，图片无效。");
            }
            try
            {
                using (new MagickImage(stream))
                {

                }
            }
            catch (MagickCorruptImageErrorException)
            {

                return new Tuple<bool, string>(false, "上传失败，图片已损坏。");
            }

            return new Tuple<bool, string>(true, null);
        }
        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="width">宽度</param>
        /// <param name="heigh">高度</param>
        /// <param name="ignoreAspectRatio">是否忽略宽高比</param>
        /// <returns></returns>
        public static MemoryStream ReSizeImage(MemoryStream stream, int width = 0, int heigh = 0, bool ignoreAspectRatio = false)
        {
            using (var image = new MagickImage(stream))
            {
                var result = new MemoryStream();
                var size = new MagickGeometry(width, heigh) { IgnoreAspectRatio = ignoreAspectRatio };
                image.Resize(size);
                image.Write(result);
                result.Seek(0, SeekOrigin.Begin);
                return result;
            }
        }

    }
}
