namespace System
{
    public static class SystemMixins
    {
        public static IEnumerator<int> GetEnumerator(this Range range) =>
            Enumerable.Range(range.Start.Value, range.End.Value).GetEnumerator();

        public static IEnumerable<T> Select<T>(this Range range, Func<int, T> action) =>
            Enumerable.Range(range.Start.Value, range.End.Value).Select(action);
    }
}

namespace FluentValidation
{
    public static class ValidationMixins
    {
        public static IRuleBuilderOptions<T, string> ValidName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Length(3, 50);
        }

        public static IRuleBuilderOptions<T, string> ValidDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MaximumLength(250);
        }
    }
}

namespace System
{
    public static class UnitMixins
    {
        public static IObservable<Unit> ToUnit<T>(this IObservable<T> observable) =>
            observable.Select(static _ => Unit.Default);
    }

    public static class ExceptionMixins
    {
        public static ErrorException AsErrorException<T>(this T exception) where T : Exception
        {
            return exception is ErrorException errorException
                ? errorException
                : new ErrorException(new Error(exception), exception);
        }
    }
}
