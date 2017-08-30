using System.ComponentModel.DataAnnotations;

namespace Chris.Framework.Infrastructure.Attributes.Validation
{
    /// <summary>
    /// 中文字符校验
    /// </summary>
    public class ChineseValidationAttribute : RegularExpressionAttribute
    {
        private const string RegixPattern = @"^[\u4e00-\u9fa5]*$";
        public ChineseValidationAttribute() : base(RegixPattern)
        {
            ErrorMessage = "请输入中文字符";
        }
    }
}
