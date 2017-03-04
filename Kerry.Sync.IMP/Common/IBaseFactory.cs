using System.Data;
using System.Text;

namespace Kerry.Sync.IMP.Common
{
    public interface IBaseFactory
    {
        string GetK3Data(StringBuilder sb);
        string InitialInsertStr();
        StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r);
        bool SynK3Data();
    }
}