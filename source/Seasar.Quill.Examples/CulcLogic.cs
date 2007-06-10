using System;
using System.Collections.Generic;
using System.Text;
using Seasar.Quill.Attrs;

namespace Seasar.Quill.Examples
{
    public class CulcLogic : ICulcLogic
    {
        protected HogeLogic hogeLogic = null;

        [Binding("fugaLogic")]
        protected IFugaLogic fugaLogic;

        #region ICulcLogic �����o

        [Aspect(typeof(ConsoleWriteInterceptor))]
        public int Plus(int x, int y)
        {
            Console.WriteLine("Plus���Ă΂�܂���");
            return LocalPlus(x, y);
        }

        #endregion

        [Aspect(typeof(ConsoleWriteInterceptor))]
        public virtual int LocalPlus(int x, int y)
        {
            fugaLogic.FugaFuga();

            return hogeLogic.HogeHoge(x, y);
        }
    }
}
