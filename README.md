# AdPlatformLocator

Простой REST API сервис на ASP.NET Core (.NET 8) для поиска рекламных площадок по локациям.

## 🧩 Описание задачи

Сервис позволяет:
- Загружать рекламные площадки с локациями из текстового файла.
- Искать подходящие площадки для заданной локации с учётом вложенности.

Локации представлены в иерархическом виде (`/ru`, `/ru/svrd/ekb`, и т.д.).  
Площадка считается подходящей, если локация из запроса вложена в одну из её локаций.

Пример вложенности:
- `/ru/svrd/ekb` вложена в `/ru/svrd`
- `/ru/svrd` вложена в `/ru`

---

## 🚀 Запуск проекта

### 1. Клонируй репозиторий
```bash
git clone https://github.com/bahromzokirov/AdPlatformLocator.git
cd AdPlatformLocator
```

### 2. Построй и запусти проект
```bash
dotnet build
dotnet run --project AdPlatformLocator
```

По умолчанию приложение запускается на `http://localhost:5000`.

### 3. Открой Swagger
```bash
http://localhost:5000/swagger
```

---

## 🔌 API Методы

### 📥 `POST /platforms/upload`

**Описание:** Загружает файл с рекламными площадками.

- Метод: `POST`
- Content-Type: `multipart/form-data`
- Параметр: файл `.txt`

**Пример содержимого файла:**
```
Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd
```

---

### 🔍 `GET /platforms/search?location=/ru/svrd/revda`

**Описание:** Возвращает список подходящих площадок для указанной локации.

**Пример запроса:**
```
GET /platforms/search?location=/ru/svrd/revda
```

**Пример ответа:**
```json
[
  "Яндекс.Директ",
  "Крутая реклама",
  "Ревдинский рабочий"
]
```

---

## 🧪 Тестирование

Проект включает юнит-тесты (`AdPlatformLocator.Tests`) для:

- Поиска по локациям
- Загрузки данных из файла

**Запуск всех тестов:**
```bash
dotnet test
```

---

## 💡 Используемые технологии

- ASP.NET Core Web API (.NET 8)
- In-Memory Storage (`List<T>`)
- xUnit (для юнит-тестов)
- Swagger / OpenAPI

---

## 👤 Автор

**Bahrom Zokirov**  
GitHub: [@bahromzokirov](https://github.com/bahromzokirov)  
Email: bahromzokirov.60@gmail.com
