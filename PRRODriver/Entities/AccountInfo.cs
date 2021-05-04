namespace PRRODriver.Entities
{
    public class AccountInfo
    {
        public readonly string SertificatePath;
        public readonly string Password;
        //TODO:  expand with required information if it is needed;  path to the key e.g.

        public AccountInfo(string aSertificate, string aPassword)
        {
            SertificatePath = aSertificate;
            Password = aPassword;
        }
    }
}
