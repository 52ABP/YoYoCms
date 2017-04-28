// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：ThreadExtensions.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System;
using System.Threading;

#endregion

namespace YoYo.Common.Extensions
{
    /// <summary>
    ///     线程扩展操作类
    /// </summary>
    public static class ThreadExtensions
    {
        /// <summary>
        ///     取消Thread.Sleep状态，继续线程
        /// </summary>
        public static void CancelSleep(this Thread thread)
        {
            if (thread.ThreadState != ThreadState.WaitSleepJoin)
                return;
            thread.Interrupt();
        }

        /// <summary>
        ///     启动线程，自动忽略停止线程时触发的<see cref="ThreadAbortException" />异常
        /// </summary>
        /// <param name="thread">线程</param>
        /// <param name="failAction">引发非<see cref="ThreadAbortException" />异常时执行的逻辑</param>
        public static void StartAndIgnoreAbort(this Thread thread, Action<Exception> failAction = null)
        {
            try
            {
                thread.Start();
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception e)
            {
                if (failAction != null)
                    failAction(e);
            }
        }
    }
}