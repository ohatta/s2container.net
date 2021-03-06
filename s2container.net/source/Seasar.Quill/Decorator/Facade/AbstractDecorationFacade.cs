﻿using System;

namespace Seasar.Quill.Decorator.Facade
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractDecorationFacade
    {
        #region Func
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RETURN_TYPE"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public virtual RETURN_TYPE Decorate<RETURN_TYPE>(Func<RETURN_TYPE> f)
        {
            return DoDecorate(() => f());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RETURN_TYPE"></typeparam>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public virtual RETURN_TYPE Decorate<RETURN_TYPE, ARG1_TYPE>(Func< ARG1_TYPE, RETURN_TYPE> f, ARG1_TYPE arg1)
        {
            return DoDecorate(() => f(arg1), arg1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RETURN_TYPE"></typeparam>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <typeparam name="ARG2_TYPE"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public virtual RETURN_TYPE Decorate<RETURN_TYPE, ARG1_TYPE, ARG2_TYPE>(
            Func<ARG1_TYPE, ARG2_TYPE, RETURN_TYPE> f, 
            ARG1_TYPE arg1, ARG2_TYPE arg2)
        {
            return DoDecorate(() => f(arg1, arg2), arg1, arg2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RETURN_TYPE"></typeparam>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <typeparam name="ARG2_TYPE"></typeparam>
        /// <typeparam name="ARG3_TYPE"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <returns></returns>
        public virtual RETURN_TYPE Decorate<RETURN_TYPE, ARG1_TYPE, ARG2_TYPE, ARG3_TYPE>(
           Func<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, RETURN_TYPE> f,
           ARG1_TYPE arg1, ARG2_TYPE arg2, ARG3_TYPE arg3)
        {
            return DoDecorate(() => f(arg1, arg2, arg3), arg1, arg2, arg3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RETURN_TYPE"></typeparam>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <typeparam name="ARG2_TYPE"></typeparam>
        /// <typeparam name="ARG3_TYPE"></typeparam>
        /// <typeparam name="ARG4_TYPE"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <returns></returns>
        public virtual RETURN_TYPE Decorate<RETURN_TYPE, ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, ARG4_TYPE>(
           Func<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, ARG4_TYPE, RETURN_TYPE> f,
           ARG1_TYPE arg1, ARG2_TYPE arg2, ARG3_TYPE arg3, ARG4_TYPE arg4)
        {
            return DoDecorate(() => f(arg1, arg2, arg3, arg4), arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RETURN_TYPE"></typeparam>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <typeparam name="ARG2_TYPE"></typeparam>
        /// <typeparam name="ARG3_TYPE"></typeparam>
        /// <typeparam name="ARG4_TYPE"></typeparam>
        /// <typeparam name="ARG5_TYPE"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <param name="arg5"></param>
        /// <returns></returns>
        public virtual RETURN_TYPE Decorate<RETURN_TYPE, ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, ARG4_TYPE, ARG5_TYPE>(
           Func<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, ARG4_TYPE, ARG5_TYPE, RETURN_TYPE> f,
           ARG1_TYPE arg1, ARG2_TYPE arg2, ARG3_TYPE arg3, ARG4_TYPE arg4, ARG5_TYPE arg5)
        {
            return DoDecorate(() => f(arg1, arg2, arg3, arg4, arg5), arg1, arg2, arg3, arg4, arg5);
        }
        #endregion

        #region Action

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        public virtual void Decorate(Action a)
        {
            DoDecorate(() => { a(); return default(object); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <param name="a"></param>
        /// <param name="arg1"></param>
        public virtual void Decorate<ARG1_TYPE>(Action<ARG1_TYPE> a, ARG1_TYPE arg1)
        {
            DoDecorate(() => { a(arg1); return default(object); }, arg1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <typeparam name="ARG2_TYPE"></typeparam>
        /// <param name="a"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public virtual void Decorate<ARG1_TYPE, ARG2_TYPE>(Action<ARG1_TYPE, ARG2_TYPE> a, ARG1_TYPE arg1, ARG2_TYPE arg2)
        {
            DoDecorate(() => { a(arg1, arg2); return default(object); }, arg1, arg2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <typeparam name="ARG2_TYPE"></typeparam>
        /// <typeparam name="ARG3_TYPE"></typeparam>
        /// <param name="a"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        public virtual void Decorate<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE>(Action<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE> a, 
            ARG1_TYPE arg1, ARG2_TYPE arg2, ARG3_TYPE arg3)
        {
            DoDecorate(() => { a(arg1, arg2, arg3); return default(object); },
                arg1, arg2, arg3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <typeparam name="ARG2_TYPE"></typeparam>
        /// <typeparam name="ARG3_TYPE"></typeparam>
        /// <typeparam name="ARG4_TYPE"></typeparam>
        /// <param name="a"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        public virtual void Decorate<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, ARG4_TYPE>(
            Action<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, ARG4_TYPE> a,
            ARG1_TYPE arg1, ARG2_TYPE arg2, ARG3_TYPE arg3, ARG4_TYPE arg4)
        {
            DoDecorate(() => { a(arg1, arg2, arg3, arg4); return default(object); },
                arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ARG1_TYPE"></typeparam>
        /// <typeparam name="ARG2_TYPE"></typeparam>
        /// <typeparam name="ARG3_TYPE"></typeparam>
        /// <typeparam name="ARG4_TYPE"></typeparam>
        /// <typeparam name="ARG5_TYPE"></typeparam>
        /// <param name="a"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <param name="arg5"></param>
        public virtual void Decorate<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, ARG4_TYPE, ARG5_TYPE>(
            Action<ARG1_TYPE, ARG2_TYPE, ARG3_TYPE, ARG4_TYPE, ARG5_TYPE> a,
            ARG1_TYPE arg1, ARG2_TYPE arg2, ARG3_TYPE arg3, ARG4_TYPE arg4, ARG5_TYPE arg5)
        {
            DoDecorate(() => { a(arg1, arg2, arg3, arg4, arg5); return default(object); },
                arg1, arg2, arg3, arg4, arg5);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="RETURN_TYPE"></typeparam>
        /// <param name="f"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual RETURN_TYPE DoDecorate<RETURN_TYPE>(Func<RETURN_TYPE> f, params object[] parameters)
        {
            return GetScopeDecorator().Execute(f, parameters);
        }

        /// <summary>
        /// 挟み込むDecoratorの取得
        /// </summary>
        /// <returns></returns>
        protected abstract IScopeDecorator GetScopeDecorator();
    }
}
