namespace TestBuilder.Models
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            IsSuccess = true;
            ErrorList = new List<string>();
            SuccessMessageList = new List<string>();
        }
        public bool IsSuccess
        {
            get
            {
                if (ErrorList.Any())
                {
                    return false;
                }
                return true;
            }
            private set { }
        }
        public List<string> ErrorList { get; private set; }

        public List<string> SuccessMessageList { get; private set; }

        public void AddError(string error)
        {
            ErrorList.Add(error);
        }

        public void AddSuccessMessage(string message)
        {
            SuccessMessageList.Add(message);
        }
    }
}
