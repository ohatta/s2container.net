using System;
using System.Windows.Forms;

namespace Seasar.S2FormExample.Forms
{
    /// <summary>
    /// �N�����X�v���b�V���E�B���h�E�t�H�[��
    /// </summary>
    public partial class FrmSplash : Form
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public FrmSplash()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �t�H�[�������Ƃ��̏���
        /// </summary>
        /// <remarks>���Ȃ��悤�ɏ������Ă���</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSplash_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
#if DEBUG
            Console.Out.WriteLine("SplashClosing Cancel");
#endif
        }
    }
}