using System;
using Seasar.Framework.Container.AutoRegister;

namespace Seasar.Windows
{
    /// <summary>
    /// �N��WindowsForm�w��p�N���X
    /// </summary>
    public class DefaultFormNaming : IAutoNaming
    {
        private string _mainFormName;
        private string _label = "MainForm";

        /// <summary>
        /// �N��WindowsForm
        /// </summary>
        public string MainFormName
        {
            get { return _mainFormName; }
            set { _mainFormName = value; }
        }

        /// <summary>
        /// WindowsForm�w�胉�x��
        /// </summary>
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        #region IAutoNaming Members

        /// <summary>
        /// �R���|�[�l���g�����`���܂��B
        /// </summary>
        /// <param name="type">�R���|�[�l���g�����`������Type</param>
        /// <returns>�R���|�[�l���g��</returns>
        public string DefineName(Type type)
        {
            string name = type.Name;
            if (name == _mainFormName)
            {
                if (!String.IsNullOrEmpty(_label))
                    return _label;
                else
                    return name;
            }
            else
            {
                return name;
            }
        }

        #endregion
    }
}