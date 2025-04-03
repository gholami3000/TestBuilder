namespace TestBuilder.Models
{
    public class HasIdValidator : Validator<RequestDto>
    {
        public override ValidationResult Validate(RequestDto input)
        {
            var result = new ValidationResult();
            if (input.Id is null)
            {
                result.AddError("id is empty");
                return result;
            }
            return this.nextValidator?.Validate(input) ?? result;
        }
    }


}
