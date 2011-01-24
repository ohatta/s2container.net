﻿#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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
using Seasar.Extension.Tx;
using Seasar.Extension.Tx.Impl;
using Seasar.Extension.Unit;
using Seasar.Quill.Dao;
using Seasar.Quill.Database.DataSource.Impl;
using Seasar.Quill.Database.Tx;
using Seasar.Quill.Util;
#if NET_4_0
using Seasar.Unit.Core;
#else
#region NET2.0
using System.Collections;
using System.IO;
using MbUnit.Core.Invokers;
using Seasar.Extension.ADO;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Util;
using Seasar.Quill.Exception;
#endregion
#endif

namespace Seasar.Quill.Unit
{
    /// <summary>
    /// Quill用テスト実行クラス
    /// </summary>
#if NET_4_0
    public class QuillTestCaseRunner : S2TestCaseRunnerBase
#else
#region NET2.0
    public class QuillTestCaseRunner : S2TestCaseRunner
#endregion
#endif
    {
        private Type _daoSettingType;
        private Type _transactionSettingType;

#if NET_4_0
        public QuillTestCaseRunner(Tx txTreatment)
            : base(txTreatment)
        {
        }

        protected override void SetUpContainer(object fixtureInstance)
        {
            QuillConfig.ConfigPath = null;
            GetInjector().Inject(fixtureInstance);

            var config = QuillConfig.GetInstance();

            //  Quillの設定ファイルがあればS2Dao、Transaction設定を使用する
            if (config.HasQuillConfig())
            {
                _daoSettingType = config.GetDaoSettingType();
                _transactionSettingType = config.GetTransationSettingType();
            }
            else
            {
                _daoSettingType = SettingUtil.GetDefaultDaoSettingType();
                _transactionSettingType = SettingUtil.GetDefaultTransactionType();
            }
        }

        protected override void SetUpAfterContainerInit(object fixtureInstance)
        {
            SetUpDataSource(fixtureInstance);
        }

        protected override void TearDownBeforeContainerDestroy(object fixtureInstance)
        {
            TearDownDataSource(fixtureInstance);
        }

        protected override void TearDownContainer(object fixtureInstance)
        {
            GetInjector().Destroy();
        }

        protected override ITransactionContext GetTransactionContext()
        {
            var container = GetInjector().Container;
            var txSetting = (ITransactionSetting)ComponentUtil.GetComponent(
                container, _transactionSettingType);

            //  SetupInjectionが呼ばれていればTransactionContextは設定済み
            return txSetting.TransactionContext;
        }

        protected override Extension.ADO.IDataSource GetDataSource(object fixtureInstance)
        {
            var container = GetInjector().Container;
            var dataSource = (SelectableDataSourceProxyWithDictionary)ComponentUtil.GetComponent(
                container, typeof(SelectableDataSourceProxyWithDictionary));
            dataSource.SetDataSourceName(null);

            //  DaoとTransaction設定は予めやっておく
            //  （テストするクラスと同じDataSource,TransactionContextを使用するため)
            var daoSetting = (IDaoSetting)ComponentUtil.GetComponent(
                container, _daoSettingType);
            if (daoSetting.IsNeedSetup())
            {
                daoSetting.Setup(dataSource);
            }

            if (_txTreatment != Tx.NotSupported)
            {
                var txSetting = (ITransactionSetting)ComponentUtil.GetComponent(
                    container, _transactionSettingType);
                if (txSetting.IsNeedSetup())
                {
                    txSetting.Setup(dataSource);
                }
            }
            return dataSource;
        }

        protected override void TearDownDataSource(object fixtureInstance)
        {
            if (_dataSource != null && _transactionContext != null)
            {
                _dataSource.CloseConnection(_transactionContext.Connection);
            }

            var ds = (SelectableDataSourceProxyWithDictionary)_dataSource;
            if (ds != null)
            {
                ds.SetDataSourceName(null);
            }
            base.TearDownDataSource(fixtureInstance);
        }

        /// <summary>
        /// singletonなQuillInjectorインスタンス取得のショートカットメソッド
        /// </summary>
        /// <returns></returns>
        protected virtual QuillInjector GetInjector()
        {
            return QuillInjector.GetInstance();
        }
#else
#region NET2.0
        private ITransactionContext _tc;
        private SelectableDataSourceProxyWithDictionary _dataSource;

        public override object Run(IRunInvoker invoker, object o, IList args)
        {
            _method = _fixture.GetType().GetMethod(invoker.Name);
            SetUpQuillContainer(o);
            SetUpContainer();
            try
            {
                try
                {
                    SetUpMethod();
                    SetUpForEachTestMethod();
                    try
                    {
                        SetUpAfterContainerInit();
                        SetupInjection(o);
                        try
                        {
                            try
                            {
                                BeginTransactionContext();
                                return invoker.Execute(o, args);
                            }
                            catch (System.Exception e)
                            {
                                ExceptionHandler(e);
                                throw;
                            }
                            finally
                            {
                                EndTransactionContext();
                            }
                        }
                        catch (System.Exception e)
                        {
                            ExceptionHandler(e);
                            throw;
                        }
                        finally
                        {
                            TearDownBeforeContainerDestroy();
                        }
                    }
                    catch (System.Exception e)
                    {
                        ExceptionHandler(e);
                        throw;
                    }
                }
                catch (System.Exception e)
                {
                    ExceptionHandler(e);
                    throw;
                }
                finally
                {
                    TearDownForEachTestMethod();
                    TearDownMethod();
                }
            }
            catch (System.Exception e)
            {
                ExceptionHandler(e);
                throw;
            }
            finally
            {
                for (int i = 0; i < 3; ++i)
                {
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                TearDownContainer();
                TearDownQuillContainer();
            }
        }

        protected override void BeginTransactionContext()
        {
            if (Tx.NotSupported != _tx)
            {
                QuillTestCase quillFixture = (QuillTestCase)_fixture;
                ITransactionSetting txSetting = (ITransactionSetting)quillFixture.GetQuillComponent(
                    _transactionSettingType);

                //  SetupInjectionが呼ばれていればTransactionContextは設定済み
                _tc = txSetting.TransactionContext;

                _tc = _tc.Create();
                _tc.Current = _tc;
                _tc.Begin();
            }
        }

        protected override void EndTransactionContext()
        {
            if (_tc != null)
            {
                if (Tx.Commit == _tx)
                {
                    _tc.Commit();
                }
                if (Tx.Rollback == _tx)
                {
                    _tc.Rollback();
                }

                _tc.Dispose();
            }
        }

        protected override void SetupDataSource()
        {
            QuillTestCase fixture = (QuillTestCase)_fixture;
            _dataSource = (SelectableDataSourceProxyWithDictionary)fixture.GetQuillComponent(
                typeof(SelectableDataSourceProxyWithDictionary));
            fixture.SetDataSource(_dataSource);
        }

        protected override void TearDownDataSource()
        {
            foreach (string dataSourceName in _dataSource.DataSourceCollection.Keys)
            {
                IDataSource dataSource = _dataSource.GetDataSource(dataSourceName);
                TxDataSource txDataSource = dataSource as TxDataSource;
                if (txDataSource != null)
                {
                    if (txDataSource.Context != null && txDataSource.Context.Connection != null)
                    {
                        txDataSource.CloseConnection(txDataSource.Context.Connection);
                    }
                }
            }
            QuillTestCase fixture = _fixture as QuillTestCase;
            if (fixture != null)
            {
                if (fixture.HasConnection)
                {
                    ConnectionUtil.Close(fixture.Connection);
                    fixture.SetConnection(null);
                }
                fixture.SetDataSource(null);
            }
            _dataSource = null;
        }

        protected virtual void SetupInjection(object target)
        {
            if (_dataSource == null)
            {
                throw new ArgumentNullException("dataSource");
            }

            QuillTestCase fixture = (QuillTestCase)target;

            //  DaoとTransaction設定は予めやっておく
            //  （テストするクラスと同じDataSource,TransactionContextを使用するため)
            IDaoSetting daoSetting = (IDaoSetting)fixture.GetQuillComponent(
                _daoSettingType);
            if (daoSetting.IsNeedSetup())
            {
                daoSetting.Setup(_dataSource);
            }

            if (_tx != Tx.NotSupported)
            {
                ITransactionSetting txSetting = (ITransactionSetting)fixture.GetQuillComponent(
                    _transactionSettingType);
                if (txSetting.IsNeedSetup())
                {
                    txSetting.Setup(_dataSource);
                }
            }
            //  必要なコンポーネントを作成した上でインジェクション実行
            fixture.Inject(target);
        }

        protected virtual void SetUpQuillContainer(object target)
        {
            QuillTestCase fixture = _fixture as QuillTestCase;
            if (fixture != null)
            {
                fixture.Injector = QuillInjector.GetInstance();
                fixture.QContainer = fixture.Injector.Container;
                //  MbUnitはstaticな変数が保持されてしまうのでここでリセット
                QuillConfig.ConfigPath = null;
                QuillConfig config = QuillConfig.GetInstance();

                //  Quillの設定ファイルがあればS2Dao、Transaction設定を使用する
                if (config.HasQuillConfig())
                {
                    _daoSettingType = config.GetDaoSettingType();
                    _transactionSettingType = config.GetTransationSettingType();
                }
                else
                {
                    _daoSettingType = SettingUtil.GetDefaultDaoSettingType();
                    _transactionSettingType = SettingUtil.GetDefaultTransactionType();
                }
            }
        }

        protected virtual void TearDownQuillContainer()
        {
            ((QuillTestCase)_fixture).Injector.Destroy();
        }

        protected override void SetUpContainer()
        {
            //  diconが存在する場合はS2Containerが利用されていると見なす
            if (File.Exists(SingletonS2ContainerFactory.ConfigPath))
            {
                base.SetUpContainer();
            }
        }

        protected override void TearDownContainer()
        {
            //  diconが存在する場合はS2Containerが利用されていると見なす
            if (File.Exists(SingletonS2ContainerFactory.ConfigPath))
            {
                base.TearDownContainer();
            }
        }
#endregion
#endif
    }
}