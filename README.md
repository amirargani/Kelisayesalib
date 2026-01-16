# 🙏 Church Management System (Kelisayesalib)

[![License](https://img.shields.io/badge/License-Apache_2.0-D22128?style=for-the-badge&logo=apache)](LICENSE.txt)
[![Language](https://img.shields.io/badge/Language-C%23-239120.svg?style=for-the-badge&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D6.svg?style=for-the-badge&logo=windows)](https://www.microsoft.com/en-us/windows)
[![Framework](https://img.shields.io/badge/Framework-ASP.NET_Core_5.0-512BD4.svg?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![Database](https://img.shields.io/badge/Database-SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)

A comprehensive church management system designed to support churches and congregations in planning, organizing, and executing worship services and events. Built with ASP.NET Core MVC, this application features intuitive user interfaces tailored for administrators, staff, and attendees.

---

## 📋 Table of Contents

- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Prerequisites](#-prerequisites)
- [Installation](#-installation)
- [Configuration](#-configuration)
- [Project Structure](#-project-structure)
- [Usage](#-usage)
- [Contributing](#-contributing)

---

## ✨ Features

### Core Functionality
- 🗓️ **Service Scheduling**: Create and manage worship dates, locations, speakers, and themes
- 🎤 **Role Assignment**: Assign responsibilities such as preaching, music leadership, technical support, or welcoming
- 📋 **Attendee Management**: Track members, guests, and their participation
- 📊 **Statistics & Reports**: Gain insights into attendance, role distribution, and feedback
- 📰 **News & Announcements**: Share updates and important information with the congregation
- 📅 **Event Management**: Organize and promote church events and activities
- 🎓 **Course Management**: Manage educational programs and classes
- 👥 **Team Management**: Organize ministry teams and volunteers
- 🖼️ **Gallery**: Share photos and memories from church events

### Technical Features
- 🖥️ **Modern User Interface**: Responsive web-based UI optimized for desktop and mobile devices
- 🔐 **User Roles & Permissions**: Tiered access levels using ASP.NET Core Identity
- 🌐 **Multi-language Support**: Includes templates for different languages (Farsi/Persian support)
- 📱 **Mobile Responsive**: Fully responsive design for all screen sizes
- 🔒 **Secure Authentication**: Built-in authentication and authorization system
- 💾 **Database Migrations**: Entity Framework Core migrations for easy database management

---

## 🛠️ Technology Stack

### Backend
- **Framework**: ASP.NET Core 5.0 MVC
- **Language**: C# 9.0
- **ORM**: Entity Framework Core 5.0.1
- **Authentication**: ASP.NET Core Identity 5.0.1
- **Database**: SQL Server (with SQLite support)

### Frontend
- **HTML5** / **CSS3** / **JavaScript**
- **Bootstrap** (Responsive Framework)
- **jQuery** (DOM Manipulation)
- **Template Engine:** Razor Views (MVC)

### Development Tools
- **Visual Studio 2019/2022** or **Visual Studio Code**
- **SQL Server Management Studio** (SSMS)
- **.NET 5.0 SDK**
- **Package Manager:** NuGet
- **Version Control:** Git

---

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) or later
- [SQL Server 2016](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or later (Express Edition is sufficient)
- [Visual Studio 2019/2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/downloads) (for version control)

---

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/Church_Kelisayesalib.git
cd Church_Kelisayesalib
```

### 2. Restore NuGet Packages

```bash
cd APPClinet
dotnet restore
```

### 3. Update Database Connection String

Edit `appsettings.json` and update the connection string to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ChurchDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Apply Database Migrations

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`

---

## ⚙️ Configuration

### Database Configuration

The application supports both SQL Server and SQLite. To switch between databases, modify the connection string in `appsettings.json`:

**SQL Server:**
```json
"DefaultConnection": "Server=localhost;Database=ChurchDB;Trusted_Connection=True;"
```

**SQLite:**
```json
"DefaultConnection": "Data Source=church.db"
```

### User Roles

The system includes the following default roles:
- **Administrator**: Full system access
- **Staff**: Manage events, services, and members
- **Member**: View schedules and participate in events
- **Guest**: Limited read-only access

### File Upload Directories

The application creates the following directories for file uploads:
- `/wwwroot/uploads/images/details/church/`
- `/wwwroot/uploads/images/details/events/`
- `/wwwroot/uploads/images/details/galleries/`
- `/wwwroot/uploads/images/details/courses/`
- `/wwwroot/uploads/images/details/news/`
- `/wwwroot/uploads/images/details/teams/`

---

## 📁 Project Structure

```
Church_Kelisayesalib/
├── APPClinet/                  # Main application project
│   ├── Areas/                  # MVC Areas (Admin, Identity, etc.)
│   ├── Controllers/            # MVC Controllers
│   ├── Models/                 # Data models
│   ├── Views/                  # Razor views
│   ├── wwwroot/                # Static files (CSS, JS, images)
│   ├── Classes/                # Helper classes and utilities
│   ├── Messages/               # Message templates
│   ├── Migrations/             # EF Core migrations
│   ├── faTemplates/            # Farsi/Persian templates
│   ├── Program.cs              # Application entry point
│   ├── Startup.cs              # Application configuration
│   └── appsettings.json        # Configuration settings
├── Church.sln                  # Visual Studio solution file
├── LICENSE.txt                 # Apache 2.0 License
└── README.md                   # This file
```

---

## 💻 Usage

### First-Time Setup

1. **Create Admin Account**: On first run, register a new user and assign admin role through the database
2. **Configure Church Information**: Navigate to Settings to add your church details
3. **Add Team Members**: Create user accounts for staff and volunteers
4. **Set Up Services**: Create your first worship service schedule

### Common Tasks

#### Adding a New Service
1. Navigate to **Services** → **Create New**
2. Fill in service details (date, time, location, theme)
3. Assign roles (speaker, worship leader, etc.)
4. Save and publish

#### Managing Members
1. Go to **Members** section
2. Add new members or import from CSV
3. Assign roles and permissions
4. Track attendance and participation

#### Creating Events
1. Navigate to **Events** → **Create New**
2. Add event details, description, and images
3. Set registration options if needed
4. Publish to the calendar

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards
- Follow C# coding conventions
- Write meaningful commit messages
- Add comments for complex logic
- Update documentation as needed

---

*Developed by Amir Argani*
