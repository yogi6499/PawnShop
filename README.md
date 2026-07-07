# 🏦 PawnShop Management System - Backend

A **Multi-Tenant Pawn Shop Management System** built using **ASP.NET Core 8 Web API** and **Entity Framework Core**. The application is designed to digitize pawn shop operations by managing customers, loans, pledged gold items, payments, capital investments, and profit tracking through a ledger-based accounting system.

This project was developed as a real-world SaaS application for managing pawn shop operations while following scalable architecture and clean domain modeling principles.

---

# 🚀 Features

* Multi-Tenant Architecture
* JWT Authentication & Authorization
* Role-Based Access (Owner / Staff)
* Customer Management
* Loan Management
* Gold Item Management
* Interest & Principal Payment Collection
* Ledger-Based Capital Management
* Profit Ledger Management
* Dashboard & Financial Analytics
* Entity Framework Core Code First
* Swagger API Documentation

---

# 🛠️ Technology Stack

| Technology            | Version           |
| --------------------- | ----------------- |
| ASP.NET Core Web API  | .NET 8            |
| Entity Framework Core | 8                 |
| Database              | PostgreSQL (Neon) |
| Authentication        | JWT Bearer Token  |
| Password Encryption   | BCrypt            |
| API Documentation     | Swagger / OpenAPI |

---

# 🏗️ Solution Architecture

```
PawnShop.sln
│
├── PawnShop.API
│
├── PawnShop.Application
│
├── PawnShop.Domain
│
└── PawnShop.Infrastructure
```

### Layer Responsibilities

| Project                 | Responsibility                                                 |
| ----------------------- | -------------------------------------------------------------- |
| PawnShop.API            | Controllers, Authentication, Middleware, Swagger Configuration |
| PawnShop.Application    | DTOs, Business Logic, Service Contracts                        |
| PawnShop.Domain         | Entities, Enums, Domain Models                                 |
| PawnShop.Infrastructure | Entity Framework Core, DbContext, Configurations               |

---

# 🔐 Authentication

Authentication is implemented using JWT Bearer Tokens.

### Authentication Flow

```
Signup
      │
      ▼
Password Encrypted using BCrypt
      │
      ▼
User Login
      │
      ▼
JWT Token Generated
      │
      ▼
Authorization Header
      │
      ▼
Protected APIs
```

### JWT Claims

* UserId
* TenantId
* Email
* Role

---

# 🏢 Multi-Tenant Design

Every business entity is associated with a Tenant.

```
Tenant
    │
    ├── Users
    ├── Customers
    ├── Loans
    ├── Gold Items
    ├── Payments
    ├── Capital Contributors
    ├── Capital Transactions
    └── Profit Transactions
```

Tenant isolation ensures complete separation of business data between pawn shops.

---

# 💾 Database Design

## Core Tables

| Table              | Description                      |
| ------------------ | -------------------------------- |
| Tenant             | Pawn shop information            |
| User               | Owner & Staff accounts           |
| Customer           | Customer information             |
| Loan               | Loan details                     |
| GoldItem           | Pledged gold items               |
| Payment            | Interest & Principal collections |
| CapitalContributor | Capital investors                |
| CapitalTransaction | Capital ledger                   |
| ProfitTransaction  | Profit ledger                    |

---

# 🔗 Entity Relationships

```
Tenant
 │
 ├── Users
 ├── Customers
 │      │
 │      └── Loans
 │              │
 │              ├── GoldItems
 │              ├── Payments
 │              │
 │              ├── CapitalTransactions
 │              │
 │              └── ProfitTransactions
 │
 └── CapitalContributors
         │
         └── CapitalTransactions
```

---

# 💰 Ledger-Based Accounting

Instead of storing current balances directly, every financial activity is recorded as a transaction.

## Capital Ledger

Tracks:

* Capital Added
* Loan Issued
* Principal Received
* Capital Withdrawn
* Capital Adjustments

## Profit Ledger

Tracks:

* Interest Received
* Penalty Received
* Service Fee Received
* Profit Withdrawn
* Profit Reinvested
* Profit Adjustments

This design provides complete financial history and auditability.

---

# 📊 Dashboard Metrics

The Dashboard module provides real-time business insights including:

* Available Capital
* Money on Loan
* Total Capital Invested
* Total Profit Earned
* Current Profit Balance
* Active Loans
* Closed Loans
* Customer Count
* Capital Contributor Summary

---

# 📡 REST API Modules

## Authentication

| Method | Endpoint                    |
| ------ | --------------------------- |
| POST   | `/api/Auth/signup`          |
| POST   | `/api/Auth/login`           |
| POST   | `/api/Auth/change-password` |

---

## Customer Management

| Method | Endpoint             |
| ------ | -------------------- |
| GET    | `/api/Customer`      |
| GET    | `/api/Customer/{id}` |
| POST   | `/api/Customer`      |
| PUT    | `/api/Customer/{id}` |
| DELETE | `/api/Customer/{id}` |

---

## Loan Management

| Method | Endpoint                                           |
| ------ | -------------------------------------------------- |
| POST   | `/api/Loan/CreateLoan`                             |
| POST   | `/api/Loan/CreatePayment`                          |
| GET    | `/api/Loan/GetLoans/{tenantId}`                    |
| GET    | `/api/Loan/GetById/{tenantId}/{id}`                |
| GET    | `/api/Loan/GetLoanHistory/{tenantId}/{customerId}` |

---

## Capital Management

| Method | Endpoint                           |
| ------ | ---------------------------------- |
| POST   | `/api/Capital/add/{tenantId}`      |
| POST   | `/api/Capital/withdraw/{tenantId}` |
| GET    | `/api/Capital/contributors`        |
| POST   | `/api/Capital/contributors`        |
| GET    | `/api/Capital/contributors/{id}`   |
| PUT    | `/api/Capital/contributors/{id}`   |
| DELETE | `/api/Capital/contributors/{id}`   |

---

## Dashboard

| Method | Endpoint                                 |
| ------ | ---------------------------------------- |
| GET    | `/api/Dashboard/GetDashboard/{tenantId}` |

---

# 📌 Business Rules

* Every user belongs to a Tenant.
* JWT is required for protected APIs.
* Passwords are securely stored using BCrypt.
* Every loan belongs to a single customer.
* A loan can contain multiple pledged gold items.
* Interest, Penalty and Service Fees are recorded as Profit.
* Principal repayment increases available Capital.
* Loan issuance decreases available Capital.
* Financial records are never physically deleted.
* Delete behaviors use Restrict to preserve financial integrity.

---

# 🚀 Future Enhancements

* Expense Management
* Soft Delete
* Audit Logging
* PDF Receipt Generation
* WhatsApp / SMS Notifications
* Azure Deployment
* Docker Support
* CI/CD Pipeline
* Multi-Language Support
* Advanced Reporting & Analytics
