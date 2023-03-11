namespace AgileBoard.Core.Validators
{
    public static class EnumValidator
    {
        public static T Validate<T>(T enumToValidate)
        {
            if (Enum.IsDefined(typeof(T), enumToValidate) == true)
                return enumToValidate;
            else
                return default;
        }
    }
}
