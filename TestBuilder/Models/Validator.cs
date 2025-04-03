using System.ComponentModel.DataAnnotations;

namespace TestBuilder.Models
{
    public abstract class Validator<T>
    {
        protected Validator<T> nextValidator;
        public void SetNext(Validator<T> next)
        {
            this.nextValidator = next;
        }

        public virtual ValidationResult Validate(T input)
        {
            return nextValidator?.Validate(input) ?? new ValidationResult();
            //var result = new ValidationResult();

            //if (nextValidator != null)
            //{
            //    return nextValidator.Validate(input);
            //}
            //return result;
        }
    }

    //public class CondationValidator<T> : Validator<T>
    //{
    //    private readonly Func<T, bool> condation;
    //    private readonly Func<Validator<T>> wrappedValidator;

    //    public CondationValidator(Func<T, bool> condation, Func<Validator<T>> wrappedValidator)
    //    {
    //        this.condation = condation;
    //        this.wrappedValidator = wrappedValidator;
    //    }

    //    public virtual ValidationResult Validate(T input)
    //    {
    //        var result = new ValidationResult();

    //        if (condation(input))
    //        {
    //            return wrappedValidator.Invoke().Validate(input);
    //        }
    //        //if (nextValidator != null)
    //        //{
    //        //    return nextValidator.Validate(input);
    //        //}
    //        return result;
    //    }
    //}

    public class CondationValidator<T> : Validator<T>
    {
        private readonly Func<T, bool> condition;
        private readonly Func<Validator<T>> wrappedValidator;

        private Validator<T> instance; // ذخیره اعتبارسنجی ایجاد شده

        public CondationValidator(Func<T, bool> condition, Func<Validator<T>> wrappedValidator)
        {
            this.condition = condition;
            this.wrappedValidator = wrappedValidator;
        }

        public override ValidationResult Validate(T input)
        {
            if (condition(input))
            {
                if (instance == null) // فقط در صورت نیاز ساخته شود
                {
                    instance = wrappedValidator.Invoke();
                }
                return instance.Validate(input);
            }

            return nextValidator?.Validate(input) ?? new ValidationResult();
        }
    }

}
