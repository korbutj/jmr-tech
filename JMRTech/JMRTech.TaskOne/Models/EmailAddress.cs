namespace JMRTech.TaskOne
{
    public class EmailAddress
    {
        public string Email { get; set; }
        public string EmailType { get; set; }

        public static string MainEmail => "main";
        public static string AlternativeEmail => "alternative";
    }
}
