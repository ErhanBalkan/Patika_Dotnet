# Veri ve Veritabanı

## VERİ
Ölçüm, sayım, deney, gözlem veya araştırma sonucuyla elde edilen ham bilgilerdir.

## VERİTABANI
Verilerin organize bir şekilde depolanmasını sağlayan sistemlerdir.

## Düzenli verilere sahip olursak;
* İleriye yönelik geliştirici kararlar verebiliriz.
* Hatalarımızı daha kolay çözeriz.
* Geleceğe yönelik başarılı tahminlerde bulunabiliriz.

Peki, gelelim kritik soruya. Bizler örneğin EXCEL benzeri yazılımlar sayesinde verilerimizi saklayabiliriz. Neden bir veritabanına ihtiyaç duyalım?

* Veritabanı sayesinde sütunlarda bulunacak verilerin aynı veri tipinde olmasını garanti ederiz.
* Veritabanları sayesinde çok büyük boyutlu veri kümeleriyle daha kolay çalışırız.
* Çoklu kullanıcı yönetimi için veri tabanları daha uygundur.
* Veritabanları başka yazılım ve uygulamalarla daha kolay çalışır.

---

# SQL (Structured Query Language) Nedir?
SQL Türkçe ifadesiyle yapılandırılmış sorgu dili anlamına gelmektedir. Biz SQL sayesinde verilerimizin bulunduğu veritabanı ile iletişime geçeriz.

Daha önce de konuştuğumuz gibi veritabanı yönetim sitemi (DBMS) veritabanını barındıra bir yazılım. Bizler bu yazılım üzerinden verilerimiz için yapmak istediğimiz sorguları SQL dili standartlarına uygun olarak yazmak durumundayız.

SQL üzerine konuşulurken ilk olarak şu soru akla gelir. SQL bir programlama dili midir? Evet, SQL ilişkisel veritabanı yönetim sistemleri ile ilişki kurmamızı sağlayan bir `declarative bildirimsel` bir programlama dilidir.

## Bildirimsel Yaklaşım
Aşağıdaki örnek bir SQL sorgusu bulabilirsiniz.

```
SELECT title FROM book
WHERE page_number > 200;
```

Yukarıdaki sorgumuzda, veritabanındaki `book` tablosundan sayfa sayısı 200 den daha fazla olan kitapları görmek istiyoruz. Burada biz işin sonuç kısmıyla ilgileniyoruz. SQL, DBMS ile nasıl çalışır, arka tarafta yapılan işlemin bizim açımızdan önemi yoktur. Bundan dolayı SQL declarative yani bildirimsel, beyan edici bir yaklaşıma sahiptir.

## Dördüncü Nesil Programlama Dili
SQL daha az kod yazarak ve daha çok belirli şablonlar kullanan bir programlama dili olarak dördüncü nesil bir programlama dilidir. Yapılması istenen işlemin her basamağının ayrıca kodlanmasına gerek duyulmaz.

