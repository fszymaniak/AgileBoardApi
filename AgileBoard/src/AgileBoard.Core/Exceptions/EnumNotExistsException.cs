namespace AgileBoard.Core.Exceptions
{
    public sealed class EnumNotExistsException : CustomException
    {
        public object ValidatedEnum { get; }

        public EnumNotExistsException(object validatedEnum) : base($"Enum '{validatedEnum}' does not exists.")
        {
            ValidatedEnum = validatedEnum;
        }
    }
}
