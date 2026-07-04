# GitHubActionsExample

Minimal ASP.NET Core (net10.0) uygulaması + **GitHub Actions CI** pipeline'ı ile
**SonarCloud kod kalitesi kapısı**.

## Ne yapar?

Her `push` (main) ve `pull_request`'te `.github/workflows/ci.yml` çalışır:

1. Projeyi derler (`dotnet build`).
2. Unit test'leri çalıştırır (`dotnet test`) ve kod kapsamını (coverage) üretir.
3. SonarCloud'a analiz gönderir ve **Quality Gate** sonucunu bekler.

Pipeline şu durumlarda **başarısız** olur (yani "build geçmez"):

- Bir unit test başarısız olursa.
- Derleyici uyarısı çıkarsa (yerelde `TreatWarningsAsErrors` açık).
- SonarCloud **Quality Gate** eşiği geçilemezse (`sonar.qualitygate.wait=true`).

## Yerel geliştirme

```bash
dotnet build      # derle
dotnet test       # testleri çalıştır
dotnet run --project GitHubActionsExample   # http://localhost:5048
```

## SonarCloud kurulumu (ilk kez — bir kereye mahsus)

CI'ın Sonar adımının çalışması için aşağıdakiler gerekir:

1. https://sonarcloud.io adresine GitHub hesabınla giriş yap.
2. Organizasyonunu ve bu repo için bir **proje** oluştur.
3. Projenin **Project Key** ve **Organization Key** değerlerini al ve
   `.github/workflows/ci.yml` içindeki `REPLACE_project_key` ve
   `REPLACE_organization_key` yer tutucularıyla değiştir.
4. SonarCloud'da bir **token** üret (My Account → Security).
5. Bu token'ı GitHub'da repo → **Settings → Secrets and variables → Actions →
   New repository secret** ile `SONAR_TOKEN` adıyla ekle.
6. SonarCloud proje ayarlarında **Analysis Method**'u "GitHub Actions" (CI tabanlı)
   olarak seç; Automatic Analysis'i kapat. Quality Gate olarak varsayılan
   "Sonar way" yeterlidir.

## GitHub'a bağlama

Bu depo yerelde henüz Git ile başlatılmamış olabilir. Workflow yalnızca GitHub'da
çalışır:

```bash
git init
git add .
git commit -m "Initial commit: app + CI + SonarCloud"
git branch -M main
git remote add origin <GitHub-repo-URL>
git push -u origin main
```
