namespace TestBuilder.Models
{
    public class CheckTitleValidator : Validator<RequestDto>
    {
        public override ValidationResult Validate(RequestDto input)
        {

            var result = new ValidationResult();
            if (string.IsNullOrEmpty(input.Title))
            {
                result.AddError("title IsNotNullOrEmpty");
                return result;
            }
            return this.nextValidator?.Validate(input) ?? result;
        }
    }


}
