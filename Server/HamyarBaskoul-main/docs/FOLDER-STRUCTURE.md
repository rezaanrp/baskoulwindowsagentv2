# Folder Structure

این پروژه برای سیستم باسکول به شکل ماژولار مرتب شده است.

## قواعد کلی

- `Common`: کدهای مشترک، helperها، resultها، interfaceهای عمومی، mapping و security مشترک.
- `Features`: قابلیت‌های عمومی نرم‌افزار مثل Identity، کاربران، داشبورد و مواردی که مخصوص باسکول نیستند.
- `Modules/Baskoul`: همه چیزهایی که مستقیم مربوط به سیستم باسکول است.
- `Persistence`: کدهای دیتابیس، DbContext، repositoryهای عمومی و seedها.
- `Infrastructure`: کدهای زیرساختی یا legacy که نباید با منطق اصلی UI قاطی شوند.

## Domain

- `Common/Entities`: کلاس‌های پایه مثل `AuditableEntity`.
- `Common/Interfaces`: قراردادهای عمومی دامنه.
- `Common/Results`: خروجی‌ها و resultهای مشترک.
- `Features/Identity`: مدل‌ها و قراردادهای مربوط به کاربر، نقش و دسترسی.
- `Modules/Baskoul`: مدل‌ها، view modelهای دامنه و interfaceهای مخصوص باسکول.

## Application

- `Common`: رفتارهای CQRS، validation، mapping، security، file upload و resultهای مشترک.
- `Features/Identity`: سرویس‌ها، DTOها و interfaceهای مربوط به کاربران.
- `Modules/Baskoul`: سرویس‌ها، interfaceها، ابزارها و view modelهای مخصوص باسکول.

## Infra.Data

- `Persistence/Context`: کانتکس EF Core.
- `Persistence/Repositories`: repositoryهای عمومی.
- `Persistence/Seed`: seedهای دیتابیس.
- `Features/Identity`: پیاده‌سازی‌های زیرساختی مربوط به Identity.
- `Modules/Baskoul`: repositoryها و کلاس‌های زیرساختی مخصوص باسکول.
- `Migrations`: فقط migrationهای EF Core.

## WebUI

- `Common`: controllerهای پایه، conventionها، view componentها، helperها، upload و view modelهای مشترک UI.
- `Features/Dashboard`: کنترلرهای داشبورد و صفحه اصلی.
- `Features/Identity`: کنترلرهای مدیریت کاربران.
- `Areas/Identity`: صفحات Identity scaffold.
- `Modules/Baskoul`: کنترلرها و viewهای مخصوص باسکول.
- `Views`: viewهای MVC عمومی که مسیرشان برای Razor مهم است.
- `wwwroot`: فایل‌های static.

