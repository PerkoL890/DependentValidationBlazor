using System.ComponentModel.DataAnnotations;

namespace DependentValidation
{
    public class IsAttribute : DependentPropertyValidationAttribute
    {
        public Operator Operator { get; private set; }
        public bool PassOnNull { get; set; }

        protected OperatorMetadata Metadata { get; private set; }

        public IsAttribute(Operator @operator, string dependentProperty)
            : base(dependentProperty)
        {
            Operator = @operator;
            PassOnNull = false;
            Metadata = OperatorMetadata.Get(Operator);
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object dependentValue = context.GetDependentValue(DependentProperty);
            if (PassOnNull && (value == null || dependentValue == null) && (value != null || dependentValue != null))
            {
                return ValidationResult.Success;
            }
            
            if (!Metadata.IsValid(value, dependentValue))
            {
                return new ValidationResult(FormatErrorMessage(context.DisplayName));
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = DefaultErrorMessage;

            return string.Format(ErrorMessageString, name, DependentProperty);
        }

        public override string DefaultErrorMessage
        {
            get { return "{0} must be " + Metadata.ErrorMessage + " {1}."; }
        }
    }
}
