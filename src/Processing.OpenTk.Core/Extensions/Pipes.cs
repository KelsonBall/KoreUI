using System;

namespace Processing.OpenTk.Core.Extensions
{
    public static class Pipes
    {
        public static TOut Then<TIn, TOut>(this TIn source, Func<TIn, TOut> transform) => transform(source);

        public static TOut Then<TIn, TParam1, TOut>(this TIn source, TParam1 param1, Func<TParam1, TIn, TOut> transform) => transform(param1, source);

        public static TOut Then<TIn, TParam1, TParam2, TOut>(this TIn source, TParam1 param1, TParam2 param2, Func<TParam1, TParam2, TIn, TOut> transform) => transform(param1, param2, source);

        public static void Then<TIn>(this TIn source, Action<TIn> sink) => sink(source);

        public static void Then<TIn, TParam>(this TIn source, TParam param, Action<TParam, TIn> sink) => sink(param, source);

        public static void Then<TIn, TParam1, TParam2>(this TIn source, TParam1 param1, TParam2 param2, Action<TParam1, TParam2, TIn> sink) => sink(param1, param2, source);
    }
}
