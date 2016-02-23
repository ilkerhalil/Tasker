using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// Uygulamaya eklenti yazabilmek i�in implemente edilmesi gereken temel interface.
    /// Bu interface'den t�reyen tipler zamanlanm�� bir g�revi yerine getirebilece�i gibi 
    /// farkl� ama�lar i�in yaz�lm�� eklentilerde olabilirler.
    /// </summary>
    public interface IModule
    {
        /// <summary>Mod�l ad�. Mod�l� tan�mlamak i�in ve di�er mod�llerden ay�rabilmek i�in kullan�l�r</summary>
        /// <value><see cref="System.String"/> tipinde bir de�er.</value>
        string ModuleName { get; }
        /// <summary>Mod�l parametreleri. Mod�l�n �al��mak i�in ihtiya� duyabilece�i parametrelerin mod�le 
        /// aktar�lmas�n� sa�layan �zellik.</summary>
        /// <value> <see cref="System.Collections.Generic.IDictionary{TKey, TValue}" /> tipinden bir instance</value>
        IDictionary<string, object> ModuleParameters { get; }
    }
}