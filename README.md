# DVLD - Driving & Vehicle License Department System

## Overview

DVLD is a Windows Forms desktop application built using **C# and .NET** that simulates a Driving & Vehicle License Department management system.

The project focuses on applying Object-Oriented Programming (OOP) principles, building a real-world business application, managing data using SQL Server, and implementing system-level features such as Windows Registry storage and Windows Event Log error tracking.

The system allows managing people, users, applications, driving tests, licenses, and different licensing services.

---

## Technologies Used

- C#
- Windows Forms (WinForms)
- .NET Framework
- SQL Server
- ADO.NET
- Object-Oriented Programming (OOP)
- Windows Registry
- Windows Event Viewer

---

## Main Features

### 1. Database Management (SQL Server)

- Stores all application data inside SQL Server database.
- Uses ADO.NET to communicate with the database.
- Implements CRUD operations:
  - Create
  - Read
  - Update
  - Delete

The database manages:

- People information
- System users
- Applications
- License classes
- Driving licenses
- Tests and appointments
- User actions

---

### 2. User Authentication & Windows Registry

- User login system with username and password.
- Saves user credentials securely in Windows Registry.
- Allows the application to remember the last logged-in user.
- Improves user experience by simplifying the login process.

---

### 3. Error Logging Using Windows Event Log

- Handles application exceptions.
- Records errors inside Windows Event Viewer.
- Stores useful debugging information such as:
  - Error message
  - Error source
  - Date and time

This helps with monitoring and troubleshooting application issues.

---

## System Modules

### People Management

Allows users to:

- Add new people
- Update personal information
- Delete people
- Search by National ID
- View person details

---

### User Management

Provides:

- Add system users
- Edit users
- Delete users
- Freeze user accounts
- Store user information

---

### Application Management

Supports:

- Creating new applications
- Searching applications
- Updating application status
- Tracking application fees
- Managing application types

---

### License Management

Supports:

- First-time license issuing
- License renewal
- Replacement of lost licenses
- Replacement of damaged licenses
- International license issuing
- License suspension management

---

### Driving Tests Management

Manages:

- Vision tests
- Theory tests
- Practical driving tests
- Test appointments
- Test results

---

## Architecture & Design

The project follows a layered approach:

### Presentation Layer
Responsible for:

- Forms
- User interface
- User interaction

### Business Logic Layer
Responsible for:

- Application rules
- Validation
- Processing operations

### Data Access Layer
Responsible for:

- Database communication
- SQL queries
- Data retrieval and storage

---

## OOP Concepts Applied

The project demonstrates:

- Classes and Objects
- Encapsulation
- Inheritance
- Abstraction
- Separation of responsibilities

---

## Additional Features

- Input validation
- Exception handling
- Reusable components
- Organized project structure
- User activity tracking

---

## Installation

1. Clone the repository:
2. Open the project using Visual Studio.
3. Restore database backup / execute SQL scripts.
4. Update the database connection string.
5. Build and run the application.
