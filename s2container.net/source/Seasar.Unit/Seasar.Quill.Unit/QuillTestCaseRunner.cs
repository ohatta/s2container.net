﻿#region Copyright
/*
 * Copyright 2005-2008 the Seasar Foundation and the Others.
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
using System.Collections;
using System.IO;
using MbUnit.Core.Invokers;
using Seasar.Extension.ADO;
using Seasar.Extension.Tx;
using Seasar.Extension.Tx.Impl;
using Seasar.Extension.Unit;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Util;
using Seasar.Quill.Database.DataSource.Impl;
using Seasar.Quill.Exception;

namespace Seasar.Quill.Unit
{
	public class QuillTestCaseRunner : S2TestCaseRunner
	{
        private Tx _tx;
        private ITransactionContext _tc;
        private SelectableDataSourceProxyWithDictionary _dataSource;

        public new object Run(IRunInvoker invoker, object o, IList args, Tx tx)
        {
            _tx = tx;
            if (typeof(QuillTestCase).IsAssignableFrom(o.GetType()) == false)
            {
                throw new QuillInvalidClassException(o.GetType(), typeof(QuillTestCase));
            }
            _fixture = o as QuillTestCase;
            return Run(invoker, o, args);
        }

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
                _tc = (ITransactionContext)((QuillTestCase)_fixture).GetQuillComponent(
                    typeof(TransactionContext));
                TransactionContext tc = (TransactionContext)_tc;
                //  初回実行時はデータソースを設定する
                if (tc.DataSouce == null)
                {
                    tc.Current = tc;
                    tc.DataSouce = _dataSource;
                    _dataSource.SetTransactionContext(tc);
                }
                tc.Begin();
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

        protected virtual void SetUpQuillContainer(object target)
        {
            QuillTestCase fixture = _fixture as QuillTestCase;
            if (fixture != null)
            {
                fixture.Injector = QuillInjector.GetInstance();
                fixture.Inject(target);
                fixture.QContainer = fixture.Injector.Container;
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
	}
}