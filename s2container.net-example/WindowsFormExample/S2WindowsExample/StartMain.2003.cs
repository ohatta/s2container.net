#region Copyright

/*
 * Copyright 2005-2006 the Seasar Foundation and the Others.
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
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using log4net.Util;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Seasar.Windows;

namespace Seasar.WindowsExample.Forms
{
    /// <summary>
    /// �A�v���P�[�V�����N���p�N���X
    /// </summary>
    /// <remarks>
    /// <newpara>
    /// ��{�I�ɂ͐ݒ�t�@�C�����Ɩ��O��Ԃ�ύX���āA�K�v�Ȓǉ������āA�g���܂킷�B
    /// </newpara>
    /// <newpara>
    /// �v���W�F�N�g�̐ݒ�̃X�^�[�g�A�b�v�I�u�W�F�N�g�����̃N���X�ɂ���̂�Y��Ȃ��B
    /// </newpara>
    /// </remarks>
    public class StartMain
    {
        /// <summary>
        /// DI�R���e�i�ݒ�t�@�C��(�ύX)
        /// </summary>
        private const string PATH = "Example.dicon";

        /// <summary>
        /// ���O(log4net)
        /// </summary>
        private static readonly ILog logger =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StartMain()
        {
            ;
        }

        /// <summary>
        /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                FileInfo info = new FileInfo(
                    string.Format("{0}.exe.config", SystemInfo.AssemblyShortName(
                                                        Assembly.GetExecutingAssembly())));
                // �A�Z���u����dll�̏ꍇ��".dll.config"

                XmlConfigurator.Configure(LogManager.GetRepository(), info);

                logger.Info("��d�N���`�F�b�N");
                Mutex mutex;
                OperatingSystem os = Environment.OSVersion;
                if ( os.Platform == PlatformID.Win32NT && os.Version.Major >= 5 )
                {
                    mutex = new Mutex(false, @"Global\" + Application.ProductName);
                }
                else
                {
                    mutex = new Mutex(false, Application.ProductName);
                }

                if ( mutex.WaitOne(0, false) )
                {
                    // �N���ς��Ȃ��ꍇ

                    logger.Info("�N��");

                    Application.EnableVisualStyles();

                    SingletonS2ContainerFactory.ConfigPath = PATH;
                    SingletonS2ContainerFactory.Init();
                    IS2Container container = SingletonS2ContainerFactory.Container;

                    ApplicationContext context
                        = (ApplicationContext) container.GetComponent(typeof (S2ApplicationContext));
                    Application.Run(context);

                    mutex.ReleaseMutex();
                }
                else
                {
                    logger.Info("��d�N���ς�");
                    MessageBox.Show("���̃A�v���P�[�V�����͂��łɋN�����Ă��܂�", "Main",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                GC.KeepAlive(mutex);
                mutex.Close();
            }
            catch ( ApplicationException ex )
            {
                MessageBox.Show(ex.Message, "Main",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch ( Exception e )
            {
                logger.Debug("�G���[:" + e.Message, e);
            }
            logger.Info("�I��");
        }
    }
}