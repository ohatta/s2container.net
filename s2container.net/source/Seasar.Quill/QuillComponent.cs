#region Copyright
/*
 * Copyright 2005-2007 the Seasar Foundation and the Others.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */
#endregion

using System;
using System.Collections.Generic;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Proxy;
using System.Reflection;
using System.Collections;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Impl;

namespace Seasar.Quill
{
    /// <summary>
    /// QuillContainerに格納するコンポーネント定義クラス
    /// </summary>
    /// <remarks>
    /// コンストラクタで初期化を行う際にコンポーネントのオブジェクト
    /// をインスタンス化する
    /// </remarks>
    public class QuillComponent
    {
        /// <summary>
        /// コンポーネントのType
        /// </summary>
        protected Type componentType;

        /// <summary>
        /// コンポーネントを受け取るフィールドのType
        /// </summary>
        protected Type receiptType;

        /// <summary>
        /// コンポーネントのObjectを格納するコレクション
        /// </summary>
        /// <remarks>
        /// Aspectが適用される場合にはプロキシオブジェクトを格納する
        /// </remarks>
        protected Dictionary<Type, object> componentObjects = 
            new Dictionary<Type, object>(2);

        /// <summary>
        /// コンポーネントのTypeを取得する
        /// </summary>
        /// <value>コンポーネントのType</value>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// コンポーネントを受け取るフィールドのTypeを取得する
        /// </summary>
        /// <value>コンポーネントを受け取るフィールドのType</value>
        public Type ReceiptType
        {
            get { return receiptType; }
        }

        /// <summary>
        /// QuillComponentを初期化するコンストラクタ
        /// </summary>
        /// <param name="componentType">コンポーネントのType</param>
        /// <param name="receiptType">コンポーネントを受け取るフィールドのType</param>
        /// <param name="aspects">Aspect定義の配列</param>
        public QuillComponent(Type componentType, Type receiptType, IAspect[] aspects)
        {
            // コンポーネントのTypeをフィールドにセットする
            this.componentType = componentType;

            // コンポーネントを受け取るフィールドのTypeをフィールドにセットする
            this.receiptType = receiptType;

            if (aspects.Length > 0)
            {
                // Aspect属性が定義されている場合はProxyObjectを作成する
                CreateProxyObject(componentType, receiptType, aspects);
            }
            else
            {
                // Aspect属性が定義されていない場合は実装クラスのインスタンスを作成する
                CreateObject(componentType, receiptType);
            }
        }

        /// <summary>
        /// 実装クラスのインスタンスを作成する
        /// </summary>
        /// <remarks>
        /// インスタンスを作成するクラスにはパラメータ(引数)無しでpublicである
        /// コンストラクタが必要とする
        /// </remarks>
        /// <param name="componentType">コンポーネントのType</param>
        protected virtual void CreateObject(Type componentType, Type receiptType)
        {
            // コンストラクタを取得する
            ConstructorInfo constructor = componentType.GetConstructor(new Type[] { });

            if (constructor == null)
            {
                // パラメータ無しでpublicであるコンストラクタがない場合は例外をスローする
                throw new QuillApplicationException(
                    "EQLL0017", new object[] { componentType.FullName });
            }

            // インスタンスを作成する
            object obj = Activator.CreateInstance(componentType);
            
            // 実装クラスの型で格納する
            componentObjects[componentType] = obj;

            if (!componentType.Equals(receiptType))
            {
                // 受け側の型が異なる場合は受け側の型で格納する
                componentObjects[receiptType] = obj;
            }
        }

        /// <summary>
        /// プロキシオブジェクトを作成する
        /// </summary>
        /// <param name="componentType">コンポーネントのType</param>
        /// <param name="receiptType">コンポーネントを受け取るフィールドのType</param>
        /// <param name="aspects">適用するAspectの配列</param>
        /// <returns>作成されたプロキシオブジェクト</returns>
        protected virtual void CreateProxyObject(
            Type componentType, Type receiptType, IAspect[] aspects)
        {
            // S2Container.NETのコンポーネント定義を作成する
            // これはS2Dao.NETでIComponentDefからDaoのTypeを取得しているのに
            // 対応する為。(今後、もっと適切な方法で対応する必要あり)
            IComponentDef componentDef = new ComponentDefImpl(componentType);

            // DynamicAopProxyに渡す為のパラメータ
            Hashtable parameters = new Hashtable();

            // コンポーネント定義をパラメータとしてセットする
            parameters[ContainerConstants.COMPONENT_DEF_NAME] = componentDef;

            // DynamicAopProxyを作成する
            DynamicAopProxy aopProxy = 
                new DynamicAopProxy(componentType, aspects, parameters);

            // ProxyObjectを作成する
            componentObjects[componentType] = aopProxy.Create();

            if (!componentType.Equals(receiptType))
            {
                // コンポーネントの型とコンポーネントを受け取るフィールドの型が
                // 異なる場合は受け取る際の型に対応したProxyObjectを作成する
                componentObjects[receiptType] =
                    aopProxy.Create(receiptType, componentObjects[componentType]);
            }
        }

        /// <summary>
        /// コンポーネントのオブジェクトを取得する
        /// </summary>
        /// <param name="type">コンポーネントを受け取るフィールドのType</param>
        /// <returns>
        /// コンポーネントのオブジェクト
        /// <para>typeに対応するオブジェクトがない場合はnullを返す</para>
        /// </returns>
        public virtual object GetComponentObject(Type type)
        {
            if (componentObjects.ContainsKey(type))
            {
                // 対応するオブジェクトを格納している場合は返す
                return componentObjects[type];
            }
            else
            {
                // 対応するオブジェクトを格納していない場合はnullを返す
                return null;
            }
        }
    }
}
