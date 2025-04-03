namespace TestBuilder.Models
{
    public class CheckIdNotZeroValidator : Validator<RequestDto>
    {
        public override ValidationResult Validate(RequestDto input)
        {
            var result = new ValidationResult();
            if (input.Id == 0)
            {
                result.AddError("id is not zero");
                return result;
            }
            return this.nextValidator?.Validate(input) ?? result;
        }
    }


}
