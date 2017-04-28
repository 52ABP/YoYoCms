// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：TryCatchExtensions.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System;

#endregion

namespace YoYo.Common.Extensions
{
    /// <summary>
    ///     Try-Catch扩展操作
    /// </summary>
    public static class TryCatchExtensions
    {
        /// <summary>
        ///     对某对象执行指定功能与后续功能，并处理异常情况
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">值</param>
        /// <param name="action">要对值执行的主功能代码</param>
        /// <param name="failureAction">catch中的功能代码</param>
        /// <param name="successAction">主功能代码成功后执行的功能代码</param>
        /// <returns>主功能代码是否顺利执行</returns>
        public static bool TryCatch<T>(this T source, Action<T> action, Action<Exception> failureAction,
            Action<T> successAction) where T : class
        {
            bool result;
            try
            {
                action(source);
                successAction(source);
                result = true;
            }
            catch (Exception obj)
            {
                failureAction(obj);
                result = false;
            }
            return result;
        }

        /// <summary>
        ///     对某对象执行指定功能，并处理异常情况
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">值</param>
        /// <param name="action">要对值执行的主功能代码</param>
        /// <param name="failureAction">catch中的功能代码</param>
        /// <returns>主功能代码是否顺利执行</returns>
        public static bool TryCatch<T>(this T source, Action<T> action, Action<Exception> failureAction) where T : class
        {
            return source.TryCatch(action,
                failureAction,
                obj => { });
        }

        /// <summary>
        ///     对某对象执行指定功能，并处理异常情况与返回值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">值</param>
        /// <param name="func">要对值执行的主功能代码</param>
        /// <param name="failureAction">catch中的功能代码</param>
        /// <param name="successAction">主功能代码成功后执行的功能代码</param>
        /// <returns>功能代码的返回值，如果出现异常，则返回对象类型的默认值</returns>
        public static TResult TryCatch<T, TResult>(this T source, Func<T, TResult> func, Action<Exception> failureAction,
            Action<T> successAction)
            where T : class
        {
            TResult result;
            try
            {
                var u = func(source);
                successAction(source);
                result = u;
            }
            catch (Exception obj)
            {
                failureAction(obj);
                result = default(TResult);
            }
            return result;
        }

        /// <summary>
        ///     对某对象执行指定功能，并处理异常情况与返回值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">值</param>
        /// <param name="func">要对值执行的主功能代码</param>
        /// <param name="failureAction">catch中的功能代码</param>
        /// <returns>功能代码的返回值，如果出现异常，则返回对象类型的默认值</returns>
        public static TResult TryCatch<T, TResult>(this T source, Func<T, TResult> func, Action<Exception> failureAction)
            where T : class
        {
            return source.TryCatch(func,
                failureAction,
                obj => { });
        }
    }
}