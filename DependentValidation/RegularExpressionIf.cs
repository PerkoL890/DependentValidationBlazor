using System.ComponentModel.DataAnnotations;


namespace DependentValidation
{
    public class RegularExpressionIfAttribute : RequiredIfAttribute
    {
        public string Pattern { get; set; }

        public RegularExpressionIfAttribute(string pattern, string dependentProperty, Operator @operator, object expectedValue)
            : base(dependentProperty, @operator, expectedValue)
        {
            Pattern = pattern;
        }

        public RegularExpressionIfAttribute(string pattern, string dependentProperty, object expectedValue)
            : this(pattern, dependentProperty, Operator.EqualTo, expectedValue) { }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object dependentValue = context.GetDependentValue(DependentProperty);
            if (Metadata.IsValid(ExpectedValue, dependentValue))
            {
                if (!OperatorMetadata.Get(Operator.RegExMatch).IsValid(Pattern, value))
                {
                    return new ValidationResult(FormatErrorMessage(context.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = DefaultErrorMessage;

            return string.Format(ErrorMessageString, name, DependentProperty, ExpectedValue, Pattern);
        }

        public override string DefaultErrorMessage
        {
            get { return "{0} must be in the format of {3} due to {1} being " + Metadata.ErrorMessage + " {2}"; }
        }
    }
}
