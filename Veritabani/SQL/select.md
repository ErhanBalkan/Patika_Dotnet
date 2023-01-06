# SELECT
## Sorgu (Query)
SQL komutlarını içeren sorgu cümleleridir.

Üzerine ilk konuşacağımız SQL komutu `SELECT` komutudur. `SELECT` en çok kullanılan SQL komutudur ve veritabanından belirtilen sütunlardaki verileri çekmemizi sağlar. Ayrıca `SELECT` komutunu çoğunlukla diğer SQL komutlarıyla birlikte kullanırız.

SELECT Söz Dizimi
```
SELECT <sütun_adı>, <sütun_adı>, ...
FROM <tablo_adı>;
```

Eğer tablodaki tüm sütunlardaki verileri çekmek istersek `asteriks (*)` karakterinden faydalanırız.

```
SELECT *
FROM <tablo_adı>;
```

`SQL komutlarının büyük harf - küçük harf duyarlılıkları yoktur. (Case Insensitive)`
Aşağıdaki sorguların tamamı aynı sonucu verir.

```
SELECT <sütun_adı>, <sütun_adı>, ...
FROM <tablo_adı>;

select <sütun_adı>, <sütun_adı>, ...
from <tablo_adı>;

Select <sütun_adı>, <sütun_adı>, ...
From <tablo_adı>;
```

Ancak biz bu çalışmalarımız boyunca yazdığımız SQL komutlarının daha kolay anlaşılması açısından `SELECT` örneğinde olduğu gibi büyük harflerden oluşan yazımı kullanacağız.

## SELECT Örnek Kullanım
```
SELECT first_name, last_name
FROM actor;
```
Bu sorgumuzda örnek `dvdrental` veritabanında bulunan `actor` tablosundaki `first_name` ve `last_name` sütunlarında bulunan verileri çekiyoruz.

