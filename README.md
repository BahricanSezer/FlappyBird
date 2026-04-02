Proje Geliştirme Süreci 

1. Fikir ve Görsel Özgünlük (Neon Tema)
Oyunun mekaniği temelde engellerden kaçmaya dayansa da, yönergedeki "birebir kopya olmayacak ve özgün bir görsel stil sağlanacak" şartı için projeyi Neon tarzda tasarladım. Sabit renkli borular yerine kodla zaman içinde (Time.time ve HSV formülüyle) sürekli renk değiştiren engeller yaptım. Ayrıca Post Processing (Bloom) ve Trail Renderer kullanarak o neon hissiyatını güçlendirdim.

2. Karakter Kontrolü (Tek Input)
Karakterin kontrolünü yönergede istendiği gibi sadece tek bir tuşa (Space) bağladım. Yeni Input System'i kullanarak karakterin Rigidbody2D'sine velocity uyguladım ve zıplama mekaniğini kurdum.

3. Oyun Yöneticisi ve Kayıt Sistemi (PlayerPrefs)
Oyunun genel akışını, skoru ve Game Over durumunu yönetmek için bir GameManager scripti yazdım. Ayrıca oyuncunun yaptığı en yüksek skoru (High Score) cihazda tutabilmek için yönergede de belirtilen PlayerPrefs yapısını kullandım.

4. Performans ve Hafıza Yönetimi (Object Pooling)
Yönergedeki en önemli şartlardan biri olan Object Pooling sistemini engeller için kurdum. Boruları sürekli Instantiate ve Destroy ile yaratıp silmek yerine, oyunun başında bir havuz oluşturup sahneden çıkan boruları gizleyerek (SetActive(false)) tekrar tekrar sahneye sürdüm.

5. Zamanlama İşlemleri (Coroutine Kullanımı)
Yönergede "en az bir sistem Coroutine ile yapılmalı" deniyordu, ben projede iki farklı yerde kullandım:

Boru Üretimi: PipeSpawner içinde boruların belirli saniye aralıklarıyla sahneye gelmesini Coroutine ile sağladım.

Game Feel (Ölüm Sekansı): Karakter engele çarptığında anında Game Over ekranını basmak yerine ölüm olayını bir Coroutine'e (DieRoutine) bağladım. Oyuncu öldüğünde karakterin Sprite'ı kapanıyor, neon bir Particle patlıyor, ufak bir Camera Shake (ekran sarsıntısı) oluyor ve saniyeler sonra Game Over paneli geliyor.

6. Veri Yapısı ve Modüler Zorluk (ScriptableObject)
Projedeki "ScriptableObject kullanılmalı" şartını doğrudan oyunun zorluk modlarını tasarlamak için kullandım. DifficultySettings adında bir ScriptableObject oluşturdum; içine boruların hızını, aralarındaki boşluk miktarını, yerçekimi gücünü ve zıplama kuvvetini ekledim. Koda hiç dokunmadan editör üzerinden Easy, Normal ve Hardcore olmak üzere 3 farklı zorluk paketi hazırladım.

7. Sahne Geçişleri ve UI
Ayrı bir Main Menu sahnesi tasarladım. Buradan zorluk seçildiğinde, seçilen zorluğun indeksini PlayerPrefs ile ana sahneye taşıdım ve Awake metodu içinde GameManager'ın doğru ScriptableObject'i çekmesini sağladım. Ayrıca oyuncu deneyimi için, oyun içinde fare imlecini kilitledim (Cursor.lockState) ve öldükten sonra "Play Again" butonuna tıklamaya gerek kalmadan direkt Space tuşuyla oyunu yeniden başlatma fonksiyonunu ekledim.

Projenin çalışır buildi word dosyasındaki drive linkinde bulunmaktadır.
