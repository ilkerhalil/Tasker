using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Uygulamaya eklenti yazabilmek için implemente edilmesi gereken temel interface.
    /// Bu interface'den türeyen tipler zamanlanmýþ bir görevi yerine getirebileceði gibi 
    /// farklý amaçlar için yazýlmýþ eklentilerde olabilirler.
    /// </summary>
    public interface IModule
    {
        /// <summary>Modül adý. Modülü tanýmlamak için ve diðer modüllerden ayýrabilmek için kullanýlýr</summary>
        /// <value><see cref="System.String"/> tipinde bir deðer.</value>
        string ModuleName { get; }
        /// <summary>Modül parametreleri. Modülün çalýþmak için ihtiyaç duyabileceði parametrelerin modüle 
        /// aktarýlmasýný saðlayan özellik.</summary>
        /// <value> <see cref="System.Collections.Generic.IDictionary{TKey, TValue}" /> tipinden bir instance</value>
        IDictionary<string, object> ModuleParameters { get; }
    }
}