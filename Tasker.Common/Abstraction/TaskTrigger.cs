using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasker.Common.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cronPrefix"></param>
        public TaskTrigger(string cronPrefix)
        {
            CronPrefix = cronPrefix;
        }

        /// <summary>
        ///  Görevi tetiklemek için tanımlanacak cron ifadelerini tutan liste
        /// </summary>
        /// <remarks>
        /// 
        /// Orjinal makale      : http://www.quartz-scheduler.org/documentation/quartz-2.2.x/tutorials/crontrigger
        /// Türkçeye çeviren    : Murat Güler
        /// 
        /// GİRİŞ:
        /// CRON uzun zamandır bilinen, çok güçlü ve gücü kanıtlanmış zamanlama yetenekleri olan bir linux aracıdır
        /// ITask arayüzünden implemente edilen Task sınıfları cron'un bu yeteneklerini kullanırlar
        /// 
        /// Task sınıfları zaman tetikleyicisi tanımlamak için cron ifadelerini kullanırlar. bu ifadeler ile
        /// "Pazartesi 'den Cumaya her sabah 8'de tetiklen" yada "Her ayın son cuması gece 1.30 da tetiklen"
        /// şeklinde tetikleyiciler tanımlanabilir
        ///  
        /// Cron ifadeleri çok güçlüdürler fakat zaman zaman kafa karışıtırıcı olabilirler
        /// 
        /// Bu makalenin amacı cron ifadeleri oluşturmak ile ilgili bazı gizemleri ortadan kaldırmak ve 
        /// bir kaynak döküman oluşturmaktır.
        /// 
        /// FORMAT:
        /// Cron ifadesi aralarında boşluk bulunan 6 yada 7 alandan oluşan bir string tir 
        /// 
        /// Cron ifadeleri kabul ettiği standart karekterlerin yanında o alan için kabul edilebilir
        /// olan özel karekterlerin combinasyonlarındanda oluşabilir. 
        /// 
        /// Cron ifadelerinin alanları ve kabul ettiği karekterler:
        ///     Alan Adı	                Zorunlumu	Kabul Ettiği Karekterler	Kabul Ettiği Özel Karekterler    
        ///     --------                    ---------   ------------------------    -----------------------------
        ///     Seconds(Saniye)             EVET	    0-59	                    , - * /
        ///     Minutes(Dakika)             EVET        0-59	                    , - * /
        ///     Hours(Saat)                 EVET        0-23	                    , - * /
        ///     Day of month(Ayın Günü)     EVET	    1-31	                    , - * ? / L W
        ///     Month(Ay)                   EVET        1-12 or JAN-DEC	            , - * /
        ///     Day of week(Haftanın Günü)  EVET        1-7 or SUN-SAT	            , - * ? / L #
        ///     Year                        HAYIR       empty, 1970-2099	        , - * /
        /// 
        /// Cron ifadeleri  "* * * * ? *" şeklinde basit ifadeler olabilecekleri gibi 
        /// "0/5 14,18,3-39,52 * ? JAN,MAR,SEP MON-FRI 2002-2010" gibi karmaşık ifadelerde olabilirler
        /// 
        /// ÖZEL KAREKTERLER:
        /// 
        /// "*" ("all values / bütün değerler") - o alanın alabileceği bütün değerleri temsil eder örneğin
        /// dakika alanındaki "*" her dakika anlamına gelir
        /// 
        /// "?" Ayın günü ve haftanın günü alanlarında kullanılabilir fakat aynı anda sadece 1 alanda
        /// kullanılabilir örneğin her ayın 15 günü tetiklemek istenen fakar haftanın hangi gününde 
        /// tetikleneceğinin önemi olmayan bir durumda ayın günü alanına "15" , haftanın günü alanına "?"
        /// ifadesi yazılır
        /// 
        /// "-" Zaman aralığı tanımlamak için kullanılır örneğin saat alanındaki "10-12" ifadesi
        /// saat 10,11 ve 12 anlamındadır
        /// 
        /// "," Aynı alan içerisine ilave değerler tanımlayabilmek için kullanılır örneğin haftanın günleri
        /// alanındaki "MON,WED,FRI" ifadesi "MONDAY(PAZARTESİ),WEDNESDAY(ÇARŞAMBA),FRIDAY(CUMA)" günlerini
        /// ifade eder
        /// 
        /// "/" İfadesi artış tanımlamak için kullanılır örneğin saniye alanındaki "0/15" ifadesi 0,15,30,45.
        /// saniyeleri temisil eder. Saniye alanındaki "5/15" ifadesi ise 5,20,35,50. saniyeleri ifade eder
        /// "/" karekterini " karekterinden sonrada kullanabilirsiniz. Bu durumda " karekteri 0 'a eşdeğerdir
        /// Günler alanındaki "1/3" ifadesi ise ayın 1. gününden itibaresn her 3 günde 1 tetiklenmeyi ifade eder
        /// 
        /// "L" (last) Bu karekter kullanıldığı alana göre farklı bir anlam ifade eder. Örneğin ayın günleri
        /// alanındaki "L" ifadesi Ocak ayı için 31. günü subat ayı için ise 28. günü(artık yıllar için) ifade eder.
        /// Eğer bu ifade haftanın günleri içerisinde TEK BAŞINA kullanılırsa bu basitçe haftanın 7. dolayısı ile
        /// "SAT" ifadesi ile eşdeğerdir yani CUMARTESİ gününü ifade eder. "L" karekteri haftanın günleri alanında
        /// BAŞKA BİR DEĞERDEN sonra kullanıldı ise o ayın son X gününü ifade eder yani "6L" ifadesi o ayın 
        /// son cuma gününü ifade eder. "L-3" şeklindeki ifade ise ayın 3. gününden o ayın son gününe kadar olan
        /// bir zaman dilimini ifade eder. "L" İFADESİNİ KULLANDIĞINIZDA LİSTE YADA ZAMAN ARALIĞI İFADELERİ 
        /// KULLANMAMAYA DİKKAT EDİNİZ EĞER BUNU YAPARSANIZ BEKLENMEDİK SONUÇLAR İLE KARŞILAŞABİLİRSİNİZ
        ///
        /// "W" (weekday) Karekteri haftanın günlerinden (HAFTA SONU DAHİL DEĞİL) kendine en yakın olanı ifade
        /// etmek için kullanılır. Örneğin ayın günleri alanında "15W" ifadesi var ise ve ayın 15. günü CUMARTESİ
        /// ise tetikleme ayın 14'ünde yani CUMA gününde gerçekleşir. Eğer ayın 15. günü PAZAR ise tetikleme
        /// ayın 16. gününde yani PAZARTESİ günü gerçekleşir. Ayın 15. günü SALI ise tetikleyici ayın 15. günü
        /// yani SALI günü tetiklenir. Eğer "1W" ifadesi kullanılmış ise ve ayın 1. günü CUMARTESİ ise tetikleyici
        /// bir önceki ayın son CUMA günü değil içinde bulunulan ayın 3. günü yani PAZARTESİ günü tetiklenir
        /// "W" KAREKTERİ ZAMAN ARALIĞI VE LİSTE İFADELERİ İLE KULLANILMAMALIDIR "L" ve "W" karekterleri kombine
        /// edilerekte kullanılabilir ayın günleri alanındaki "LW" ifadesi içinde bulunanlan ayın son haftasının
        /// o ay içerisindeki son gününü ifade eder
        /// 
        /// "#" Karekteri haftanın günlerinden bir tanesini aylık periodda tanımlamak için kullanılır. Örneğin
        /// "6#3" ayın 3. CUMA gününü belirtir. "2#1" ifadesi ayın ilk PAZARTESİ gününü ifade eder. "4#5" ifadesi
        /// o ayın 5. ÇARŞAMBA günü ifade eder eğer o ay içerisinde 5 tane ÇARŞAMBA günü yok ise tetiklenme olmaz
        /// 
        /// NOT: Karekterler ,Ay ve Gün isimleri büyük/küçüük harf duyarlı değildir.
        ///
        /// ÖRNEKLER :
        /// 
        ///         İfade                               Açıklama
        ///         --------                            -----------
        ///         0 0 12 * * ?	                    Hergün öğlen 12pm (noon) 
        ///         0 15 10 ? * *	                    Her gün öğlenden önce 10:15am 
        ///         0 15 10 * * ?	                    Her gün öğlenden önce 10:15am 
        ///         0 15 10 * * ? *	                    Her gün öğlenden önce 10:15am 
        ///         0 15 10 * * ? 2005	                2015 yılı boyunca Her gün öğlenden önce 10:15am 
        ///         0 * 14 * * ?	                    Hergün öğlenden sonra 2pm de başlayarak gece 2:59pm'e kadar her dakika
        ///         0 0/5 14 * * ?	                    Hergün öğlenden sonra 2pm de başlayarak gece 2:55pm'e kadar her 5 dakikada bir
        ///         0 0/5 14,18 * * ?	                Hergün öğlenden sonra 2pm de başlayarak gece 2:59pm'e kadar her 5 dakikada bir ve her sabah 6pm'en başlayarak sabah 6:55pm'e kadar
        ///         0 0-5 14 * * ?	                    Hergün öğlenden sonra 2pm de başlayarak gece 2:05pm'e kadar her dakika
        ///         0 10,44 14 ? 3 WED                  Mart ayındaki her çarşamba 2:10pm'de ve 2:44pm'de
        ///         0 15 10 ? * MON-FRI                 Pazartesi,Salı,Çarşamba,Perşembe,Cuma 10:15am'de
        ///         0 15 10 15 * ?	                    Her ayın 15. günü 10:15am'de
        ///         0 15 10 L* ?	                    Her ayın son günü 10:15am'de
        ///         0 15 10 L-2 * ?	                    Her ayın 2. gününden son gününe kadar 10:15am'de
        ///         0 15 10 ? * 6L	                    Her ayın son cuma günü 10:15am'de
        ///         0 15 10 ? * 6L 2002-2005	        2002, 2003, 2004 ve 2005 yıllarında her ayın son cuma günü 10:15am'de
        ///         0 15 10 ? * 6#3	                    Her ayın 3. cuma günü 10:15am'de
        ///         0 0 12 1/5 * ?	                    Her ayın 1. gününden başlayarak 5 günde bir öğlen 12pm'de
        ///         0 11 11 11 11 ?	                    Her kasım ayının 11. günü 11:11am'de
        ///         
        ///         NOT : HAFTANIN GÜNÜ VE AYIN GÜNÜ ALANLARINDA "?" VE "*" KAREKTERLERİNİN KULLANIRKEN DİKKATLİ OLUNUZ
        /// 
        /// </remarks>
        public string CronPrefix { get; }


        /// <summary>
        ///Todo : Dökümantasyon gerekiyor. 
        /// </summary>
        public DateTime NextFireTime { get; set; }

        /// <summary>
        /// Todo : Dökümantasyon gerekiyor.
        /// </summary>
        public DateTime PreviousFireTime { get; set; }
    }
}