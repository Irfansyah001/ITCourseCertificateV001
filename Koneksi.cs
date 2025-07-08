using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms; // Penting untuk MessageBox

namespace ITCourseCertificateV001
{
    internal class Koneksi
    {
        // Variabel statis untuk menyimpan string koneksi dan IP yang terakhir berhasil dibaca.
        // Ini akan di-cache di seluruh aplikasi.
        private static string _cachedConnectionString = null;
        private static string _cachedServerIP = null;
        // Variabel ini akan melacak apakah sudah ada upaya pemuatan koneksi,
        // agar MessageBox error tidak muncul berulang kali jika ada banyak form yang mencoba memuatnya.
        private static bool _hasAttemptedLoad = false;

        /// <summary>
        /// Mengembalikan string koneksi database yang sudah di-cache.
        /// Jika belum ada di cache, akan mencoba memuatnya dari file konfigurasi.
        /// </summary>
        /// <returns>String koneksi yang valid, atau string kosong jika gagal.</returns>
        public static string GetConnectionString() // <-- INI ADALAH METODE UTAMA YANG AKAN DIPANGGIL FORM-FORM
        {
            // Jika string koneksi sudah ada di cache, kembalikan saja.
            // Ini mencegah pembacaan file yang tidak perlu berulang kali.
            if (_cachedConnectionString != null && _cachedConnectionString != string.Empty)
            {
                return _cachedConnectionString;
            }

            // Jika belum di-cache atau cache-nya kosong (mungkin karena error sebelumnya),
            // paksa untuk memuat ulang dan membangun string koneksi.
            // Tandai bahwa ada upaya pemuatan, agar MessageBox tidak muncul berulang kali
            // jika banyak form mencoba memuat dan gagal.
            _hasAttemptedLoad = true;
            return LoadAndBuildConnectionString();
        }

        /// <summary>
        /// Memaksa sistem untuk memuat ulang string koneksi dari file konfigurasi.
        /// Ini harus dipanggil setelah file server_config.txt diubah.
        /// </summary>
        /// <returns>String koneksi yang baru dimuat, atau string kosong jika gagal.</returns>
        public static string RefreshConnectionString() // <-- INI AKAN DIPANGGIL OLEH FORM PENGATURAN IP
        {
            _cachedConnectionString = null; // Hapus cache agar dibaca ulang
            _cachedServerIP = null;        // Hapus cache IP juga
            _hasAttemptedLoad = false;     // Reset flag agar MessageBox bisa muncul lagi jika ada error baru
            return LoadAndBuildConnectionString();
        }

