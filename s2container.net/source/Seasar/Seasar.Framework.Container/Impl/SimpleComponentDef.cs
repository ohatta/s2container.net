#region Copyright
/*
 * Copyright 2005 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Container.Impl
{
	/// <summary>
	/// �R���|�[�l���g�̃C���X�^���X�𒼐ڕԂ��ꍇ�Ɏg�p����܂��B
	/// </summary>
	public class SimpleComponentDef : IComponentDef
	{
		private object component_;
		private Type componentType_;
		private string componentName_;
		private IS2Container container_;
		private IDictionary proxies_ = new Hashtable();

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public SimpleComponentDef()
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="componentType">�R���|�[�l���g��Type</param>
		public SimpleComponentDef(Type componentType) 
			: this(null,componentType,null)
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="component">�R���|�[�l���g</param>
		public SimpleComponentDef(object component) 
			: this(component,component.GetType())
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="component">�R���|�[�l���g</param>
		/// <param name="componentType">�R���|�[�l���g��Type</param>
		public SimpleComponentDef(object component,Type componentType)
			: this(component,componentType,null)
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="component">�R���|�[�l���g</param>
		/// <param name="componentName">�R���|�[�l���g�̖��O</param>
		public SimpleComponentDef(object component,string componentName)
			: this(component,component.GetType(),componentName)
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="component">�R���|�[�l���g</param>
		/// <param name="componentType">�R���|�[�l���g��Type</param>
		/// <param name="componentName">�R���|�[�l���g�̖��O</param>
		public SimpleComponentDef(object component, Type componentType,string componentName)
		{
			component_ = component;
			componentType_ = componentType;
			componentName_ = componentName;
		}

		#region ComponentDef �����o

		public virtual object GetComponent()
		{
			return component_;
		}

		public virtual object GetComponent(Type receiveType)
		{
			return component_;
		}

		public void InjectDependency(Object outerComponent)
		{
			
			throw new NotSupportedException("InjectDependency");
		}

		public IS2Container Container
		{
			get
			{
				
				return container_;
			}
			set
			{
				
				container_ = value;
			}
		}

		public Type ComponentType
		{
			get
			{
				
				return componentType_;
			}
		}

		public string ComponentName
		{
			get
			{
				
				return componentName_;
			}
		}

		public string AutoBindingMode
		{
			get
			{
				
				throw new NotSupportedException("AutoBindingMode");
			}
			set
			{
				
				throw new NotSupportedException("AutoBindingMode");
			}
		}

		public string InstanceMode
		{
			get
			{
				
				throw new NotSupportedException("InstanceMode");
			}
			set
			{
				
				throw new NotSupportedException("InstanceMode");
			}
		}

		public string Expression
		{
			get
			{
				
				throw new NotSupportedException("Expression");
			}
			set
			{
				
				throw new NotSupportedException("Expression");
			}
		}

		public void Init()
		{
			
		}

		public void Destroy()
		{
		}

		public object GetProxy(Type proxyType)
		{
			return proxies_[proxyType];
		}

		public void AddProxy(Type proxyType, object proxy)
		{
			proxies_[proxyType] = proxy;
		}

		#endregion

		#region IArgDefAware �����o

		public void AddArgDef(IArgDef argDef)
		{
			
			throw new NotSupportedException("AddArgDef");
		}

		public int ArgDefSize
		{
			get
			{
				
				throw new NotSupportedException("ArgDefSize");
			}
		}

		public IArgDef GetArgDef(int index)
		{
			
			throw new NotSupportedException("GetArgDef");
		}

		#endregion

		#region IPropertyDefAware �����o

		public void AddPropertyDef(IPropertyDef propertyDef)
		{
			
			throw new NotSupportedException("AddPropertyDef");
		}

		public int PropertyDefSize
		{
			get
			{
				
				throw new NotSupportedException("PropertyDefSize");
			}
		}

		public IPropertyDef GetPropertyDef(int index)
		{
			
			throw new NotSupportedException("GetPropertyDef");
		}

		public IPropertyDef GetPropertyDef(string propertyName)
		{
			
			throw new NotSupportedException("GetPropertyDef");
		}

		public bool HasPropertyDef(string propertyName)
		{
			
			throw new NotSupportedException("HasPropertyDef");
		}

		#endregion

		#region IInitMethodDefAware �����o

		public void AddInitMethodDef(IInitMethodDef methodDef)
		{
			
			throw new NotSupportedException("AddInitMethodDef");
		}

		public int InitMethodDefSize
		{
			get
			{
				
				throw new NotSupportedException("InitMethodDefSize");
			}
		}

		public IInitMethodDef GetInitMethodDef(int index)
		{
			
			throw new NotSupportedException("GetInitMethodDef");
		}

		#endregion

		#region IDestroyMethodDefAware �����o

		public void AddDestroyMethodDef(IDestroyMethodDef methodDef)
		{
			
			throw new NotSupportedException("AddDestroyMethodDef");
		}

		public int DestroyMethodDefSize
		{
			get
			{
				
				throw new NotSupportedException("DestroyMethodDefSize");
			}
		}

		public IDestroyMethodDef GetDestroyMethodDef(int index)
		{
			
			throw new NotSupportedException("GetDestroyMethodDef");
		}

		public IDestroyMethodDef[] GetDestroyMethodDefs()
		{
			throw new NotSupportedException("GetDestroyMethodDefs");
		}

		#endregion

		#region IAspectDefAware �����o

		public void AddAspeceDef(IAspectDef aspectDef)
		{
			
			throw new NotSupportedException("AddAspectDef");
		}

		public int AspectDefSize
		{
			get
			{
				
				throw new NotSupportedException("AspectDefSize");
			}
		}

		public IAspectDef GetAspectDef(int index)
		{
			
			throw new NotSupportedException("GetAspectDef");
		}

		#endregion

		#region IMetaDefAware �����o

		public void AddMetaDef(IMetaDef metaDef)
		{
			
			throw new NotSupportedException("AddMetaDef");
		}

		public int MetaDefSize
		{
			get
			{
				
				throw new NotSupportedException("MetaDefSize");
			}
		}

		public IMetaDef GetMetaDef(int index)
		{
			
			throw new NotSupportedException("GetMetaDef");
		}

		public IMetaDef GetMetaDef(string name)
		{
			
			throw new NotSupportedException("GetMethodDef");
		}

		public IMetaDef[] GetMetaDefs(string name)
		{
			
			throw new NotSupportedException("GetMetaDefs");
		}

		#endregion
	}
}
