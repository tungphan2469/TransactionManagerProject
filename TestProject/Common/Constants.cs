namespace TestProject.Common
{
    public class Constants
    {
        public class AppSettingKeys
        {
            public const string DEFAULT_CONTROLLER_ROUTE = "api/[controller]";
            public const string DEFAULT_CONNECTION = "DefaultConnection";
            public const string AUTH_SECRET = "AuthSecret";
        }

        public class ExceptionMessage
        {
            public const string NOT_FOUND = "{0} not found.";
            public const string ITEM_NOT_FOUND = "Item not found.";
            public const string ALREADY_EXIST = "{0} already exist.";
            public const string SUCCESS = "{0} success.";
            public const string SHOULD_GREATER_TODAY = "{0} Date is late.";
            public const string INVALID = "{0} invalid.";
            public const string EMAIL_NOT_ACTIVATED = "Email not activated";
            public const string FILE_NOT_FOUND = "File {0} not found";
            public const string INSUFFICIENT_BALANCE = "Your balance is not sufficient for this transaction.";
            public const string WRONG_PASSWORD = "Wrong password.";

        }

        public class Message
        {
            public const string LOGIN_SUCCESS = "Login successful.";
        }

        public class ContextItem
        {
            public const string USER = "User";
            public const string PERMISSIONS = "Permissions";
        }

        public class DefaultValue
        {
            public const int DEFAULT_PAGE_SIZE_VIDEO = 9;
            public const int DEFAULT_PAGE_SIZE = 10;
            public const int DEFAULT_PAGE_NO = 1;
            public const string DEFAULT_CONTROLLER_ROUTE = "api/[controller]/[action]";
        }

    }
}
