# Crazy Musicians API

Crazy Musicians, ASP.NET Core Web API kullanılarak hazırlanmış bir CRUD uygulamasıdır.  
Ödev kapsamında verilen tabloda yer alan "çılgın müzisyenler" için temel API işlemleri geliştirilmiştir.

---

## Özellikler
- **2 GET** endpointi  
  - Tüm müzisyenleri listeleme (+ [FromQuery] ile arama & sayfalama)  
  - ID’ye göre müzisyen getirme  
- **1 POST** endpointi (yeni müzisyen ekleme)  
- **1 PUT** endpointi (tam güncelleme)  
- **1 PATCH** endpointi (kısmi güncelleme – JSON Patch ile)  
- **1 DELETE** endpointi (silme)  
- **Model validation** ([Required], [StringLength])  
- **Swagger UI** üzerinden test imkanı  

