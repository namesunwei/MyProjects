using System.ComponentModel.DataAnnotations;

namespace Chris.Framework.Infrastructure.Attributes.Validation
{
    /// <summary>
    /// 电话号码校验
    /// </summary>
    public class MobilePhoneNumValidationAttribute : RegularExpressionAttribute
    {
        private const string RegexPattern = @"^1[0-9]{10}$";
        public MobilePhoneNumValidationAttribute() : base(RegexPattern)
        {
            ErrorMessage = "手机号格式不正确";
        }
    }
}
