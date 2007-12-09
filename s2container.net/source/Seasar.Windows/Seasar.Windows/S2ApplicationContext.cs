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

using System.Windows.Forms;
using Seasar.Framework.Container;

namespace Seasar.Windows
{
    /// <summary>
    /// DI�R���e�i�pApplicationContext�h���N���X
    /// </summary>
    public class S2ApplicationContext : ApplicationContext
    {
        /// <summary>
        /// DI�R���e�i
        /// </summary>
        private IS2Container _container;

        /// <summary>
        /// DI�R���e�i
        /// </summary>
        public IS2Container DIContainer
        {
            get { return _container; }
            set { _container = value; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="container">DI�R���e�i</param>
        public S2ApplicationContext(IS2Container container)
        {
            _container = container;
        }

        /// <summary>
        /// �X���b�h�I������
        /// </summary>
        protected override void ExitThreadCore()
        {
            // �N���[���A�b�v����
            _container.Destroy();

            // ���C���X���b�h�̏I������
            base.ExitThreadCore();
        }
    }
}