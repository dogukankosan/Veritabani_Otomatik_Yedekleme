![Yedekleme](https://github.com/user-attachments/assets/024ba561-4ee7-449a-95d2-adbeff71a125)

# 🗄️ Veritabanı Otomatik Yedekleme

## 📝 Tanıtım

Belirli aralıklarla veritabanı yedeklemesi yapan ve yedek dosyalarını güvenli bir şekilde saklayan otomatik bir Windows uygulamasıdır. Özellikle SQL Server gibi veritabanlarında periyodik yedekleme gereksinimi olan şirketler ve sistemler için geliştirilmiştir.

---

## 🚀 Özellikler

- ⏰ Zamanlanmış otomatik yedekleme (günlük, haftalık vb.)
- 🗂️ SQL veritabanı yedeği alma ve farklı klasöre kaydetme
- 🔒 Yedek dosyalarını şifreli veya sıkıştırılmış olarak saklama opsiyonu
- 📅 Görev zamanlayıcı (Task Scheduler veya Timer)
- 📝 Yedekleme geçmişi ve log kaydı
- ⚠️ Hata durumunda uyarı/loglama

---

## 🏗️ Teknik Altyapı

- **Platform:** Windows Forms veya Console App (C#)
- **Veritabanı:** SQL Server (veya uyumlu diğer veritabanları)
- **Zamanlama:** .NET Timer veya Task Scheduler ile periyodik tetikleme
- **Konfigürasyon:** app.config veya JSON dosya üzerinden ayarlanabilir
- **Loglama:** Dosya tabanlı log kaydı (yedekleme başarı/hata takibi)
- **Ekstra:** E-posta ile bilgilendirme veya FTP ile yedek transferi desteği (isteğe bağlı)

---

## 📂 Klasör Yapısı

```
Veritabani_Otomatik_Yedekleme/
├── Backup/             # Yedekleme işlemi ve dosya yönetimi
├── Scheduler/          # Zamanlayıcı veya görev yönetimi
├── Config/             # Ayar dosyaları (app.config, json)
├── Logging/            # Loglama mekanizması
├── Models/             # Yedekleme işlemi modelleri
├── Program.cs          # Uygulama girişi
└── ...
```

---

## ⚙️ Kurulum & Kullanım

1. `app.config` veya `settings.json` dosyasına veritabanı bağlantı bilgilerinizi girin.
2. Yedekleme sıklığı, dosya yolu ve diğer ayarları yapılandırın.
3. Uygulamayı başlatın; belirlenen aralıklarla otomatik yedekleme işlemleri gerçekleşecektir.

---

## 🤝 Katkı

Katkı sağlamak için projeyi forklayabilir ve pull request gönderebilirsiniz.

---

## 📄 Lisans

Bu proje MIT lisansı ile sunulmuştur.
