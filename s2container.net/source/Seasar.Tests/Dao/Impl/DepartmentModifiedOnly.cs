using System;
using System.Collections.Generic;
using System.Text;
using Seasar.Dao.Attrs;
using System.Collections;

namespace Seasar.Tests.Dao.Impl
{
    [Table("DEPT")]
    public class DepartmentModifiedOnly
    {
        private int _deptno;
        private string _dname;
        private string _loc;
        private int _versionNo;
        private string _dummy;
        private IDictionary _modifiedPropertyNames = new Hashtable();

        public int Deptno
        {
            set { 
                _deptno = value;
                _modifiedPropertyNames["Deptno"] = _deptno;
            }
            get { return _deptno; }
        }

        public string Dname
        {
            set { 
                _dname = value;
                _modifiedPropertyNames["Dname"] = _dname;
            }
            get { return _dname; }
        }

        public string Loc
        {
            set { 
                _loc = value;
                _modifiedPropertyNames["Loc"] = _loc;
            }
            get { return _loc; }
        }

        public int VersionNo
        {
            set { 
                _versionNo = value;
                _modifiedPropertyNames["VersionNo"] = _versionNo;
            }
            get { return _versionNo; }
        }

        public bool equals(object other)
        {
            if ( !( other.GetType() == typeof(Department) ) ) return false;
            Department castOther = (Department)other;
            return Deptno == castOther.Deptno;
        }

        public int hashCode()
        {
            return Deptno;
        }

        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append(_deptno).Append(", ");
            buf.Append(_dname).Append(", ");
            buf.Append(_loc).Append(", ");
            buf.Append(_versionNo);
            return buf.ToString();
        }

        public string Dummy
        {
            set { _dummy = value; }
            get { return _dummy; }
        }

        public IDictionary ModifiedPropertyNames
        {
            get { return _modifiedPropertyNames; }
        }

        public void ClearModifiedPropertyNames()
        {
            _modifiedPropertyNames.Clear();
        }
    }
}