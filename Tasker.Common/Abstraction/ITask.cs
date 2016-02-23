using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>Zamanlanmış görev oluşturmak için implemente edilmesi gereken temel interface</summary>
    /// <remarks><see cref="Tasker.Common.Abstraction.IModule"/> interface'inden türer</remarks>
    /// 
    public interface ITask:IModule
    {
        /// <summary>
        ///  Görevi tetiklemek için tanımlanacak cron ifadelerini tutan liste
        /// </summary>
        /// <remarks>
        /// 
        /// Orjinal makale      : http://www.quartz-scheduler.org/documentation/quartz-2.2.x/tutorials/crontrigger <br/>
        /// Türkçeye çeviren    : Murat Güler<br/>
        /// <br/>
        /// <b>GİRİŞ:</b><br/>
        /// CRON uzun zamandır bilinen, çok güçlü ve gücü kanıtlanmış zamanlama yetenekleri olan bir linux aracıdır<br/>
        /// ITask arayüzünden implemente edilen Task sınıfları cron'un bu yeteneklerini kullanırlar<br/>
        /// <br/>
        /// Task sınıfları zaman tetikleyicisi tanımlamak için cron ifadelerini kullanırlar. bu ifadeler ile<br/>
        /// "Pazartesi 'den Cumaya her sabah 8'de tetiklen" yada "Her ayın son cuması gece 1.30 da tetiklen"<br/>
        /// şeklinde tetikleyiciler tanımlanabilir<br/>
        ///  <br/>
        /// Cron ifadeleri çok güçlüdürler fakat zaman zaman kafa karışıtırıcı olabilirler<br/>
        /// <br/>
        /// Bu makalenin amacı cron ifadeleri oluşturmak ile ilgili bazı gizemleri ortadan kaldırmak ve <br/>
        /// bir kaynak döküman oluşturmaktır.<br/>
        /// <br/>
        /// <b>FORMAT:</b><br/>
        /// Cron ifadesi aralarında boşluk bulunan 6 yada 7 alandan oluşan bir string tir <br/>
        /// <br/>
        /// Cron ifadeleri kabul ettiği standart karekterlerin yanında o alan için kabul edilebilir<br/>
        /// olan özel karekterlerin combinasyonlarındanda oluşabilir. <br/>
        /// <br/>
        /// Cron ifadelerinin alanları ve kabul ettiği karekterler:<br/>
        ///     <table>
        ///         <thead>
        ///         <tr><th>Alan Adı</th><th>Zorunlumu</th><th>Kabul Ettiği Karekterler</th><th>Kabul Ettiği Özel Karekterler</th></tr>
        ///     </thead>
        ///     <tbody>
        ///     <tr><td>Seconds(Saniye)</td><td>EVET</td><td>0-59</td><td>, - * /</td></tr>
        ///     <tr><td>Minutes(Dakika)</td><td>EVET</td><td>0-59</td><td>, - * /</td></tr>
        ///     <tr><td>Hours(Saat)</td><td>EVET</td><td>0-23</td><td>, - * /</td></tr>
        ///     <tr><td>Day of month(Ayın Günü)</td><td>EVET</td><td>1-31</td><td>, - * ? / L W</td></tr>
        ///     <tr><td>Month(Ay)</td><td>EVET</td><td>1-12 or JAN-DEC</td><td>, - * /</td></tr>
        ///     <tr><td>Day of week(Haftanın Günü)</td><td>EVET</td><td>1-7 or SUN-SAT</td><td>, - * ? / L #</td></tr>
        ///     <tr><td>Year</td><td>HAYIR</td><td>empty, 1970-2099</td><td>, - * /</td></tr>
        ///     </tbody>
        ///     </table>
        /// <br/>
        /// Cron ifadeleri  "* * * * ? *" şeklinde basit ifadeler olabilecekleri gibi <br/>
        /// "0/5 14,18,3-39,52 * ? JAN,MAR,SEP MON-FRI 2002-2010" gibi karmaşık ifadelerde olabilirler<br/>
        /// <br/>
        /// <b>ÖZEL KAREKTERLER:</b><br/>
        /// <br/>
        /// "<b>*</b>" ("all values / bütün değerler") - o alanın alabileceği bütün değerleri temsil eder örneğin<br/>
        /// dakika alanındaki "*" her dakika anlamına gelir<br/>
        /// <br/>
        /// "<b>?</b>" Ayın günü ve haftanın günü alanlarında kullanılabilir fakat aynı anda sadece 1 alanda<br/>
        /// kullanılabilir örneğin her ayın 15 günü tetiklemek istenen fakar haftanın hangi gününde <br/>
        /// tetikleneceğinin önemi olmayan bir durumda ayın günü alanına "15" , haftanın günü alanına "?"<br/>
        /// ifadesi yazılır<br/>
        /// <br/>
        /// "<b>-</b>" Zaman aralığı tanımlamak için kullanılır örneğin saat alanındaki "10-12" ifadesi<br/>
        /// saat 10,11 ve 12 anlamındadır<br/>
        /// <br/>
        /// "<b>,</b>" Aynı alan içerisine ilave değerler tanımlayabilmek için kullanılır örneğin haftanın günleri<br/>
        /// alanındaki "MON,WED,FRI" ifadesi "MONDAY(PAZARTESİ),WEDNESDAY(ÇARŞAMBA),FRIDAY(CUMA)" günlerini<br/>
        /// ifade eder<br/>
        /// <br/>
        /// "<b>/</b>" İfadesi artış tanımlamak için kullanılır örneğin saniye alanındaki "0/15" ifadesi 0,15,30,45.<br/>
        /// saniyeleri temisil eder. Saniye alanındaki "5/15" ifadesi ise 5,20,35,50. saniyeleri ifade eder<br/>
        /// "/" karekterini " karekterinden sonrada kullanabilirsiniz. Bu durumda " karekteri 0 'a eşdeğerdir<br/>
        /// Günler alanındaki "1/3" ifadesi ise ayın 1. gününden itibaresn her 3 günde 1 tetiklenmeyi ifade eder<br/>
        /// <br/>
        /// "<b>L</b>" (last) Bu karekter kullanıldığı alana göre farklı bir anlam ifade eder. Örneğin ayın günleri<br/>
        /// alanındaki "L" ifadesi Ocak ayı için 31. günü subat ayı için ise 28. günü(artık yıllar için) ifade eder.<br/>
        /// Eğer bu ifade haftanın günleri içerisinde TEK BAŞINA kullanılırsa bu basitçe haftanın 7. dolayısı ile<br/>
        /// "SAT" ifadesi ile eşdeğerdir yani CUMARTESİ gününü ifade eder. "L" karekteri haftanın günleri alanında<br/>
        /// BAŞKA BİR DEĞERDEN sonra kullanıldı ise o ayın son X gününü ifade eder yani "6L" ifadesi o ayın <br/>
        /// son cuma gününü ifade eder. "L-3" şeklindeki ifade ise ayın 3. gününden o ayın son gününe kadar olan<br/>
        /// bir zaman dilimini ifade eder. <b>"L" İFADESİNİ KULLANDIĞINIZDA LİSTE YADA ZAMAN ARALIĞI İFADELERİ <br/>
        /// KULLANMAMAYA DİKKAT EDİNİZ EĞER BUNU YAPARSANIZ BEKLENMEDİK SONUÇLAR İLE KARŞILAŞABİLİRSİNİZ</b><br/>
        ///<br/>
        /// "<b>W</b>" (weekday) Karekteri haftanın günlerinden (HAFTA SONU DAHİL DEĞİL) kendine en yakın olanı ifade<br/>
        /// etmek için kullanılır. Örneğin ayın günleri alanında "15W" ifadesi var ise ve ayın 15. günü CUMARTESİ<br/>
        /// ise tetikleme ayın 14'ünde yani CUMA gününde gerçekleşir. Eğer ayın 15. günü PAZAR ise tetikleme<br/>
        /// ayın 16. gününde yani PAZARTESİ günü gerçekleşir. Ayın 15. günü SALI ise tetikleyici ayın 15. günü<br/>
        /// yani SALI günü tetiklenir. Eğer "1W" ifadesi kullanılmış ise ve ayın 1. günü CUMARTESİ ise tetikleyici<br/>
        /// bir önceki ayın son CUMA günü değil içinde bulunulan ayın 3. günü yani PAZARTESİ günü tetiklenir<br/>
        ///<b>"W" KAREKTERİ ZAMAN ARALIĞI VE LİSTE İFADELERİ İLE KULLANILMAMALIDIR</b> "L" ve "W" karekterleri kombine<br/>
        /// edilerekte kullanılabilir ayın günleri alanındaki "LW" ifadesi içinde bulunanlan ayın son haftasının<br/>
        /// o ay içerisindeki son gününü ifade eder<br/>
        /// <br/>
        /// "<b>#</b>" Karekteri haftanın günlerinden bir tanesini aylık periodda tanımlamak için kullanılır. Örneğin<br/>
        /// "6#3" ayın 3. CUMA gününü belirtir. "2#1" ifadesi ayın ilk PAZARTESİ gününü ifade eder. "4#5" ifadesi<br/>
        /// o ayın 5. ÇARŞAMBA günü ifade eder eğer o ay içerisinde 5 tane ÇARŞAMBA günü yok ise tetiklenme olmaz<br/>
        /// <br/>
        /// <b>NOT: Karekterler ,Ay ve Gün isimleri büyük/küçüük harf duyarlı değildir.</b><br/>
        ///<br/>
        /// <b>ÖRNEKLER :</b><br/>
        /// <br/>
        ///    
        ///    <table>
        ///         <thead>
        ///             <tr>
        ///                 <th>İfade</th>
        ///                 <th>Açıklama</th>
        ///             </tr>
        ///         </thead>
        ///         <tbody>
        ///             <tr><td>0 0 12 * * ?</td><td>Hergün öğlen 12pm (noon)</td></tr>
        ///             <tr><td>0 15 10 ? * *</td><td>Her gün öğlenden önce 10:15am</td></tr>
        ///             <tr><td>0 15 10 * * ?</td><td>Her gün öğlenden önce 10:15am</td></tr>
        ///          	<tr><td>0 15 10 * * ? *</td><td>Her gün öğlenden önce 10:15am</td></tr>                 
        ///             <tr><td>0 15 10 * * ? 2005</td><td>2015 yılı boyunca Her gün öğlenden önce 10:15am</td></tr>
        ///             <tr><td>0 * 14 * * ?</td><td>Hergün öğlenden sonra 2pm de başlayarak gece 2:59pm'e kadar her dakika</td></tr>
        ///             <tr><td>0 0/5 14 * * ?</td><td>Hergün öğlenden sonra 2pm de başlayarak gece 2:55pm'e kadar her 5 dakikada bir</td></tr>  
        ///         	<tr><td>0 0/5 14,18 * * ?</td><td>Hergün öğlenden sonra 2pm de başlayarak gece 2:59pm'e kadar her 5 dakikada bir ve her sabah 6pm'en başlayarak sabah 6:55pm'e kadar</td></tr>
        ///             <tr><td>0 0-5 14 * * ?</td><td>Hergün öğlenden sonra 2pm de başlayarak gece 2:05pm'e kadar her dakika</td></tr>
        ///             <tr><td>0 10,44 14 ? 3 WED</td><td>Mart ayındaki her çarşamba 2:10pm'de ve 2:44pm'de</td></tr>         	                    
        ///             <tr><td>0 15 10 ? * MON-FRI</td><td>Pazartesi,Salı,Çarşamba,Perşembe,Cuma 10:15am'de</td></tr>
        ///             <tr><td>0 15 10 15 * ?</td><td>Her ayın 15. günü 10:15am'de</td></tr>                          
        ///             <tr><td>0 15 10 L* ?</td><td>Her ayın son günü 10:15am'de</td></tr>         	                    
        ///             <tr><td>0 15 10 L-2 * ?</td><td>Her ayın 2. gününden son gününe kadar 10:15am'de</td></tr>         	                    
        ///             <tr><td>0 15 10 ? * 6L</td><td>Her ayın son cuma günü 10:15am'de</td></tr>         	                    
        ///             <tr><td>0 15 10 ? * 6L 2002-2005</td><td>2002, 2003, 2004 ve 2005 yıllarında her ayın son cuma günü 10:15am'de</td></tr>         	                    
        ///             <tr><td>0 15 10 ? * 6#3</td><td>Her ayın 3. cuma günü 10:15am'de</td></tr>        	        
        ///         	<tr><td>0 0 12 1/5 * ?</td><td>Her ayın 1. gününden başlayarak 5 günde bir öğlen 12pm'de</td></tr>                    
        ///             <tr><td>0 11 11 11 11 ?</td><td>Her kasım ayının 11. günü 11:11am'de</td></tr>            	                    
        ///         </tbody>
        ///     </table>
        /// <br/>
        ///         <b>NOT : HAFTANIN GÜNÜ VE AYIN GÜNÜ ALANLARINDA "?" VE "*" KAREKTERLERİNİN KULLANIRKEN DİKKATLİ OLUNUZ</b><br/>
        /// </remarks>
        IList<string> CronPrefix { get; }
        /// <summary>
        /// Çalıştırılacak olan görevin adı
        /// </summary>
        string JobName { get; }
        /// <summary>
        /// Görevi başlatan metod. Bir görev için tanımlanmış olan zamanlayıcı tetiklendiği zaman bu metodu
        /// çağırarak görevi başlatır.
        /// </summary>
        void Run();

        //void StartTask();

        //void StopTask();

        //void PauseTask();
    }
}
