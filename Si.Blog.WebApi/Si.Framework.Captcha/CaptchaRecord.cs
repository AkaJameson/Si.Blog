namespace Si.Framework.Captcha
{
    public struct CaptchaRecord
    {
        public string CaptchaCode { get; set; }
        public byte[] CaptchaImage { get; set; }
        public DateTime ExpiraTime { get; set; }
    }
}
