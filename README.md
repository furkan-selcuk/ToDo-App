# 📝 Görev Yönetim Sistemi (ToDo App)

Modern ve kullanıcı dostu arayüze sahip, .NET teknolojileri ile geliştirilmiş, tam kapsamlı görev yönetim uygulaması.

![Proje Görünümü](https://github.com/user-attachments/assets/4f4eda58-63eb-4563-aa07-42696fcf5768)

## 🚀 Özellikler

*   **Görev Yönetimi:** Kolayca görev oluşturun, düzenleyin ve silin.
*   **Kategorilendirme:** İşlerinizi düzenli tutmak için kategori desteği.
*   **Filtreleme:** Görevleri durumuna (Beklemede, Tamamlandı vb.) veya kategorisine göre filtreleyin.
*   **Responsive Tasarım:** Telefon, tablet ve masaüstü cihazlarla tam uyumlu (Bootstrap 5).
*   **Güvenlik:** Kullanıcı girişi ve token tabanlı (JWT) yetkilendirme.

## 🛠️ Teknolojiler

*   **Backend:** .NET 8 Web API
*   **Frontend:** ASP.NET Core MVC
*   **Veritabanı:** Microsoft SQL Server
*   **Arayüz:** HTML5, CSS3, Bootstrap 5

## ⚙️ Kurulum

1.  Bu repoyu bilgisayarınıza klonlayın.
2.  `SQL/MyDatabase.sql` dosyasını çalıştırarak veritabanı tablolarını oluşturun.
3.  `ToDo.WebAPI/appsettings.json` dosyasındaki veritabanı bağlantı ayarlarını kendi sisteminize göre düzenleyin.
4.  Projeyi çalıştırın:
    ```bash
    dotnet run --project ToDo.WebAPI
    dotnet run --project ToDo.MvcUI
    ```
## 🔑 Test Kullanıcı

Projeyi test etmek için hazır bir test kullanıcı kullanabilirsiniz:

* **Kullanıcı Adı:** testuser  
* **Şifre:** 123456

Bu kullanıcı ile uygulamaya giriş yapabilir ve görev yönetimi özelliklerini deneyebilirsiniz.

---
👋 **İyi Kodlamalar!**
