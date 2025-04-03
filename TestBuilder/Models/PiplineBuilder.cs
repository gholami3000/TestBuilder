namespace TestBuilder.Models
{
    public class PiplineBuilder<T>
    {
        private Validator<T> head;
        private Validator<T> tail;
        public PiplineBuilder<T> Add(Validator<T> validator)
        {
            if (head == null)
            {
                head = validator;
                tail = validator;
            }
            else
            {
                tail.SetNext(validator);
                tail = validator;
            }
            return this;
        }

        //public PiplineBuilder<T> When(Func<T, bool> condation, Func<Validator<T>> validatorFactory)
        //{
        //    var qq = new CondationValidator<T>(condation, validatorFactory);

        //    tail.SetNext(qq);
        //    tail = qq;
        //    return this;
        //}

        //public PiplineBuilder<T> When(Func<T, bool> condition, Func<Validator<T>> validatorFactory)
        //{
        //    var validator = new CondationValidator<T>(condition, validatorFactory);

        //    if (head == null)
        //    {
        //        head = validator;
        //        tail = validator;
        //    }
        //    else
        //    {
        //        tail.SetNext(validator);
        //        tail = validator;
        //    }
        //    return this;
        //}

        public PiplineBuilder<T> When(Func<T, bool> condition, params Func<Validator<T>>[] validators)
        {
            Validator<T> headValidator = null;
            Validator<T> tailValidator = null;

            foreach (var validatorFactory in validators)
            {
                var validator = validatorFactory.Invoke();
                if (headValidator == null)
                {
                    headValidator = validator;
                    tailValidator = validator;
                }
                else
                {
                    tailValidator.SetNext(validator);
                    tailValidator = validator;
                }
            }

            var conditionalValidator = new CondationValidator<T>(condition, () => headValidator);

            if (head == null)
            {
                head = conditionalValidator;
                tail = conditionalValidator;
            }
            else
            {
                tail.SetNext(conditionalValidator);
                tail = conditionalValidator;
            }

            return this;
        }

        public Validator<T> Build()
        {
            return head;
        }
    }


}
