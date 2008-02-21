using System;
using System.Collections.Generic;
using System.Text;
using Seasar.Framework.Aop;
using Seasar.Extension.Tx;

namespace Seasar.Quill.Tx
{
    /// <summary>
    /// Quill�pTransactionInterceptor
    /// ����Ă��邱�Ƃ�S2Container��TransactionInterceptor��
    /// �S������
    /// </summary>
    public class QuillTransactionInterceptor : IMethodInterceptor
    {
        /// <summary>
        /// �{����TransactionInterceptor�̃C���X�^���X��ێ�����
        /// </summary>
        private TransactionInterceptor s2daoTxInterceptor = null;

        private ITransactionHandler transactionhandler = null;
        private ITransactionStateHandler tansactionstatehandler = null;

        /// <summary>
        /// S2Dao��TransactionInterceptor�𗘗p����
        /// �iTransactionInterceptor��TransactionHandler��readonly��
        /// �������Ȃ���΁ATransactionInterceptor���g��Ȃ��������j
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public object Invoke(IMethodInvocation invocation)
        {
            //  S2Dao��TransactionInterceptor���Ȃ�������C���X�^���X����
            //  �{�N���X��QuillComponent�Ƃ��Ĉ����ꍇ��singleton
            //  �����ŕێ�����TransactionInterceptor�������I��singleton�ƂȂ�
            if ( s2daoTxInterceptor == null )
            {
                s2daoTxInterceptor = new TransactionInterceptor(TransactionHandler);
                s2daoTxInterceptor.TransactionStateHandler = TransactionStateHandler;
            }
            return s2daoTxInterceptor.Invoke(invocation);
        }

        public ITransactionHandler TransactionHandler
        {
            get { return transactionhandler; }
            set { transactionhandler = value; }
        }

        public ITransactionStateHandler TransactionStateHandler
        {
            get { return tansactionstatehandler; }
            set { tansactionstatehandler = value; }
        }
    }
}
