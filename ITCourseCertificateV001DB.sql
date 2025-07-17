drop database CertificateCourseDB;

CREATE DATABASE CertificateCourseDB;
GO

USE CertificateCourseDB;
GO

--view all data based on that table
select * from DataKursus
select * from Users
select * from Certificate0
select * from Materi
select * from JawabanUser
select * from Kuis

-- TABEL USERS
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL, --HASH
    FullName VARCHAR(100),
    Email VARCHAR(100)
);

-- TABEL KURSUS
CREATE TABLE DataKursus (
    KursusID INT PRIMARY KEY IDENTITY(1,1),
    JudulKursus VARCHAR(100) NOT NULL,
    Deskripsi TEXT
);

EXEC sp_help 'Kuis';

-- TABEL MATERI
CREATE TABLE Materi (
    MateriID INT PRIMARY KEY IDENTITY(1,1),
    KursusID INT FOREIGN KEY REFERENCES DataKursus(KursusID),
    JudulMateri VARCHAR(100),
    LinkVideo VARCHAR(255),
    Urutan INT,
    DurasiMenit INT
);

-- Rev.
ALTER TABLE Materi
ADD Author VARCHAR(100);

-- Rev.Constraint on durasimenit
ALTER TABLE Materi
ADD CONSTRAINT CHK_DurasiMenit_MinValue CHECK (DurasiMenit >= 9);

DELETE FROM Materi WHERE DurasiMenit < 9;

-- Rev. fixed no redudant LinkYoutube

ALTER TABLE Materi
ADD CONSTRAINT UQ_LinkVideo UNIQUE (LinkVideo);

-- TABEL KUIS -- insert sama update store procedure
CREATE TABLE Kuis (
    KuisID INT PRIMARY KEY IDENTITY(1,1),
    KursusID INT FOREIGN KEY REFERENCES DataKursus(KursusID),
    Pertanyaan TEXT NOT NULL,
    PilihanA VARCHAR(100),
    PilihanB VARCHAR(100),
    PilihanC VARCHAR(100),
    PilihanD VARCHAR(100),
    JawabanBenar CHAR(1) CHECK (JawabanBenar IN ('A','B','C','D')),
    Poin INT DEFAULT 20
); -- history kuis/ import saja cukup

-- TABEL JAWABAN USER
CREATE TABLE JawabanUser (
    JawabanID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    KuisID INT FOREIGN KEY REFERENCES Kuis(KuisID),
    Jawaban CHAR(1),
    IsCorrect BIT,
    WaktuJawab DATETIME DEFAULT GETDATE()
);

-- TABEL SERTIFIKAT
CREATE TABLE Certificate0 (
    CertificateID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    KursusID INT FOREIGN KEY REFERENCES DataKursus(KursusID), --
    Nilai FLOAT, --
    TanggalDapat DATETIME DEFAULT GETDATE(), --
    IsPrinted BIT DEFAULT 0
);

ALTER TABLE Certificate0
ADD CertificateIDuniq VARCHAR(50);

-- drop the procedure:
DROP PROCEDURE sp_GetRiwayatKuisLengkapByUserID;
drop procedure UpdateMateri;
drop procedure InsertMateri;
drop procedure DeleteMateri;
drop procedure GetMateriByKursusID;

SET STATISTICS IO ON;
EXEC GetMateriByKursusID 1;
SET STATISTICS IO OFF;

-- rev. stored procedure


-- sp. autenticateuser on form1
IF OBJECT_ID('AuthenticateUser', 'P') IS NOT NULL
    DROP PROCEDURE AuthenticateUser;
GO

CREATE PROCEDURE AuthenticateUser
    @Username VARCHAR(50),
    @Password VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT UserID, FullName
        FROM Users
        WHERE Username = @Username AND Password = @Password;
        -- Jika hashing password diimplementasikan di masa depan, logika ini akan berubah
        -- menjadi SELECT UserID, FullName, PasswordHash, PasswordSalt FROM Users WHERE Username = @Username;
        -- dan verifikasi hash akan dilakukan di C#.
    END TRY
    BEGIN CATCH
        THROW; -- Re-throw the original error
    END CATCH
