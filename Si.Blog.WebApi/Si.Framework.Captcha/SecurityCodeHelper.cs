namespace Si.Framework.Captcha
{
    public class SecurityCodeHelper
    {
        private Lazy<CaptchaGenerater> captchaGenerater = new Lazy<CaptchaGenerater>(()=>new CaptchaGenerater());
        public void GenerateCode(int length, TimeSpan expireTime, Action<CaptchaRecord>? saveAction)
        {
            var captchaRecord = captchaGenerater.Value.GetCaptcha(expireTime, length);
            saveAction?.Invoke(captchaRecord);
        }
    }
}