        /// <summary>
        /// Metode internal untuk memuat IP dari file konfigurasi, membangun string koneksi, dan meng-cache-nya.
        /// </summary>
        /// <returns>String koneksi yang sudah dibangun, atau string kosong jika gagal.</returns>
        private static string LoadAndBuildConnectionString() // <-- JADIKAN STATIC
        {
            string connectStr = "";
            try
            {
                string serverIP = GetServerIPFromConfig(); // Panggil static GetServerIPFromConfig

                // Gunakan default port 1433 jika tidak ditentukan di IP
                if (!serverIP.Contains(":"))
                {
                    serverIP += ",1433"; // Default SQL Server port
                }

                connectStr = $"Server={serverIP};Initial Catalog=CertificateCourseDB;" +
                             $"User ID=sa;Password=12345678910; TrustServerCertificate=true;";

                _cachedConnectionString = connectStr; // Cache string koneksi yang valid
                _cachedServerIP = serverIP;           // Cache IP juga

                return connectStr;
            }
            catch (FileNotFoundException ex)
            {
                if (!_hasAttemptedLoad) // Tampilkan MessageBox hanya jika ini upaya pemuatan pertama atau setelah refresh
                {
                    string expectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ITCourseCertificate", "server_config.txt");
                    MessageBox.Show("File konfigurasi koneksi database tidak ditemukan!\n" +
                                    "Mohon buat file 'server_config.txt' di:\n" + expectedPath +
                                    "\ndan isi dengan IP Address server SQL Anda (misal: 192.168.1.100).",
                                    "Kesalahan Konfigurasi Aplikasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Console.WriteLine("Error: " + ex.ToString());
                _cachedConnectionString = string.Empty; // Reset cache jika ada error
                return string.Empty;
            }
            catch (Exception ex) // Menangkap semua Exception lain (isi file kosong, format IP salah, gagal membuat file default, dll.)
            {
                if (!_hasAttemptedLoad) // Tampilkan MessageBox hanya jika ini upaya pemuatan pertama atau setelah refresh
                {
                    MessageBox.Show("Gagal membaca atau memvalidasi IP server dari file konfigurasi:\n" + ex.Message +
                                    "\nMohon periksa isi file 'server_config.txt' dan pastikan format IP Address sudah benar.",
                                    "Kesalahan Konfigurasi Aplikasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Console.WriteLine("Error: " + ex.ToString());
                _cachedConnectionString = string.Empty; // Reset cache jika ada error
                return string.Empty;
            }
        }

        /// <summary>
        /// Mengambil IP Address server dari file konfigurasi. Jika file tidak ada, akan membuatnya dengan IP default.
        /// </summary>
        /// <returns>IP Address server yang dibaca atau default.</returns>
        /// <exception cref="Exception">Dilemparkan jika ada masalah saat membaca atau membuat file, atau format IP tidak valid.</exception>
        private static string GetServerIPFromConfig() // <-- JADIKAN STATIC
        {
            string configFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ITCourseCertificate");
            string configPath = Path.Combine(configFolder, "server_config.txt");

            // Pastikan folder konfigurasi ada
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            // Jika file konfigurasi tidak ada, buat dengan IP default
            if (!File.Exists(configPath))
            {
                string defaultIp = "192.168.100.32"; // <-- IP DEFAULT YANG AKAN TERCATAT PERTAMA KALI
                try
                {
                    File.WriteAllText(configPath, defaultIp);
                    // Tidak perlu MessageBox di sini, karena connectionString() yang akan menampilkannya.
                }
                catch (Exception ex)
                {
                    // Jika gagal menulis file, lemparkan Exception agar ditangkap di connectionString()
                    throw new Exception($"Gagal membuat file konfigurasi default: {ex.Message}");
                }
            }

            string ip = File.ReadAllText(configPath).Trim();

            if (string.IsNullOrEmpty(ip))
            {
                throw new Exception("Isi file konfigurasi 'server_config.txt' kosong.");
            }

            // Anda sudah punya IsValidIPv4 di sini
            if (!IsValidIPv4(ip)) // Panggil static IsValidIPv4
            {
                throw new Exception("Format IP tidak valid. Harap masukkan IP Address dengan format IPv4 (contoh: 192.168.100.32).");
            }

            return ip;
        }

        /// <summary>
        /// Memvalidasi apakah sebuah string adalah format IP Address IPv4 yang valid.
        /// </summary>
        /// <param name="ipString">String IP Address untuk divalidasi.</param>
        /// <returns>True jika valid IPv4, False jika tidak.</returns>
        private static bool IsValidIPv4(string ipString) // <-- JADIKAN STATIC
        {
            // Menambahkan validasi untuk kasus di mana user mungkin memasukkan IP:PORT
            // IPAddress.TryParse hanya akan memparsing bagian IP, bukan port.
            // Kita bisa split stringnya dulu.
            string ipPart = ipString.Split(':')[0].Split(',')[0].Trim(); // Hapus bagian port jika ada

            if (IPAddress.TryParse(ipPart, out IPAddress ip))
            {
                return ip.AddressFamily == AddressFamily.InterNetwork; // Menggunakan AddressFamily dari System.Net.Sockets
            }
            return false;
        }

        /// <summary>
        /// Mengambil IP Address lokal IPv4 dari PC yang menjalankan aplikasi.
        /// </summary>
        /// <returns>IP Address lokal IPv4.</returns>
        /// <exception cref="Exception">Dilemparkan jika tidak ada IP Address lokal IPv4 yang valid ditemukan.</exception>
        public static string GetLocalIPAddress() // Ini sudah static
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Tidak dapat menemukan IP Address lokal yang valid.");
        }
    }
}