END;
GO

---
-- Stored Procedure for getting the latest CertificateID for a user
---
-- sp. for Dashboard page and formriwayatkuis
IF OBJECT_ID('GetLatestCertificateIdForUser', 'P') IS NOT NULL
    DROP PROCEDURE GetLatestCertificateIdForUser;
GO

CREATE PROCEDURE GetLatestCertificateIdForUser
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT TOP 1 CertificateID
        FROM Certificate0
        WHERE UserID = @UserID
        ORDER BY TanggalDapat DESC; -- Ambil yang terbaru
    END TRY
    BEGIN CATCH
        THROW; -- Re-throw the original error
    END CATCH
END;
GO

---
-- Stored Procedure for getting FullName by UserID
---
-- sp. for dashboard
IF OBJECT_ID('GetFullNameByUserId', 'P') IS NOT NULL
    DROP PROCEDURE GetFullNameByUserId;
GO

CREATE PROCEDURE GetFullNameByUserId
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT FullName
        FROM Users
        WHERE UserID = @UserID;
    END TRY
    BEGIN CATCH
        THROW; -- Re-throw the original error
    END CATCH
END;
GO

-- sp. for TampilanKerjaKuis page

IF OBJECT_ID('InsertCertificate', 'P') IS NOT NULL
    DROP PROCEDURE InsertCertificate;
GO

CREATE PROCEDURE InsertCertificate
    @UserID INT,
    @KursusID INT,
    @Nilai FLOAT,
    @TanggalDapat DATETIME,
    @IsPrinted BIT,
    @CertificateIDuniq VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Certificate0 (UserID, KursusID, Nilai, TanggalDapat, IsPrinted, CertificateIDuniq)
        VALUES (@UserID, @KursusID, @Nilai, @TanggalDapat, @IsPrinted, @CertificateIDuniq);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW; -- Re-throw the original error
    END CATCH
END;
GO

-- sp. for tampilankerjakankuis
IF OBJECT_ID('InsertJawabanUser', 'P') IS NOT NULL
    DROP PROCEDURE InsertJawabanUser;
GO

CREATE PROCEDURE InsertJawabanUser
    @UserID INT,
    @KuisID INT,
    @Jawaban CHAR(1),
    @IsCorrect BIT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO JawabanUser (UserID, KuisID, Jawaban, IsCorrect, WaktuJawab)
        VALUES (@UserID, @KuisID, @Jawaban, @IsCorrect, GETDATE());

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW; -- Re-throw the original error
    END CATCH
END;
GO

-- sp.Delete Materi
IF OBJECT_ID('DeleteMateri', 'P') IS NOT NULL
    DROP PROCEDURE DeleteMateri;
GO

CREATE PROCEDURE DeleteMateri
    @MateriID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check if the material exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM Materi WHERE MateriID = @MateriID)
        BEGIN
            -- Raise a custom error if the material is not found
            RAISERROR('Materi dengan ID %d tidak ditemukan.', 16, 1, @MateriID);
            -- No need to COMMIT/ROLLBACK here, as no DML was performed on existing data
            RETURN; -- Exit the procedure
        END

        DELETE FROM Materi
        WHERE MateriID = @MateriID;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END;
GO

drop procedure InsertMateri;
-- sp.Insert Materi
DROP PROCEDURE IF EXISTS InsertMateri;
GO

