using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Uygulamaya eklenti yazabilmek için implemente edilmesi gereken temel interface.
    /// Bu interface'den türeyen tipler zamanlanmýþ bir görevi çalýþtýrabileceði gibi 
    /// farklý amaçlar için yazýlmýþ eklentilerde olabilirler.
    /// </summary>
    public interface IModule
    {
        /// <summary>Modül adý</summary>
        /// <remarks>Modülü tanýmlamak için ve diðer modüllerden ayýrabilmek için kullanýlýr</remarks>
        /// <value><see cref="System.String"/> tipinde bir deðer.</value>
        string ModuleName { get; }
        /// <summary>Modül parametreleri</summary>
        /// <remarks>Modülün çalýþmak için ihtiyaç duyabileceði bilgilerin modüle aktarýlmasýný saðlayan özellik</remarks>
        /// <value> <see cref="System.Collections.Generic.IDictionary{TKey, TValue}" /></value>
        IDictionary<string, object> ModuleParameters { get; }
    }
}