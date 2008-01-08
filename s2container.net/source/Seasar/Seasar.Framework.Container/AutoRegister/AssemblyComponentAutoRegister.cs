#region Copyright
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
using System.Reflection;

namespace Seasar.Framework.Container.AutoRegister
{
    /// <summary>
    /// �A�Z���u������R���|�[�l���g���������ăR���|�[�l���g�������o�^���܂��B
    /// </summary>
	public class AssemblyComponentAutoRegister : AbstractComponentAutoRegister
	{
        private string assemblyName = null;

        /// <summary>
        /// �R���|�[�l���g�����Ώۂ̃A�Z���u���ȈՖ����擾�E�ݒ肵�܂��B
        /// </summary>
        public string AssemblyName
        {
            set { assemblyName = value; }
            get { return assemblyName; }
        }

        /// <summary>
        /// �R���|�[�l���g�������o�^���܂��B
        /// </summary>
        public override void RegisterAll()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                if (assemblyName == null || assembly.GetName().Name.Equals(assemblyName))
                {
                    ProcessAssembly(assembly);

                    if (assemblyName != null)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// �A�Z���u�����������ăR���|�[�l���g�������o�^���܂��B
        /// </summary>
        /// <param name="assembly"></param>
        public void ProcessAssembly(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                ProcessType(type);
            }
        }
    }
}