CREATE PROCEDURE InsertMateri
    @KursusID INT,
    @JudulMateri VARCHAR(100),
    @LinkVideo VARCHAR(255),
    @Urutan INT,
    @DurasiMenit INT,
    @Author VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

	-- Validasi durasi minimal 9 menit
    IF @DurasiMenit < 9
    BEGIN
        RAISERROR('Durasi tidak boleh kurang dari 9 menit.', 16, 1);
        RETURN;
    END

	-- Validasi apakah LinkVideo sudah ada
    IF EXISTS (SELECT 1 FROM Materi WHERE LinkVideo = @LinkVideo)
    BEGIN
        RAISERROR('Link video ini sudah pernah ditambahkan.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Materi (KursusID, JudulMateri, LinkVideo, Urutan, DurasiMenit, Author)
        VALUES (@KursusID, @JudulMateri, @LinkVideo, @Urutan, @DurasiMenit, @Author);

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END;
GO


-- sp.Update Materi
DROP PROCEDURE IF EXISTS UpdateMateri;
GO

CREATE PROCEDURE UpdateMateri
    @MateriID INT,
    @KursusID INT,
    @JudulMateri VARCHAR(100),
    @LinkVideo VARCHAR(255),
    @Urutan INT,
    @DurasiMenit INT,
    @Author VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

	-- Validasi durasi minimal 9 menit
    IF @DurasiMenit < 9
    BEGIN
        RAISERROR('Durasi tidak boleh kurang dari 9 menit.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Materi WHERE MateriID = @MateriID)
        BEGIN
            RAISERROR('Materi dengan ID %d tidak ditemukan untuk diupdate.', 16, 1, @MateriID);
            RETURN;
        END

        UPDATE Materi
        SET JudulMateri = @JudulMateri,
            LinkVideo = @LinkVideo,
            Urutan = @Urutan,
            DurasiMenit = @DurasiMenit,
            KursusID = @KursusID,
            Author = @Author
        WHERE MateriID = @MateriID;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO


-- sp.Get Materi by Kursus ID
---
-- Stored Procedure for GetMateriByKursusID with TRY-CATCH
---
DROP PROCEDURE IF EXISTS GetMateriByKursusID;
GO

CREATE PROCEDURE GetMateriByKursusID
    @KursusID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT MateriID, KursusID, JudulMateri, LinkVideo, Urutan, DurasiMenit, Author
        FROM Materi
        WHERE KursusID = @KursusID
        ORDER BY Urutan ASC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO


IF OBJECT_ID('sp_GetRiwayatKuisLengkapByUserID', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRiwayatKuisLengkapByUserID;
GO

-- Modifikasi sp_GetRiwayatKuisLengkapByUserID ver 02
CREATE PROCEDURE sp_GetRiwayatKuisLengkapByUserID
    @UserID INT	
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT
            c.KursusID,
            dk.JudulKursus AS NamaKuis,
            c.Nilai,
            c.TanggalDapat AS TanggalDikerjakan,
            c.CertificateID
        FROM Certificate0 c
        INNER JOIN Users u ON c.UserID = u.UserID
        INNER JOIN DataKursus dk ON c.KursusID = dk.KursusID
        WHERE c.UserID = @UserID
        ORDER BY c.TanggalDapat DESC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

-- sp.FormCertificateViewer

IF OBJECT_ID('GetCertificateDetails', 'P') IS NOT NULL
    DROP PROCEDURE GetCertificateDetails;
GO

CREATE PROCEDURE GetCertificateDetails
    @CertificateID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT
            c.CertificateID,
            u.FullName AS NamaPeserta,
            dk.JudulKursus,
            c.Nilai,
            c.TanggalDapat,
            NULL AS JudulMateri,
            NULL AS LinkVideo,
            NULL AS Urutan,
            NULL AS DurasiMenit
        FROM Certificate0 c
        JOIN Users u ON c.UserID = u.UserID
        JOIN DataKursus dk ON c.KursusID = dk.KursusID
        WHERE c.CertificateID = @CertificateID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

-- sp. Save Certificate
IF OBJECT_ID('SaveCertificate', 'P') IS NOT NULL -- Nama baru untuk menunjukkan fungsi Upsert
    DROP PROCEDURE SaveCertificate;
GO

-- sp Save certivicate
CREATE PROCEDURE SaveCertificate -- ver02
    @CertificateID INT = NULL,
    @UserID INT,
    @KursusID INT,
    @Nilai FLOAT,
    @TanggalDapat DATETIME,
    @IsPrinted BIT,
    @CertificateIDuniq VARCHAR(50) = NULL,
    @OutputCertificateID INT OUTPUT 
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @CertificateID IS NOT NULL AND @CertificateID > 0
        BEGIN
            -- Logic for UPDATE
            UPDATE Certificate0
            SET
                Nilai = @Nilai,
                TanggalDapat = @TanggalDapat,
                IsPrinted = @IsPrinted,
                CertificateIDuniq = ISNULL(@CertificateIDuniq, Certificate0.CertificateIDuniq)
            WHERE CertificateID = @CertificateID
              AND UserID = @UserID
              AND KursusID = @KursusID;

            SET @OutputCertificateID = @CertificateID; 
        END
        ELSE
        BEGIN
            -- Logic for INSERT
            IF @CertificateIDuniq IS NULL OR @CertificateIDuniq = ''
            BEGIN
                SET @CertificateIDuniq = 'CERT-' + CONVERT(VARCHAR(8), GETDATE(), 112) + '-' + LEFT(NEWID(), 4);
            END

            INSERT INTO Certificate0 (UserID, KursusID, Nilai, TanggalDapat, IsPrinted, CertificateIDuniq)
            VALUES (@UserID, @KursusID, @Nilai, @TanggalDapat, @IsPrinted, @CertificateIDuniq);

            SET @OutputCertificateID = SCOPE_IDENTITY(); 
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- indexing

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Users_UsernamePassword' AND object_id = OBJECT_ID('Users'))
    DROP INDEX idx_Users_UsernamePassword ON Users;
GO

CREATE NONCLUSTERED INDEX idx_Users_UsernamePassword
ON Users (Username, Password); -- Composite index untuk query WHERE Username = ... AND Password = ...
GO

-- Tambahkan ini ke file ITCourseCertificateV001DB.sql Anda
-- Index untuk performa mengambil sertifikat terbaru berdasarkan user
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Certificate_UserID_TanggalDapat' AND object_id = OBJECT_ID('Certificate0'))
    DROP INDEX idx_Certificate_UserID_TanggalDapat ON Certificate0;
GO

CREATE NONCLUSTERED INDEX idx_Certificate_UserID_TanggalDapat
ON Certificate0 (UserID, TanggalDapat DESC); -- Urutan DESC penting untuk ORDER BY DESC
GO

-- Tambahkan ini ke file ITCourseCertificateV001DB.sql Anda
-- Index untuk mempercepat pengambilan CertificateID terakhir berdasarkan UserID dan KursusID
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Certificate0_UserKursusTanggalDapat' AND object_id = OBJECT_ID('Certificate0'))
    DROP INDEX idx_Certificate0_UserKursusTanggalDapat ON Certificate0;
GO

CREATE NONCLUSTERED INDEX idx_Certificate0_UserKursusTanggalDapat
ON Certificate0 (UserID, KursusID, TanggalDapat DESC);
GO

---
-- Index for Materi table to optimize retrieval by KursusID and Urutan
---
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Materi_KursusID_Urutan' AND object_id = OBJECT_ID('Materi'))
    DROP INDEX idx_Materi_KursusID_Urutan ON Materi;
GO

CREATE NONCLUSTERED INDEX idx_Materi_KursusID_Urutan
ON Materi (KursusID, Urutan ASC); -- Urutan ASC penting untuk ORDER BY Urutan ASC
GO

CREATE INDEX idx_Kuis_KursusID ON Kuis(KursusID);

CREATE INDEX idx_JawabanUser_UserID ON JawabanUser(UserID);

CREATE INDEX idx_JawabanUser_KuisID ON JawabanUser(KuisID);

CREATE NONCLUSTERED INDEX idx_Materi_KursusID_Author
ON Materi (KursusID, Author);

SELECT UserID, FullName
FROM Users
WHERE Username = @Username AND Password = @Password;

-- see all index on that table
SELECT name AS IndexName, object_name(object_id) AS Materi
FROM sys.indexes
WHERE object_name(object_id) = 'Materi';

SELECT name AS IndexName, object_name(object_id) AS Certificate0
FROM sys.indexes
WHERE object_name(object_id) = 'Certificate0';

SELECT name AS IndexName, object_name(object_id) AS JawabanUser
FROM sys.indexes
WHERE object_name(object_id) = 'JawabanUser';

-- drop index
DROP INDEX idx_Materi_KursusID ON Materi;

DROP INDEX idx_Materi_KursusID_Urutan ON Materi;

DROP INDEX idx_JawabanUser_UserID ON JawabanUser;

DROP INDEX idx_JawabanUser_KuisID ON JawabanUser;

DROP INDEX idx_JawabanUser_UserKuis ON JawabanUser;

DROP INDEX idx_Certificate_UserKursus ON Certificate0;

DROP INDEX idx_Kuis_KursusID ON Kuis;


INSERT INTO Users (Username, Password, FullName, Email)
VALUES 
('admin1', 'admin123', 'Admin Satu', 'admin1@example.com'),
('user1', 'user123', 'User Pertama', 'user1@example.com');

INSERT INTO Users (Username, Password, FullName, Email)
VALUES 
('user2', 'user234', 'User Kedua', 'user2@example.com'),
('user3', 'user345', 'User Ketiga', 'user3@example.com'),
('user4', 'user456', 'User Keempat', 'user4@example.com'),
('user5', 'user567', 'User Kelima', 'user5@example.com');


INSERT INTO Users (Username, Password, FullName, Email)
VALUES 
('Irfansyah', 'Irfansyah123', 'Irfansyah Ridho Aninda', 'Irfansyah@example.com');

INSERT INTO DataKursus (JudulKursus, Deskripsi)
VALUES 
('Dasar-Dasar Pemrograman', 'Belajar dasar-dasar coding menggunakan bahasa C#'),
('Jaringan Komputer', 'Materi tentang pengantar jaringan dan protokol TCP/IP'),
('UI/UX Design', 'Belajar dasar User Interface dan User Experience Design'),
('Database', 'Mengenal sistem basis data dan SQL dasar');

INSERT INTO Materi (KursusID, JudulMateri, LinkVideo, Urutan, DurasiMenit)
VALUES 
(1, 'Pengenalan Pemrograman', 'https://youtu.be/intro-csharp', 1, 10),
(1, 'Variabel dan Tipe Data', 'https://youtu.be/variables-csharp', 2, 12),
(2, 'Apa itu Jaringan?', 'https://youtu.be/networking-intro', 1, 9),
(2, 'Model OSI Layer', 'https://youtu.be/osi-model', 2, 14);


-- DATA KUIS

-- Dasar Pemrograman
INSERT INTO Kuis (KursusID, Pertanyaan, PilihanA, PilihanB, PilihanC, PilihanD, JawabanBenar) VALUES
(1, 'Bahasa pemrograman C# dijalankan menggunakan...', 'Compiler', 'Interpreter', 'Assembler', 'Browser', 'A'),
(1, 'Apa tipe data untuk angka pecahan di C#?', 'int', 'string', 'float', 'char', 'C'),
(1, 'Simbol untuk komentar satu baris di C# adalah...', '//', '/*', '#', '--', 'A'),
(1, 'Method Main() berada di dalam...', 'class', 'interface', 'namespace', 'module', 'A'),
(1, 'Variabel di C# tidak bisa diawali dengan...', 'huruf', 'angka', 'underscore', 'tanda dolar', 'B');

-- Jaringan Komputer
INSERT INTO Kuis (KursusID, Pertanyaan, PilihanA, PilihanB, PilihanC, PilihanD, JawabanBenar) VALUES
(2, 'OSI Layer terdiri dari berapa lapisan?', '5', '6', '7', '8', 'C'),
(2, 'Protokol standar untuk transfer file adalah...', 'HTTP', 'FTP', 'SMTP', 'TCP', 'B'),
(2, 'Perangkat yang menghubungkan dua jaringan berbeda adalah...', 'Router', 'Switch', 'Hub', 'Bridge', 'A'),
(2, 'Port standar HTTP adalah...', '21', '80', '443', '25', 'B'),
(2, 'Alamat IP versi 4 terdiri dari...', '16 bit', '32 bit', '64 bit', '128 bit', 'B');

-- UI/UX Design
INSERT INTO Kuis (KursusID, Pertanyaan, PilihanA, PilihanB, PilihanC, PilihanD, JawabanBenar) VALUES
(3, 'Apa singkatan dari UI?', 'User Input', 'User Interface', 'Universal Input', 'Universal Interface', 'B'),
(3, 'UX berfokus pada...', 'Desain grafis', 'Pengalaman pengguna', 'Bahasa pemrograman', 'Koneksi internet', 'B'),
(3, 'Wireframe digunakan untuk...', 'Database', 'Desain tampilan awal', 'Pemrograman', 'Debugging', 'B'),
(3, 'Warna yang umum untuk error adalah...', 'Hijau', 'Biru', 'Merah', 'Kuning', 'C'),
(3, 'Apa itu prototyping?', 'Debugging kode', 'Simulasi aplikasi sebelum jadi', 'Membuat database', 'Hosting aplikasi', 'B');

-- Database
INSERT INTO Kuis (KursusID, Pertanyaan, PilihanA, PilihanB, PilihanC, PilihanD, JawabanBenar) VALUES
(4, 'Apa singkatan dari SQL?', 'Structured Query Language', 'System Query Line', 'Strong Query Logic', 'Secure Query Language', 'A'),
(4, 'Perintah untuk menampilkan data adalah...', 'SELECT', 'SHOW', 'PRINT', 'GET', 'A'),
(4, 'Kunci primer digunakan untuk...', 'Warna tabel', 'Unikkan baris', 'Koneksi user', 'Menambah kolom', 'B'),
(4, 'Perintah untuk menambahkan data ke tabel adalah...', 'INSERT INTO', 'UPDATE', 'MERGE', 'APPEND', 'A'),
(4, 'Fungsi COUNT digunakan untuk...', 'Menjumlah angka', 'Menjumlah baris', 'Menjumlah kolom', 'Menjumlah tabel', 'B');

-- Update
-- KursusID 1: Dasar Pemrograman
INSERT INTO Materi (KursusID, JudulMateri, LinkVideo, Urutan, DurasiMenit) VALUES
(1, 'Pengenalan C#', 'https://youtu.be/CSharpIntro', 1, 10),
(1, 'Variabel dan Tipe Data', 'https://youtu.be/CSharpVariables', 2, 12),
(1, 'Percabangan If Else', 'https://youtu.be/CSharpIfElse', 3, 15),
(1, 'Perulangan For dan While', 'https://youtu.be/CSharpLoops', 4, 14),
(1, 'Array dan List', 'https://youtu.be/CSharpArrays', 5, 13),
(1, 'Method dan Fungsi', 'https://youtu.be/CSharpMethods', 6, 16),
(1, 'Class dan Object', 'https://youtu.be/CSharpClass', 7, 18),
(1, 'Encapsulation & Inheritance', 'https://youtu.be/CSharpOOP', 8, 17),
(1, 'Exception Handling', 'https://youtu.be/CSharpException', 9, 11),
(1, 'Latihan Mini Project', 'https://youtu.be/CSharpProject', 10, 20);

-- KursusID 2: Jaringan Komputer
INSERT INTO Materi (KursusID, JudulMateri, LinkVideo, Urutan, DurasiMenit) VALUES
(2, 'Apa itu Jaringan?', 'https://youtu.be/NetworkIntro', 1, 9),
(2, 'Model OSI Layer', 'https://youtu.be/NetworkOSI', 2, 14),
(2, 'TCP/IP dan Protokol', 'https://youtu.be/TCPIPExplained', 3, 12),
(2, 'IP Address dan Subnetting', 'https://youtu.be/IPSubnetting', 4, 18),
(2, 'Router & Switch', 'https://youtu.be/RouterSwitch', 5, 15),
(2, 'DNS & DHCP', 'https://youtu.be/DNSDHCP', 6, 13),
(2, 'Firewall dan Keamanan', 'https://youtu.be/NetworkFirewall', 7, 16),
(2, 'Topologi Jaringan', 'https://youtu.be/NetworkTopo', 8, 11),
(2, 'Jaringan Wireless', 'https://youtu.be/WirelessNetworking', 9, 10),
(2, 'Simulasi dengan Cisco Packet Tracer', 'https://youtu.be/CiscoSim', 10, 20);

-- KursusID 3: UI/UX Design
INSERT INTO Materi (KursusID, JudulMateri, LinkVideo, Urutan, DurasiMenit) VALUES
(3, 'Apa itu UI/UX?', 'https://youtu.be/IntroUIUX', 1, 8),
(3, 'Prinsip Dasar Desain', 'https://youtu.be/DesignPrinciples', 2, 12),
(3, 'Wireframing', 'https://youtu.be/Wireframe', 3, 10),
(3, 'Mockup vs Prototype', 'https://youtu.be/MockupPrototype', 4, 9),
(3, 'Tools Figma Dasar', 'https://youtu.be/FigmaBasic', 5, 14),
(3, 'User Flow dan Sitemap', 'https://youtu.be/UserFlow', 6, 13),
(3, 'Desain Responsif', 'https://youtu.be/ResponsiveUI', 7, 16),
(3, 'UX Writing', 'https://youtu.be/UXWriting', 8, 11),
(3, 'Heuristic Evaluation', 'https://youtu.be/HeuristicEval', 9, 10),
(3, 'Finalisasi dan Uji Coba', 'https://youtu.be/UXTesting', 10, 15);

-- KursusID 4: Database
INSERT INTO Materi (KursusID, JudulMateri, LinkVideo, Urutan, DurasiMenit) VALUES
(4, 'Pengenalan Database', 'https://youtu.be/DBIntro', 1, 10),
(4, 'Relasi Tabel dan Primary Key', 'https://youtu.be/DBRelation', 2, 11),
(4, 'Bahasa SQL: SELECT', 'https://youtu.be/SQLSelect', 3, 12),
(4, 'SQL: INSERT, UPDATE, DELETE', 'https://youtu.be/SQLModify', 4, 15),
(4, 'JOIN Antar Tabel', 'https://youtu.be/SQLJoin', 5, 16),
(4, 'Fungsi Aggregat (COUNT, SUM)', 'https://youtu.be/SQLAggregate', 6, 13),
(4, 'WHERE dan ORDER BY', 'https://youtu.be/SQLWhereOrder', 7, 11),
(4, 'GROUP BY dan HAVING', 'https://youtu.be/SQLGroupBy', 8, 10),
(4, 'Subquery dan Nested Query', 'https://youtu.be/SQLSubquery', 9, 14),
(4, 'Studi Kasus Mini Project', 'https://youtu.be/SQLProject', 10, 20);

-- Update link available
UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=gfkTfcpWqAY'
WHERE JudulMateri = 'Pengenalan C#';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=JQtxKqjKoXU'
WHERE JudulMateri = 'Percabangan If Else';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=NbYgm0r7u6o'
WHERE JudulMateri = 'Array dan List';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=1oN7JxLTVzA'
WHERE JudulMateri = 'Variabel dan Tipe Data';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=evQk00rCemo'
WHERE JudulMateri = 'Perulangan For dan While';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=hycocEOIjoI'
WHERE JudulMateri = 'Fungsi dan Prosedur';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=_Ld8wMr4OZ4'
WHERE JudulMateri = 'Kelas dan Objek';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=_nmm0nZqIIY'
WHERE JudulMateri = 'Exception Handling';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=gqlAmSqq6Tw'
WHERE JudulMateri = 'File I/O';

UPDATE Materi
SET LinkVideo = 'https://www.youtube.com/watch?v=EWOENHkxJ0k'
WHERE JudulMateri = 'LINQ';


-- Ganti link yang berlaku pada materi jaringan
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=WPhjxoVDygk' WHERE JudulMateri = 'Apa itu Jaringan?';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=vv4y_uOneC0' WHERE JudulMateri = 'Model OSI Layer';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=2I1HnSN1H9o' WHERE JudulMateri = 'TCP/IP dan Protokol';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=7hIbzlxbebc' WHERE JudulMateri = 'IP Address dan Subnetting';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=qIIRSwnIcaA' WHERE JudulMateri = 'Router & Switch';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=sndVKp6o_7M' WHERE JudulMateri = 'DNS & DHCP';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=Fum7Y8ycQHg' WHERE JudulMateri = 'Firewall dan Keamanan';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=7Ut4u8qVwRU' WHERE JudulMateri = 'Topologi Jaringan';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=6fvAxf-lIIE' WHERE JudulMateri = 'Jaringan Wireless';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=ylsbDW4KTHE' WHERE JudulMateri = 'Simulasi dengan Cisco Packet Tracer';

-- Ganti link yang berlaku pada materi ui/ux
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=AHr8FDRLGIM' WHERE JudulMateri = 'Apa itu UI/UX?';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=vcFyKsUvnZM' WHERE JudulMateri = 'Prinsip Dasar Desain';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=bE9Jh4dSthY' WHERE JudulMateri = 'Wireframing';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=Gt-NnzR_6LA' WHERE JudulMateri = 'Mockup vs Prototype';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=JGLfyTDgfDc' WHERE JudulMateri = 'Tools Figma Dasar';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=vcFyKsUvnZM' WHERE JudulMateri = 'User Flow dan Sitemap';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=vcFyKsUvnZM' WHERE JudulMateri = 'Desain Responsif';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=vcFyKsUvnZM' WHERE JudulMateri = 'UX Writing';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=vcFyKsUvnZM' WHERE JudulMateri = 'Heuristic Evaluation';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=vcFyKsUvnZM' WHERE JudulMateri = 'Finalisasi dan Uji Coba';

-- Ganti link yang berlaku pada materi database
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=23rANwAHuqo' WHERE JudulMateri = 'Pengenalan Database';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=g3U3bWLb01A' WHERE JudulMateri = 'Relasi Tabel dan Primary Key';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=SfAQK131IFU' WHERE JudulMateri = 'Bahasa SQL: SELECT';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=mC6kG2v_xK0' WHERE JudulMateri = 'SQL: INSERT, UPDATE, DELETE';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=ywJ4N-42IAo' WHERE JudulMateri = 'JOIN Antar Tabel';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=IxtmaxnvvZw' WHERE JudulMateri = 'Fungsi Aggregat (COUNT, SUM)';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=oTroUTsuHS4' WHERE JudulMateri = 'WHERE dan ORDER BY';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=1wayTL1Mwt0' WHERE JudulMateri = 'GROUP BY dan HAVING';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=yzV_FVVZxqM' WHERE JudulMateri = 'Subquery dan Nested Query';
UPDATE Materi SET LinkVideo = 'https://www.youtube.com/watch?v=69Iwp7_yNeY' WHERE JudulMateri = 'Studi Kasus Mini Project';
