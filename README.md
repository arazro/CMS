# 📆 CMS Microservices Project

## 📚 Overview / Genel Bakış

This is a CMS (Content Management System) application structured with a **Microservices Architecture** using **.NET 9**, **PostgreSQL**, **Docker**, and **YARP Gateway**.  
Bu proje, **.NET 9**, **PostgreSQL**, **Docker** ve **YARP Gateway** kullanılarak geliştirilmiş bir **Mikroservis Mimarisine** sahip İçerik Yönetim Sistemi (CMS) uygulamasıdır.

---

## ⚙️ Technologies Used / Kullanılan Teknolojiler

| Technology | Description | Açıklama |
|-----------|-------------|----------|
| .NET 9 | Backend Framework | Backend Çatısı |
| PostgreSQL | Relational Database | İlişkisel Veritabanı |
| Docker & Docker Compose | Containerization | Konteynerleştirme |
| YARP (Yet Another Reverse Proxy) | Gateway | Ters Proxy |
| REST API | Inter-service communication | Servisler arası iletişim |
| Swagger | API documentation | API dokümantasyonu |
| xUnit | Unit Testing | Birim Testleri |
| RabbitMQ (optional) | Async messaging | Asenkron mesajlaşma (isteğe bağlı) |

---

## 🧱 Architecture / Mimari

```plaintext
         ┌─────────────────────┐        ┌─────────────────────┐
         │ ContentService     │◄──────────────▶│  UserService        │
         └─────────────────────┘        └─────────────────────┘
                  │                             │
                  ▼                             ▼
           PostgreSQL DB                 PostgreSQL DB

                     ▲
                     │
              ┌────────────────────┐
              │  YARP Gateway│
              └────────────────────┘
                   │
         ┌───────────────────────────────┐
         │      Client (UI)     │
         └───────────────────────────────┘
```

Each service (Content & User) has its own **API**, **Application**, **Domain**, **Infrastructure**, and **Test** layers.  
Her servis (İçerik & Kullanıcı) kendi **API**, **Application**, **Domain**, **Infrastructure** ve **Test** katmanlarına sahiptir.

---

## 🚀 Running the Project with Docker / Docker ile Projeyi Çalıştırma

### 📦 Requirements / Gereksinimler

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Optional: Visual Studio 2022 or Rider

---

### 🛠️ Step-by-Step / Adım Adım

1. **Clone the repository**  
   Reponunuzu klonlayın:

   ```bash
   git clone https://github.com/arazro/CMS.git
   cd cms-microservices
   ```

2. **Build & Run with Docker Compose**  
   Docker Compose ile projeyi başlatın:

   ```bash
   docker-compose up --build
   ```

   This command will:
   - Build `UserService`, `ContentService`, and `YarpGateway`
   - Setup PostgreSQL for both services
   - Run all containers on the same network

---

### 🔍 API Endpoints (via Swagger)

After running, you can test APIs from:

- `http://localhost:44271/swagger/index.html` → ContentService
- `http://localhost:44272/uswagger/index.html` → UserService
- `http://localhost:44270/swagger/index.html` → YARP Gateway

---

## 🧪 Unit Tests / Birim Testleri

Each service has its own xUnit-based unit tests. To run them:

```bash
dotnet test content-service/ContentService.Tests
dotnet test user-service/UserService.Tests
```

---

## 🔒 Authentication

JWT Authentication is built-in and used during login for secured endpoints.

---

## 📬 Inter-Service Communication / Servisler Arası İletişim

- ContentService verifies if `AuthorId` exists in UserService by sending a RESTful call to `/users/{id}`.
- Example:
  ```csharp
  var response = await _httpClient.GetAsync($"http://user-service/api/users/{authorId}");
  ```

---

## 📁 Folder Structure / Klasör Yapısı

```
cms-microservices/
├ content-service/
│  ├ ContentService.API
│  ├ ContentService.Application
│  ├ ContentService.Domain
│  ├ ContentService.Infrastructure
│  └ ContentService.Tests
├ user-service/
│  ├ UserService.API
│  ├ UserService.Application
│  ├ UserService.Domain
│  ├ UserService.Infrastructure
│  └ UserService.Tests
├ yarp-gateway/
│  ├ YarpGateway.API
│  └ YarpGateway.Tests
├ docker-compose.yml
└ README.md
```

---

## ✍️ Contributors / Katkıda Bulunanlar

- 👤 Araz Rostami  
- 💼 Project Type: Technical Case Study  
- 🌍 Technologies: .NET 9, Microservices, Docker, PostgreSQL, REST

---

## 📌 Notes / Notlar

- Ensure Docker Desktop is running.
- Do not forget to run `dotnet restore` if needed.
- Token authentication can be extended for YARP if needed.

---
