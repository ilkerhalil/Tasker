using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>Uygulamaya eklenti yazabilmek için implemente edilmesi gereken temel interface.</summary>

    /// <remarks>Bu interface'den türeyen tipler zamanlanmýþ bir görevi yerine getirebileceði gibi 
    /// farklý amaçlar için yazýlmýþ eklentilerde olabilirler.</remarks> 
    public interface IModule
    {
        /// <summary>Modül adý. Modülü tanýmlamak için ve diðer modüllerden ayýrabilmek için kullanýlýr</summary>
        string ModuleName { get; }
        /// <summary>Modül parametreleri. Modülün çalýþmak için ihtiyaç duyabileceði parametrelerin modüle 
        /// aktarýlmasýný saðlayan özellik.</summary>
        IDictionary<string, object> ModuleParameters { get; }
    }
}