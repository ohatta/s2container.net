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

using System;

namespace Seasar.Dao.Examples.AutoInsert
{
    public interface IAutoInsertLogic
    {
        void TestAutoInsert();
    }

    public class AutoInsertLogicImpl : IAutoInsertLogic
    {
        private readonly IEmployeeDao _employeeDao;

        public AutoInsertLogicImpl(IEmployeeDao employeeDao)
        {
            _employeeDao = employeeDao;
        }

        #region IAutoInsertLogic o

        public void TestAutoInsert()
        {
            // ]ΖυΤ9999Μ]ΖυπmF
            int empno = 9999;
            Employee emp1 = _employeeDao.GetEmployeeByEmpno(empno);
            if (emp1 == null)
            {
                Console.WriteLine("]ΖυΤ[" + empno + "]Μ]ΖυΝΆέ΅άΉρB");
            }

            // ]ΖυΤ9999Μ]ΖυπΗΑ
            Employee emp2 = new Employee();
            emp2.Empno = empno;
            emp2.Ename = "eXg";
            emp2.Deptnum = 12;
            int ret = _employeeDao.InsertEmployee(emp2);
            Console.WriteLine("InsertEmployee\bhΜίθl:" + ret);

            // ]ΖυΤ9999Μ]ΖυπmF
            Employee emp3 = _employeeDao.GetEmployeeByEmpno(empno);
            Console.WriteLine("]ΖυΤ[" + empno + "]Μ]ΖυF" + emp3.ToString());

            throw new ForCleanupException();
        }

        #endregion
    }
}
