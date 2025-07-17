# 📚 IT Course Certificate (ITC)

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/69ea49a6-480c-4245-94aa-6ae5c09664d7" />

A Windows Forms (.NET Framework) application for managing IT course materials, video-based learning, quizzes, and certificate issuance. This application is built with SQL Server backend, stored procedures, and follows modern validation and error-handling practices.

## 🔧 Features

- 👨‍🏫 **Course & Material Management**  
  - Add, update, delete IT learning materials
  - YouTube link validation & duplicate prevention
  - Duration and author metadata

- 🎥 **Video-based Learning Integration**  
  - Clickable video links (YouTube only)
  - Clean layout for watching materials directly from the app

- 🧠 **Quiz System**  
  - Multiple-choice quizzes with A–D options
  - Global timer for each quiz
  - Automatic scoring & retry feature
  - Certificate generation on passing score

- 📄 **Certificate Generator**  
  - Generates certificate as printable PDF
  - Includes student name, course, score & unique ID

- 💾 **Stored Procedures & Transactions**  
  - All data handled securely through stored procedures
  - Built-in validation (e.g. prevent duplicate video links)

- 🎨 **Dark Mode UI**  
  - Clean, modern interface with centered layout
  - Friendly warning and error prompts

## 🗂️ Technologies Used

- **Frontend:** Windows Forms App (.NET Framework)
- **Backend:** SQL Server 2019+
- **Language:** C# (.NET)
- **Database Access:** ADO.NET with transactions
- **Exporting:** EPPlus for Excel/PDF
- **Regex Validation:** YouTube URL patterns

## 🚀 Getting Started

1. **Clone this repository**  
   ```bash
   git clone https://github.com/your-username/ITCourseCertificateApp.git
   cd ITCourseCertificateApp

2. **Import the database**

   * Use the included `ITCourseCertificateV001DB.sql` to set up the SQL Server database.
   * Make sure the connection string in `Koneksi.cs` is set to your local/remote server.

3. **Install EPPlus License (if needed)**
   Add the following to your project folder (only once):

   ```json
   {
     "EPPlus": {
       "LicenseContext": "NonCommercial"
     }
   }
   ```

4. **Run the project in Visual Studio**

   * Target `.NET Framework 4.7.2` or later
   * Start from `FormLogin.cs`

## ⚠️ Important Validations

* YouTube video links must be unique and valid.
* Material duration must be at least **9 minutes**.
* All input fields are required.
